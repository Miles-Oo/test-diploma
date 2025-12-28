using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class paczka : MonoBehaviour,IInteractable
{

    // wywoływane, jak gracz na stronie zamówi to tworzy nową paczkę oraz jej zawartość, wraz z miejscem do którego ma dodać rzeczy jak gracz użyje interakcji.
   //Zamienic na interfejs inventory
   //Przerobić inventory aby był to hashset czy coś gdzie da się drugą rzecz jako ilosc.
   [SerializeField] Core _miejsceDodania;
   [SerializeField] Przedmiot _przedmiot;
   void Start()
    {
    }
    
    public void Interact()
    {
        Recive();
        Destroy(this.gameObject);
        //TODO 
        //jak to zrobić aby działało z różnymi IInteractable? jak gracz zamówi jedzenie to doda do lodówki, nowe skiny to doda skiny, książki to książki w bibliotece
    }


    public void Sender(Core miejsce, Przedmiot rzecz)
    {
        _miejsceDodania=miejsce;
        _przedmiot=rzecz;
    }
    private void Recive()
    {
          _miejsceDodania.GetInventory().AddPrzedmiot(_przedmiot);
    }
}
