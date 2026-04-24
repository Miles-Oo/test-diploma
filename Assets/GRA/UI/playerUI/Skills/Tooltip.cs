using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance;

    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text text;

    private void Awake()
    {
        Instance = this;
        Hide();
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void Show(string content)
    {
        panel.SetActive(true);
        text.text = content;
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}