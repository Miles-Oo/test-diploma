using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PcMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    public GameObject GetMenuCanvas() 
    { 
        if (_menu == null)
            return null;
        return _menu; 
    }
    
    [SerializeField] private PcDesktop _desktop;
    [SerializeField] private PcTerminal _terminal;

    public void Start()
    {
        if (_menu != null)
            _menu.SetActive(false);
        
        if (_desktop != null)
            _desktop.ShowDesktop();
    }

    public void ActivateTerminal()
    {
        if (_terminal != null)
            _terminal.ActivateTerminal();
    }

    public void DeactivateTerminal()
    {
        if (_terminal != null)
            _terminal.DeactivateTerminal();
    }
    
    public PcDesktop GetDesktop()
    {
        if (_desktop == null)
            return null;
        return _desktop;
    }
    
    public PcTerminal GetTerminal()
    {
        if (_terminal == null)
            return null;
        return _terminal;
    }
}
