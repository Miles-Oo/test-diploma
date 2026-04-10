using System.Collections.Generic;
using UnityEngine;

public class paczka : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Przedmiot> _przedmiotList = new List<Przedmiot>();

    private void Start()
    {
    }

    public void SetItems(List<Przedmiot> items)
    {
        _przedmiotList = items;
    }

    public void Interact(GameObject gameObject, InteractorType interactor)
    {
        Recive();
        Destroy(this.gameObject);
    }

    private void Recive()
    {
        if (_przedmiotList == null || _przedmiotList.Count == 0)
            return;

        Core core = FindCoreFromInteractor();
        if (core == null)
        {
            return;
        }

        Inventory inv = core.GetInventory();

        for (int i = 0; i < _przedmiotList.Count; i++)
        {
            inv.AddPrzedmiot(_przedmiotList[i]);
        }
    }

    private Core FindCoreFromInteractor()
    {
        return FindObjectOfType<LodowkaCORE>(); //placeholder na teraz poki dodaje tylko do lodowki
    }
}