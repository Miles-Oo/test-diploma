using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LodowkaCORE : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject _gracz;
    private bool m_flipflop=false;
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
        ReloadInventory();
    }
    public void FlipFlop(){
        if (m_flipflop) m_flipflop=false;
        else m_flipflop=true;
    }
    public void Interact()
    {
        if (m_flipflop) TurnOFFInteract();
        else TurnONInteract();
        FlipFlop();
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
      _gracz.GetComponent<PlayerMovement>().CanMove(false); 


        //ZMIANA TUTAJ JAK JEDNAK LODÓWKA BEZ PRĄDU MA DZIAŁAĆ
      _gracz.GetComponentInChildren<latarka>().Lock();
        if (_gracz.GetComponentInChildren<latarka>().IsFlashlightOn())
        {
            _gracz.GetComponentInChildren<latarka>().turnOff();
        }
         source.PlayOneShot(open);


        //KAMERA

      //grafika
      m_normalSprite=GetComponentInParent<SpriteRenderer>().sprite;
      GetComponentInParent<SpriteRenderer>().sprite=m_sleepSprite;
      _menu.SetActive(true);
    }
    public void TurnOFFInteract()
    {
      Debug.Log("Zamykam Lodówkę");

      _gracz.GetComponent<PlayerMovement>().CanMove(true);

      _gracz.GetComponentInChildren<latarka>().Unlock();

       source.PlayOneShot(close);

      _menu.SetActive(false);
       //KAMERA

      //grafika
      GetComponentInParent<SpriteRenderer>().sprite=m_normalSprite;

    }
}
