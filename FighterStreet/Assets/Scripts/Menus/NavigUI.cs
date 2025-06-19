using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class NavigUI : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject optionsPanel;

    public GameObject firstMenuButton;
    public GameObject firstOptionsButton;

    public GameObject current;

    public Esp32InputReader input;
    public float inputDelay = 0.5f;
    public float lastInputTime = 0f;

    void Start()
    {
        input = Esp32InputReader.Instance;
        SetInitialSelection();
    }

    void Update()
    {
        if (Time.time - lastInputTime > inputDelay)
        {
            bool moved = false;
            if (input.y1 == -1)
            {
                moved = true;
            }
            else if (input.y1 == 1)
            {
                MoveSelection(Vector2.down);
                moved = true;
            }

            if (moved)
                lastInputTime = Time.time;
        }

        if (input.buttonState1P1)
        {
            PressCurrent();
        }
    }

    void SetInitialSelection()
    {
        GameObject first = menuPanel.activeSelf ? firstMenuButton : firstOptionsButton;
        EventSystem.current.SetSelectedGameObject(first);
    }

    void MoveSelection(Vector2 direction)
    {
        current = EventSystem.current.currentSelectedGameObject;
        if (current == null) return;

        Selectable currentSelectable = current.GetComponent<Selectable>();
        if (currentSelectable == null) return;

        Selectable next = direction == Vector2.up ?
            currentSelectable.FindSelectableOnUp() :
            currentSelectable.FindSelectableOnDown();

        // Only switch to buttons that are active in the same panel
        if (next != null && next.gameObject.activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(next.gameObject);
        }
    }

    bool IsInActivePanel(GameObject obj)
    {
        return (menuPanel.activeSelf && obj.transform.IsChildOf(menuPanel.transform)) ||
               (optionsPanel.activeSelf && obj.transform.IsChildOf(optionsPanel.transform));
    }

    void PressCurrent()
    {
        current = EventSystem.current.currentSelectedGameObject;
        if (current == null) return;

        Button btn = current.GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.Invoke();
        }
    }
}