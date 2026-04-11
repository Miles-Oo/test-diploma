using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory : MonoBehaviour
{
    [SerializeField] protected List<Przedmiot> _przedmioty;
    public List<Przedmiot>  GetPrzedmioty(){return _przedmioty;}

    public void AddPrzedmiot(Przedmiot przedmiot)
    {
       if(przedmiot is Przedmiot typed){
       typed.AddIloscWEQ(1);
       }
    }
}
