using UnityEngine;
public class LodowkaInvetory : Inventory
{
   [SerializeField] private LodowkaCORE _lodowkaCORE;
       public LodowkaCORE GetCORE(){return _lodowkaCORE;}
    [SerializeField] private Menu _lodowkaMenu;
    public Menu GetMenu(){return _lodowkaMenu;}
}
