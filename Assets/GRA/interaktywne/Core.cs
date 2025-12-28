using Unity.VisualScripting;
using UnityEngine;

public abstract class Core: MonoBehaviour
{
    // flipflop 
    private bool m_jestInterakcja=false;
    protected void SetJestInterakcja(bool isInteractja)
    {
        m_jestInterakcja=isInteractja;
    }
    protected bool IsInteractja(){return m_jestInterakcja;}
    [SerializeField] private GameObject _gracz;
    public GameObject GetGracz(){return _gracz;}

    //audio
    [SerializeField] private AudioSource source;
   [SerializeField] private AudioClip openClip;
   [SerializeField] private AudioClip closeClip;


    protected void PlayAudioOn(){source.PlayOneShot(openClip);}
    protected void PlayAudioOff(){source.PlayOneShot(closeClip);}
    protected void PlayAudio(AudioClip audioClip){source.PlayOneShot(audioClip);}

    //nw jak to zrobić aby zwracał inventory, bo inventory może nie musi posidać i to różnego typu.
    public abstract Inventory GetInventory();
    protected void PlayersDisabes(){
         _gracz.GetComponent<PlayerMovement>().CanMove(false); 
        //ZMIANA TUTAJ JAK JEDNAK LODÓWKA BEZ PRĄDU MA DZIAŁAĆ
      _gracz.GetComponentInChildren<latarka>().Lock();
        if (_gracz.GetComponentInChildren<latarka>().IsFlashlightOn())
        {
            _gracz.GetComponentInChildren<latarka>().turnOff();
        }
    }   
    protected void PlayersEnabes()
    {
    _gracz.GetComponent<PlayerMovement>().CanMove(true);
    _gracz.GetComponentInChildren<latarka>().Unlock();

    }
}
