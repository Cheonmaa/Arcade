using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomNavigation : MonoBehaviour
{
    public EventSystem eventSystem;
    public Esp32InputReader esp32InputReader;
    public GameObject selectedObject;

    public float inputDelay = 0.3f;
    private float inputTimer = 0f;

    void Start()
    {
        esp32InputReader = Esp32InputReader.Instance;
        eventSystem.SetSelectedGameObject(selectedObject);
    }

    void Update()
    {
        selectedObject = eventSystem.currentSelectedGameObject;
        if (selectedObject == null)
            return;

        inputTimer += Time.deltaTime;

        if (inputTimer >= inputDelay)
        {
            inputTimer = 0f;

            if (esp32InputReader.y1 == -1)
            {
                MoveUp();
            }
            else if (esp32InputReader.y1 == 1)
            {
                MoveDown();
            }
            else if (esp32InputReader.x1 == -1)
            {
                MoveLeft();
            }
            else if (esp32InputReader.x1 == 1)
            {
                MoveRight();
            }
            else if (esp32InputReader.buttonState1P1)
            {
                ActivateButton();
            }
        }
    }

    void MoveUp()
    {
        Selectable current = eventSystem.currentSelectedGameObject.GetComponent<Selectable>();
        if (current != null && current.navigation.selectOnUp != null)
        {
            eventSystem.SetSelectedGameObject(current.navigation.selectOnUp.gameObject);
        }
    }

    void MoveDown()
    {
        Selectable current = eventSystem.currentSelectedGameObject.GetComponent<Selectable>();
        if (current != null && current.navigation.selectOnDown != null)
        {
            eventSystem.SetSelectedGameObject(current.navigation.selectOnDown.gameObject);
        }
    }

    void MoveLeft()
    {
        Selectable current = eventSystem.currentSelectedGameObject.GetComponent<Selectable>();


        if (current is Slider slider)
        {
            slider.value -= 5f;
        }
        else if (current != null && current.navigation.selectOnLeft != null)
        {
            eventSystem.SetSelectedGameObject(current.navigation.selectOnLeft.gameObject);
        }
    }

    void MoveRight()
    {
        Selectable current = eventSystem.currentSelectedGameObject.GetComponent<Selectable>();
        

        if (current is Slider slider)
        {
            slider.value += 5f;
        }
        else if (current != null && current.navigation.selectOnRight != null)
        {
            eventSystem.SetSelectedGameObject(current.navigation.selectOnRight.gameObject);
        }
    }

    void ActivateButton()
    {
        Button button = selectedObject.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.Invoke();
        }
    }
}
