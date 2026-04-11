using UnityEngine;
using UnityEngine.UI;

public class MiniGameBezpiecznikiButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image diode;
    [SerializeField] private Image button_color;

    [SerializeField] private Color dimmedColor = new Color(0.3f, 0.3f, 0.3f);
    [SerializeField] private Color activeColor = Color.white;

    private const float ALPHA_HIDDEN = 0f;
    private const float ALPHA_DIMMED = 128f / 255f;

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

        SetButtonImageAlpha(ALPHA_HIDDEN); // aktywny → znika
    }

    public void Deactivate()
    {
        if (isSolved) return;

        diode.color = Color.white;
        button.interactable = false;
        SetButtonColor(dimmedColor);

        SetButtonImageAlpha(ALPHA_DIMMED); // nieaktywny → 128
    }

    public void Solve()
    {
        if (isSolved) return;

        isSolved = true;
        diode.color = Color.green;
        button.interactable = false;
        SetButtonColor(activeColor);

        SetButtonImageAlpha(ALPHA_HIDDEN); // rozwiązany → znika
    }

    public void ResetState()
    {
        isSolved = false;
        SetDefault();
    }

    private void OnClick()
    {
        if (isSolved) return;

        SetButtonImageAlpha(ALPHA_HIDDEN); // kliknięty → znika
        controller.PlayerClicked(this);
    }

    private void SetDefault()
    {
        diode.color = Color.white;
        button.interactable = false;
        SetButtonColor(dimmedColor);

        SetButtonImageAlpha(ALPHA_DIMMED); // domyślnie → 128
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

    private void SetButtonImageAlpha(float alpha)
    {
        if (button_color == null) return;

        Color c = button_color.color;
        c.a = alpha;
        button_color.color = c;
    }

    public bool IsSolved()
    {
        return isSolved;
    }
}
