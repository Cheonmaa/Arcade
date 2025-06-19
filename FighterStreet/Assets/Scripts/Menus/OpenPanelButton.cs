using UnityEngine;

public class OpenPanelButton : MonoBehaviour
{
    [SerializeField] private PanelType type;

    private MenuController controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = FindAnyObjectByType<MenuController>();
    }

    public void OnClick()
    {
        controller.OpenPanel(type);
    }
}
