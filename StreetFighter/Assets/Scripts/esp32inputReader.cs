using System;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

/// <summary>
/// This read SerialInput (which is made by the c++ code) and 
///- turns button into boolean with true if pressed
///- turns joystick values into -1,0,1 depending of its orientation.
/// </summary>

public class Esp32InputReader : MonoBehaviour
{
    public static Esp32InputReader Instance;

    SerialPort serialPort = new SerialPort("COM4", 9600);
    Thread serialThread;
    volatile bool keepReading = true;
    string latestData = "";

    public bool buttonState1P1;
    public bool buttonState2P1;
    public bool buttonState1P2;
    public bool buttonState2P2;

    public int x1, y1, x2, y2;

    public bool debug;

    void Awake()
    {
        // Ensure only one instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        debug = false;
        DetectAndOpenPort();
    }

    void DetectAndOpenPort()
    {
        string[] ports = SerialPort.GetPortNames();

        foreach (string portName in ports)
        {
            try
            {
                SerialPort testPort = new SerialPort(portName, 9600);
                testPort.ReadTimeout = 1000;
                testPort.Open();

                // Flushes the first (possibly partial) line
                if (testPort.BytesToRead > 0)
                    testPort.ReadLine();

                string testLine = testPort.ReadLine();

                if (testLine.StartsWith("BTN"))
                {
                    Debug.Log("ESP32 Found on " + portName);
                    serialPort = testPort;
                    serialThread = new Thread(ReadSerial);
                    serialThread.Start();
                    return;
                }

                testPort.Close();
            }
            catch (Exception e)
            {
                Debug.LogWarning("Could not use " + portName + ": " + e.Message);
            }
        }

        Debug.LogError("ESP32 not found on any COM port.");
    }

    void ReadSerial()
    {
        while (keepReading)
        {
            try
            {
                string line = serialPort.ReadLine();
                latestData = line;
            }
            catch (Exception) { }
        }
    }

    void Update()
    {
        if (!string.IsNullOrEmpty(latestData))
        {
            ParseInput(latestData);
            latestData = ""; // Clear after processing
        }
    }

    void ParseInput(string data)
    {
        // Example input:
        // BTN1P1:1 BTN2P1:1 XP1:1839 YP1:1782 BTN1P2:1 BTN2P2:1 XP2:1764 YP2:1806

        string[] parts = data.Split(' ');

        foreach (string part in parts)
        {
            if (part.StartsWith("BTN1P1:"))
            {
                buttonState1P1 = int.Parse(part.Substring(7)) == 1;
            }
            else if (part.StartsWith("BTN2P1:"))
            {
                buttonState2P1 = int.Parse(part.Substring(7)) == 1;
            }
            else if (part.StartsWith("XP1:"))
            {
                x1 = int.Parse(part.Substring(4));
                if (x1 <= 100)
                    x1 = -1;
                else if (x1 >= 4000)
                    x1 = 1;
                else
                    x1 = 0;
            }
            else if (part.StartsWith("YP1:"))
            {
                y1 = int.Parse(part.Substring(4));
                if (y1 <= 100)
                    y1 = -1;
                else if (y1 >= 4000)
                    y1 = 1;
                else
                    y1 = 0;
            }
            else if (part.StartsWith("BTN1P2:"))
            {
                buttonState1P2 = int.Parse(part.Substring(7)) == 1;
            }
            else if (part.StartsWith("BTN2P2:"))
            {
                buttonState2P2 = int.Parse(part.Substring(7)) == 1;
            }
            else if (part.StartsWith("XP2:"))
            {
                x2 = int.Parse(part.Substring(4));
                if (x2 <= 100)
                    x2 = -1;
                else if (x2 >= 4000)
                    x2 = 1;
                else
                    x2 = 0;
            }
            else if (part.StartsWith("YP2:"))
            {
                y2 = int.Parse(part.Substring(4));
                if (y2 <= 100)
                    y2 = -1;
                else if (y2 >= 4000)
                    y2 = 1;
                else
                    y2 = 0;
            }
        }

        if (debug)
        {
            Debug.Log($"buttonState1P1:{buttonState1P1} buttonState2P1:{buttonState2P1} xP1:{x1} yP1:{y1} " +
                      $"buttonState1P2:{buttonState1P2} buttonState2P2:{buttonState2P2} xP2:{x2} yP2:{y2}");
        }
    }

    void OnApplicationQuit()
    {
        keepReading = false;

        if (serialThread != null && serialThread.IsAlive)
            serialThread.Join();

        if (serialPort != null && serialPort.IsOpen)
            serialPort.Close();
    }
}
