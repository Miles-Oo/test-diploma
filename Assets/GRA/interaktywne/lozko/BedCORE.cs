using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BedTAG : MonoBehaviour, IInteractable
{
    [SerializeField] gamewordTimer _gamewordTimer;
    [SerializeField] GameObject _gracz;
    [SerializeField] Transform _sleepPoint;
    [SerializeField] Transform _wakeUpPoint;

    private bool m_flipflop=false;
    //boxColider łóżka, ten od kolizji, na rodzicu 
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Sprite m_sleepSprite;
    private Sprite m_normalSprite;
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

    public void TurnONInteract()
    {
      Debug.Log("Kładę się spać");
      _gamewordTimer.ChangeOfSpeed(10);
      _gracz.GetComponent<PlayerMovement>().CanMove(false); 
      _boxCollider2D.enabled=false;
      _gracz.GetComponent<Rigidbody2D>().MovePosition(_sleepPoint.position);
      _gracz.GetComponentInChildren<latarka>().Lock();
      //grafika
       _gracz.GetComponent<SpriteRenderer>().enabled=false;
      m_normalSprite=GetComponentInParent<SpriteRenderer>().sprite;
      GetComponentInParent<SpriteRenderer>().sprite=m_sleepSprite;
    }
    public void TurnOFFInteract()
    {
      Debug.Log("Wstaje");
      _gamewordTimer.ChangeSpeedToNormal();
      _gracz.GetComponent<PlayerMovement>().CanMove(true);
      _boxCollider2D.enabled=true;
      _gracz.GetComponent<Rigidbody2D>().MovePosition(_wakeUpPoint.position);
      _gracz.GetComponentInChildren<latarka>().Unlock();
      //grafika
       _gracz.GetComponent<SpriteRenderer>().enabled=true;
      GetComponentInParent<SpriteRenderer>().sprite=m_normalSprite;

    }
}
