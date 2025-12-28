using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BedCORE : Core, IInteractable
{
    [SerializeField] gamewordTimer _gamewordTimer;
    [SerializeField] Transform _sleepPoint;
    [SerializeField] Transform _wakeUpPoint;

    //boxColider łóżka, ten od kolizji, na rodzicu 
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Sprite m_sleepSprite;
    private Sprite m_normalSprite;
    public override Inventory GetInventory()
    {
       return null;
    }
    public void Interact()
    {
        if (IsInteractja()){
            TurnOFFInteract();
          SetJestInterakcja(false);
        }
        else{
             TurnONInteract();
            SetJestInterakcja(true);
        }
    }
    public void TurnONInteract()
    {
      Debug.Log("Kładę się spać");
      _gamewordTimer.ChangeOfSpeed(10);
     
     PlayersDisabes();
      _boxCollider2D.enabled=false;
      GetGracz().GetComponent<Rigidbody2D>().MovePosition(_sleepPoint.position);
      //tak, trzeba to tak skomplikować inaczej odpala audio i tak jak gracz ma wył. latarkę
      //grafika
       GetGracz().GetComponent<SpriteRenderer>().enabled=false;
      m_normalSprite=GetComponentInParent<SpriteRenderer>().sprite;
      GetComponentInParent<SpriteRenderer>().sprite=m_sleepSprite;
    }
    public void TurnOFFInteract()
    {
      Debug.Log("Wstaje");
      _gamewordTimer.ChangeSpeedToNormal();
     PlayersEnabes();
      _boxCollider2D.enabled=true;
      GetGracz().GetComponent<Rigidbody2D>().MovePosition(_wakeUpPoint.position);
     
      //grafika
       GetGracz().GetComponent<SpriteRenderer>().enabled=true;
      GetComponentInParent<SpriteRenderer>().sprite=m_normalSprite;

    }
}
