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
    [SerializeField] private float sleepSpeed;
    public void Interact(GameObject gameObject,InteractorType interactor)
    {
        switch (interactor)
        {
            case InteractorType.Gracz:
            InteractPlayer();
            break;
            case InteractorType.Npc:
            InteractNpc(gameObject);
            break;
        }
    }
    public void InteractPlayer()
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
    public void InteractNpc(GameObject gameObject)
    {
        gameObject.GetComponent<SomsiadAI>().ZnajdzStatystyke("energia").GetStat().addToCurrStat(100);
    }
    protected override void Awake()
    {
        base.Awake();
        if (sleepSpeed >1000)
        {
            sleepSpeed=10;
        }
    }
    public void TurnONInteract()
    {
      Debug.Log("Kładę się spać");
      _gamewordTimer.ChangeOfSpeed(sleepSpeed);
     
     PlayersDisabes();
 GetGracz().GetComponentInChildren<Latarka>().Lock();
        if (GetGracz().GetComponentInChildren<Latarka>().IsFlashlightOn())
        {
            GetGracz().GetComponentInChildren<Latarka>().turnOff();
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

         GetGracz().GetComponentInChildren<Latarka>().Unlock();
      GetGracz().GetComponent<Rigidbody2D>().position=_wakeUpPoint.transform.position;
     _boxCollider2D.enabled=true;
      //grafika
       GetGracz().GetComponent<SpriteRenderer>().enabled=true;
      GetComponentInParent<SpriteRenderer>().sprite=m_normalSprite;

    }
}