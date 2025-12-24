using UnityEngine;
public class LodowkaInvetory : Inventory<Produkt>
{
   [SerializeField] private LodowkaCORE _lodowkaCORE;
       public LodowkaCORE GetLodowkaCORE(){return _lodowkaCORE;}
    [SerializeField] private LodowkaMenu _lodowkaMenu;
    public LodowkaMenu GetLodowkaMenu(){return _lodowkaMenu;}
    [SerializeField] private Produkt[] _produkt;
    public Produkt[] GetProdukts(){return _produkt;}

    public Przedmiot[] GetItems()
    {return _produkt;}
}
