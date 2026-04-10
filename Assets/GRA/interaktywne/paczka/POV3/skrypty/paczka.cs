using System.Collections.Generic;
using UnityEngine;

public class paczka : MonoBehaviour, IInteractable

{
    // wywoływane, jak gracz na stronie zamówi to tworzy nową paczkę oraz jej zawartość, wraz z miejscem do którego ma dodać rzeczy jak gracz użyje interakcji.
   //Zamienic na interfejs inventory
   //Przerobić inventory aby był to hashset czy coś gdzie da się drugą rzecz jako ilosc.
    private IInventoryTarget _miejsceDodania;

    [SerializeField] private List<Przedmiot> _przedmiotList = new List<Przedmiot>();

    public void SetMiejsce(IInventoryTarget target)
    {
        _miejsceDodania = target;
    }

    public void Interact(GameObject gameObject, InteractorType interactor)
    {
        Recive();
        Destroy(this.gameObject);
        //TODO 
        //jak to zrobić aby działało z różnymi IInteractable? jak gracz zamówi jedzenie to doda do lodówki, nowe skiny to doda skiny, książki to książki w bibliotece
    }

    private void Recive()
    {
        if (_przedmiotList == null || _przedmiotList.Count == 0)
            return;

        if (_miejsceDodania == null)
        {
            Debug.LogError("Brak targetu inventory w paczce!");
            return;
        }

        Inventory inv = _miejsceDodania.GetInventory();

        for (int i = 0; i < _przedmiotList.Count; i++)
        {
            inv.AddPrzedmiot(_przedmiotList[i]);
        }
    }
}