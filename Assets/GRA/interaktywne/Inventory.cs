using Unity.VisualScripting;
using UnityEngine;

public abstract class Inventory<T> : MonoBehaviour where T : Przedmiot
{
    [SerializeField] protected T[] _przedmioty;
    public T[] GetPrzedmioty(){return _przedmioty;}

    public void AddPrzedmiot(Przedmiot przedmiot)
    {
       if(przedmiot is T typed){
       typed.AddIloscWEQ(1);
       }
    }
}
