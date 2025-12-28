using UnityEngine;
public class LodowkaInvetory : Inventory
{
   [SerializeField] private LodowkaCORE _lodowkaCORE;
       public LodowkaCORE GetLodowkaCORE(){return _lodowkaCORE;}
    [SerializeField] private Menu _lodowkaMenu;
    public Menu GetLodowkaMenu(){return _lodowkaMenu;}
}
