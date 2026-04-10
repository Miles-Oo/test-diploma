using UnityEngine;

public class OrderUI : MonoBehaviour
{
    [SerializeField] private GameObject rootPanel;

    private bool isOpen = false;

    void Start()
    {
        rootPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        isOpen = !isOpen;
        rootPanel.SetActive(isOpen);
    }

    public void Close()
    {
        isOpen = false;
        rootPanel.SetActive(false);
    }
}