using UnityEngine;
using UnityEngine.UI;

public class PcDesktop : MonoBehaviour
{
    [SerializeField] private GameObject _desktopPanel;
    [SerializeField] private DesktopIcon[] _icons;

    public void Start()
    {
        if (_desktopPanel != null)
            _desktopPanel.SetActive(true);
    }

    public void ShowDesktop()
    {
        if (_desktopPanel != null)
            _desktopPanel.SetActive(true);
    }

    public void HideDesktop()
    {
        if (_desktopPanel != null)
            _desktopPanel.SetActive(false);
    }

    public GameObject GetDesktopPanel()
    {
        if (_desktopPanel == null)
            return null;
        return _desktopPanel;
    }
}
