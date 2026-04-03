using System.Collections.Generic;
using UnityEngine;

public class paczka : MonoBehaviour,IInteractable
{

    // wywoływane, jak gracz na stronie zamówi to tworzy nową paczkę oraz jej zawartość, wraz z miejscem do którego ma dodać rzeczy jak gracz użyje interakcji.
   //Zamienic na interfejs inventory
   //Przerobić inventory aby był to hashset czy coś gdzie da się drugą rzecz jako ilosc.
   [SerializeField] Core _miejsceDodania;
   [SerializeField] List<Przedmiot> _przedmiotList=new List<Przedmiot>();
   void Start()
    {
    
    }
    
    public void Interact(GameObject gameObject,InteractorType interactor)
    {
        Recive();
        Destroy(this.gameObject);
        //TODO 
        //jak to zrobić aby działało z różnymi IInteractable? jak gracz zamówi jedzenie to doda do lodówki, nowe skiny to doda skiny, książki to książki w bibliotece
    }


    public void SenderOne(Core miejsce, Przedmiot rzecz)
    {
        _miejsceDodania=miejsce;
        _przedmiotList.Add(rzecz);
    }
    public void SenderMulti(Core miejsce, List<Przedmiot> rzeczy)
    {
        _miejsceDodania=miejsce;
        for(int i = 0; i < rzeczy.Count; i++)
        {
             _przedmiotList.Add(rzeczy[i]);
        }
    }
    private void Recive()
    {
        if(_przedmiotList==null) return;
        if(_przedmiotList.Count==0) return;
        for(int i = 0; i < _przedmiotList.Count; i++)
        {
             _miejsceDodania.GetInventory().AddPrzedmiot(_przedmiotList[i]);
        }
         
    }
}
