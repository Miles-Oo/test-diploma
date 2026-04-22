using System.Collections.Generic;
using UnityEngine;

public class paczka : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Przedmiot> _przedmiotList = new List<Przedmiot>();

    // [Header("Inventory Targets")]
    // [SerializeField] private MonoBehaviour lodowkaTarget;
    // [SerializeField] private MonoBehaviour biblioteczkaTarget;
    // [SerializeField] private MonoBehaviour szafaTarget;

    private IInventoryTarget lodowka;
    private IInventoryTarget biblioteczka;
    // private IInventoryTarget szafa;

    // private void Awake()
    // {
    //     lodowka = lodowkaTarget as IInventoryTarget;
    //     biblioteczka = biblioteczkaTarget as IInventoryTarget;
    //     // szafa = szafaTarget as IInventoryTarget;
    // }

    public void SetItems(List<Przedmiot> items)
    {
        _przedmiotList = items;
    }

    public void SetTargets(IInventoryTarget lodowkaTarget, IInventoryTarget biblioteczkaTarget)
    {
        lodowka = lodowkaTarget;
        biblioteczka = biblioteczkaTarget;
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

        foreach (var item in _przedmiotList)
        {
            var target = GetTargetForItem(item);

            if (target == null)
            {
                Debug.LogWarning("Brak targetu dla itemu: " + item.GetNazwa());
                continue;
            }

            target.GetInventory().AddPrzedmiot(item);
        }
    }

    private IInventoryTarget GetTargetForItem(Przedmiot item)
    {
        switch (item.GetTyp())
        {
            case TypPrzedmiotu.Jedzenie:
                return lodowka;

            case TypPrzedmiotu.Ksiazka:
                return biblioteczka;

            // case TypPrzedmiotu.Ubranie:
            //     return szafa;

            default:
                return null;
        }
    }
}