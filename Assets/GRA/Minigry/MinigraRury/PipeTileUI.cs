using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PipeTileUI : MonoBehaviour, IPointerClickHandler
{
    // Bitmask: 1=Up, 2=Right, 4=Down, 8=Left
    public const int UP = 1, RIGHT = 2, DOWN = 4, LEFT = 8;

    public enum PipeType { Straight, Elbow, TJunction, Cross }

    [Header("Runtime")]
    public PipeType type;
    [Range(0, 3)] public int rotationSteps;
    public bool locked;
    public bool isStart;
    public bool isEnd;

    [Header("Highlight Colors")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color dimColor = new Color(1f, 1f, 1f, 0.30f);
    [SerializeField] private Color startColor = new Color(0.2f, 1f, 0.2f, 1f);
    [SerializeField] private Color endColor = new Color(1f, 0.2f, 0.2f, 1f);

    private PipePuzzleUIMinigame _mgr;
    private Image _img;
    private RectTransform _rt;

    private void Awake()
    {
        _img = GetComponent<Image>();
        _rt = GetComponent<RectTransform>();
    }

    public void SetManager(PipePuzzleUIMinigame mgr) => _mgr = mgr;

    public void Setup(PipeType t, int rotSteps, bool isLocked, bool start, bool end, Sprite sprite)
    {
        type = t;
        rotationSteps = rotSteps % 4;
        locked = isLocked;
        isStart = start;
        isEnd = end;

        if (_img != null && sprite != null) _img.sprite = sprite;

        ApplyRotationVisual();
        SetHighlighted(false);
    }

    // WAŻNE: BAZA musi odpowiadać Twoim sprite’om przy rotationSteps=0.
    // Jeśli Straight w 0° jest poziomy -> zmień na LEFT|RIGHT.
    // Jeśli Elbow w 0° to np. Right+Down -> zmień.
    public int BaseMask()
    {
        return type switch
        {
            PipeType.Straight  => UP | DOWN,
            PipeType.Elbow     => DOWN | RIGHT,
            PipeType.TJunction => UP | RIGHT | DOWN,        // baza: brak LEFT
            PipeType.Cross     => UP | RIGHT | DOWN | LEFT,
            _ => 0
        };
    }

    public int CurrentMask()
    {
        int mask = BaseMask();
        int steps = ((rotationSteps % 4) + 4) % 4;
        for (int i = 0; i < steps; i++) mask = RotateMask90(mask);
        return mask;
    }

    private int RotateMask90(int mask)
    {
        // 90° zgodnie z ruchem wskazówek: Up->Right->Down->Left->Up
        int newMask = 0;
        if ((mask & UP) != 0) newMask |= RIGHT;
        if ((mask & RIGHT) != 0) newMask |= DOWN;
        if ((mask & DOWN) != 0) newMask |= LEFT;
        if ((mask & LEFT) != 0) newMask |= UP;
        return newMask;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (locked) return;

        rotationSteps = (rotationSteps + 1) % 4;
        ApplyRotationVisual();
        _mgr?.OnTileRotated();
    }

    public void ForceVisual()
    {
        ApplyRotationVisual();
    }

    private void ApplyRotationVisual()
    {
        if (_rt != null)
            _rt.localEulerAngles = new Vector3(0f, 0f, -90f * rotationSteps);
    }

    public void SetHighlighted(bool highlighted)
    {
        if (_img == null) return;

        if (isStart)
        {
            _img.color = highlighted ? startColor : new Color(startColor.r, startColor.g, startColor.b, dimColor.a);
            return;
        }
        if (isEnd)
        {
            _img.color = highlighted ? endColor : new Color(endColor.r, endColor.g, endColor.b, dimColor.a);
            return;
        }

        _img.color = highlighted ? normalColor : dimColor;
    }
}
