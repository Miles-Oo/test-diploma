using UnityEngine;
public class BedCORE : Core, IInteractable
{
    [SerializeField] gamewordTimer _gamewordTimer;
    [SerializeField] GameObject _sleepPoint;
    [SerializeField] GameObject _wakeUpPoint;

    //boxColider łóżka, ten od kolizji, na rodzicu 
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Sprite m_sleepSprite;
    private Sprite m_normalSprite;
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
 GetGracz().GetComponentInChildren<latarka>().Lock();
        if (GetGracz().GetComponentInChildren<latarka>().IsFlashlightOn())
        {
            GetGracz().GetComponentInChildren<latarka>().turnOff();
        }

      _boxCollider2D.enabled=false;
      GetGracz().GetComponent<Rigidbody2D>().MovePosition(_sleepPoint.transform.position);
      print(_sleepPoint.transform.position);
      //tak, trzeba to tak skomplikować inaczej odpala audio i tak jak gracz ma wył. latarkę
      //grafika
       GetGracz().GetComponent<SpriteRenderer>().enabled=false;
      m_normalSprite=GetComponentInParent<SpriteRenderer>().sprite;
      GetComponentInParent<SpriteRenderer>().sprite=m_sleepSprite;
    }
    public void TurnOFFInteract()
    {      
            print(_wakeUpPoint.transform.position);

      Debug.Log("Wstaje");
      _gamewordTimer.ChangeSpeedToNormal();
     PlayersEnabes();

         GetGracz().GetComponentInChildren<latarka>().Unlock();
      GetGracz().GetComponent<Rigidbody2D>().position=_wakeUpPoint.transform.position;
     _boxCollider2D.enabled=true;
      //grafika
       GetGracz().GetComponent<SpriteRenderer>().enabled=true;
      GetComponentInParent<SpriteRenderer>().sprite=m_normalSprite;

    }
}
