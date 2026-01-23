using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DesktopIcon : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _iconLabel;
    [SerializeField] private Sprite _normalSprite;
    [SerializeField] private Sprite _highlightSprite;
    [SerializeField] private string _iconName;
    
    private PcDesktop _desktop;
    private PcTerminal _terminal;
    private bool _isSelected = false;

    public void Initialize(PcDesktop desktop, PcTerminal terminal, string name, Sprite icon)
    {
        _desktop = desktop;
        _terminal = terminal;
        _iconName = name;
        
        if (_iconImage != null && icon != null)
        {
            _iconImage.sprite = icon;
            _normalSprite = icon;
        }
        
        if (_iconLabel != null)
            _iconLabel.text = name;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // Podwójne kliknięcie - otwórz terminal
            if (eventData.clickCount == 2)
            {
                OpenTerminal();
            }
            else
            {
                // Pojedyncze kliknięcie - zaznacz ikonę
                SelectIcon();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isSelected && _iconImage != null && _highlightSprite != null)
        {
            _iconImage.sprite = _highlightSprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_isSelected && _iconImage != null && _normalSprite != null)
        {
            _iconImage.sprite = _normalSprite;
        }
    }

    private void SelectIcon()
    {
        _isSelected = true;
        if (_iconImage != null && _highlightSprite != null)
            _iconImage.sprite = _highlightSprite;
    }

    private void DeselectIcon()
    {
        _isSelected = false;
        if (_iconImage != null && _normalSprite != null)
            _iconImage.sprite = _normalSprite;
    }

    private void OpenTerminal()
    {
        if (_terminal != null)
        {
            _terminal.ShowTerminalOptions();
        }
    }
    
    public void SetHighlightSprite(Sprite highlight)
    {
        _highlightSprite = highlight;
    }

    public void SetTerminal(PcTerminal terminal)
    {
        _terminal = terminal;
    }
}
