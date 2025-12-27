using UnityEngine;
public class LodowkaInvetory : Inventory<Produkt>
{
   [SerializeField] private LodowkaCORE _lodowkaCORE;
       public LodowkaCORE GetLodowkaCORE(){return _lodowkaCORE;}
    [SerializeField] private Menu _lodowkaMenu;
    public Menu GetLodowkaMenu(){return _lodowkaMenu;}
}
