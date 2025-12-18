using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LodowkaCORE : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject _gracz;
    private bool m_jestInterakcja=false;
    //boxColider łóżka, ten od kolizji, na rodzicu 
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Sprite m_sleepSprite;
    private Sprite m_normalSprite;
   [SerializeField] private AudioSource source;
   [SerializeField] private AudioClip open;
   [SerializeField] private AudioClip close;
   [SerializeField] private GameObject _menu;
    [SerializeField] private ItemSlot[] _itemSlot;
    [SerializeField] private Produkt[] _produkt;
    public void Start()
    {
        _menu.SetActive(false);
        SetForItemSlots();
        ReloadInventory();
    }
    public void Interact()
    {
        if (m_jestInterakcja){
            TurnOFFInteract();
            m_jestInterakcja=false;
        }
        else{
             TurnONInteract();
             m_jestInterakcja=true;
        }
    }

    public void SetForItemSlots()
    {
        for(int i =0;i<_itemSlot.Length; i++){
            _itemSlot[i].SetLodowka(this);
        }
    }
    public void UnFocusAll()
    {
        for(int j=0;j< _itemSlot.Length; j++){
            if (_itemSlot[j].IsUsed()){
                _itemSlot[j].Focus(false);
            }
            }   
    }
    public void ReloadInventory()
    {
        for(int i = 0; i < _produkt.Length; i++)
        {
            for(int j=0;j< _itemSlot.Length; j++)
            {
                if (!_itemSlot[j].IsUsed())
                {
                    _itemSlot[j].AddItem(_produkt[i]);
                    break;
                }
            }
        }
    }
    public void TurnONInteract()
    {
    Debug.Log("Otwieram Lodówkę");
    PlayersDisabes();

    //dźwięk otwierania lodówki
    source.PlayOneShot(open);

    //grafika zapisanie spritea oraz zmiana na sprite otwartej lodówki
    m_normalSprite=GetComponentInParent<SpriteRenderer>().sprite;
    GetComponentInParent<SpriteRenderer>().sprite=m_sleepSprite;

    //uruchomienie menu lodówki
    _menu.SetActive(true);
    }
    public void TurnOFFInteract()
    {
    Debug.Log("Zamykam Lodówkę");
    PlayersEnabes();

    //dźwięk zamykania lodówki;
    source.PlayOneShot(close);

    //wyłączenie menu lodówki
    _menu.SetActive(false);
    
    //grafika
    GetComponentInParent<SpriteRenderer>().sprite=m_normalSprite;

    UnFocusAll();
    }

    private void PlayersDisabes(){
         _gracz.GetComponent<PlayerMovement>().CanMove(false); 
        //ZMIANA TUTAJ JAK JEDNAK LODÓWKA BEZ PRĄDU MA DZIAŁAĆ
      _gracz.GetComponentInChildren<latarka>().Lock();
        if (_gracz.GetComponentInChildren<latarka>().IsFlashlightOn())
        {
            _gracz.GetComponentInChildren<latarka>().turnOff();
        }
    }
    private void PlayersEnabes()
    {
    _gracz.GetComponent<PlayerMovement>().CanMove(true);
    _gracz.GetComponentInChildren<latarka>().Unlock();

    }
}
