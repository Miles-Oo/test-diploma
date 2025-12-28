using UnityEngine;

public abstract class Inventory : MonoBehaviour
{
    [SerializeField] protected Przedmiot[] _przedmioty;
    public Przedmiot[] GetPrzedmioty(){return _przedmioty;}

    public void AddPrzedmiot(Przedmiot przedmiot)
    {
       if(przedmiot is Przedmiot typed){
       typed.AddIloscWEQ(1);
       }
    }
}
