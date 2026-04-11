using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public abstract class Core: MonoBehaviour
{
    // flipflop 
    private bool m_jestInterakcja=false;
    protected void SetJestInterakcja(bool isInteractja)
    {
        m_jestInterakcja=isInteractja;
    }
    protected bool IsInteractja(){return m_jestInterakcja;}
    private GameObject _gracz;
    public GameObject GetGracz(){return _gracz;}

    //audio
    private AudioSource source;
   [SerializeField] private AudioClip openClip;
   [SerializeField] private AudioClip closeClip;

    protected virtual void Awake()
    {
     _gracz=Object.FindFirstObjectByType<PlayerMovement>().gameObject;
     source=GetComponent<AudioSource>();
    }
    protected void PlayAudioOn(){source.PlayOneShot(openClip);}
    protected void PlayAudioOff(){source.PlayOneShot(closeClip);}
    protected void PlayAudio(AudioClip audioClip){source.PlayOneShot(audioClip);}

    //nw jak to zrobić aby zwracał inventory, bo inventory może nie musi posidać i to różnego typu.
    public virtual Inventory GetInventory()
    {
        return null;
    }
    protected void PlayersDisabes(){
         _gracz.GetComponent<PlayerMovement>().CanMove(false); 
        //ZMIANA TUTAJ JAK JEDNAK LODÓWKA BEZ PRĄDU MA DZIAŁAĆ
        _gracz.GetComponent<PlayerRotation>().enabled=false;
         GetGracz().GetComponentInChildren<Latarka>().CanvasLightActive(true);
    }   
    protected void PlayersEnabes()
    {
         GetGracz().GetComponentInChildren<Latarka>().CanvasLightActive(false);
    _gracz.GetComponent<PlayerMovement>().CanMove(true);

 _gracz.GetComponent<PlayerRotation>().enabled=true;
    }
}
