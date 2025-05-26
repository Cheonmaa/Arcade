using System;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class SerialReader : MonoBehaviour
{
    SerialPort serialPort = new SerialPort("COM4", 9600);
    Thread serialThread;
    volatile bool keepReading = true;
    string latestData = "";

    public int buttonState1P1;
    public int buttonState2P1;
    public int buttonState1P2;
    public int buttonState2P2;

    public int x1;
    public int y1;
    public int x2;
    public int y2;

    void Start()
    {
        serialPort.Open();
        serialThread = new Thread(ReadSerial);
        serialThread.Start();
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
            Debug.Log("Received: " + latestData);
            ParseInput(latestData);
            latestData = ""; // clear after processing
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
                buttonState1P1 = int.Parse(part.Substring(7));
            }
            else if (part.StartsWith("BTN2P1:"))
            {
                buttonState2P1 = int.Parse(part.Substring(7));
            }
            else if (part.StartsWith("XP1:"))
            {
                x1 = int.Parse(part.Substring(4));
            }
            else if (part.StartsWith("YP1:"))
            {
                y1 = int.Parse(part.Substring(4));
            }
            else if (part.StartsWith("BTN1P2:"))
            {
                buttonState1P2 = int.Parse(part.Substring(7));
            }
            else if (part.StartsWith("BTN2P2:"))
            {
                buttonState2P2 = int.Parse(part.Substring(7));
            }
            else if (part.StartsWith("XP2:"))
            {
                x2 = int.Parse(part.Substring(4));
            }
            else if (part.StartsWith("YP2:"))
            {
                y2 = int.Parse(part.Substring(4));
            }
            Debug.Log("buttonState1P1:" + buttonState1P1.ToString()+" buttonState2P1:" +buttonState2P1.ToString() +" xP1:" + x1.ToString() + " yP1:" + y1.ToString() + "\nbuttonState1P2:" + buttonState1P2 + " buttonState2P2:" + buttonState2P2 + " xP2:" + x2.ToString() + " yP2:" + y2);
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
