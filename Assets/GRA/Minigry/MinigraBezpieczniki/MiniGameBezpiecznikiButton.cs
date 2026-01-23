using UnityEngine;
using UnityEngine.UI;

public class MiniGameBezpiecznikiButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image diode;

    [SerializeField] private Color dimmedColor = new Color(0.3f, 0.3f, 0.3f);
    [SerializeField] private Color activeColor = Color.white;

    private bool isSolved = false;
    private MiniGameBezpiecznikiController controller;

    public void Init(MiniGameBezpiecznikiController ctrl)
    {
        controller = ctrl;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);

        SetDefault();
    }

    public void Activate()
    {
        if (isSolved) return;

        diode.color = Color.red;
        button.interactable = true;
        SetButtonColor(activeColor);
    }

    public void Deactivate()
    {
        if (isSolved) return;

        diode.color = Color.white;
        button.interactable = false;
        SetButtonColor(dimmedColor);
    }

    public void Solve()
    {
        if (isSolved) return;

        isSolved = true;
        diode.color = Color.green;
        button.interactable = false;
        SetButtonColor(activeColor);
    }

    public void ResetState()
    {
        isSolved = false;
        SetDefault();
    }

    private void OnClick()
    {
        if (isSolved) return;
        controller.PlayerClicked(this);
    }

    private void SetDefault()
    {
        diode.color = Color.white;
        button.interactable = false;
        SetButtonColor(dimmedColor);
    }

    private void SetButtonColor(Color color)
    {
        var colors = button.colors;
        colors.normalColor = color;
        colors.highlightedColor = color;
        colors.pressedColor = color;
        colors.selectedColor = color;
        colors.disabledColor = color;
        button.colors = colors;
    }

    public bool IsSolved()
    {
        return isSolved;
    }
}
