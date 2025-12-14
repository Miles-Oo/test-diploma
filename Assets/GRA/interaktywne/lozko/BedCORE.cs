using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class BedTAG : MonoBehaviour, IInteractable
{
    [SerializeField] gamewordTimer _gamewordTimer;
    [SerializeField] GameObject _gracz;
    [SerializeField] Transform _sleepPoint;
    [SerializeField] Transform _wakeUpPoint;
    [SerializeField] private BoxCollider2D _boxCollider2D;

    public void Start()
    {
        //_boxCollider2D=GetComponent<BoxCollider2D>();
    }

    public void TurnONInteract()
    {
        Debug.Log("Kładę się spać");
        _gamewordTimer.ChangeOfSpeed(10);
        _gracz.GetComponent<PlayerMovement>().CanMove(false); 
        _boxCollider2D.enabled=false;
        _gracz.GetComponent<Rigidbody2D>().MovePosition(_sleepPoint.position);
       
    }
    public void TurnOFFInteract()
    {
         Debug.Log("Wstaje");
        _gamewordTimer.ChangeSpeedToNormal();
      _gracz.GetComponent<PlayerMovement>().CanMove(true);
               _boxCollider2D.enabled=true;
       _gracz.GetComponent<Rigidbody2D>().MovePosition(_wakeUpPoint.position);
     //  _gracz.GetComponent<Transform>().position=_wakeUpPoint.position;

    }
}
