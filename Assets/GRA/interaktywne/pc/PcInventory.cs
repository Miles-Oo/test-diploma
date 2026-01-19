using UnityEngine;
public class PcInventory : Inventory
{
   [SerializeField] private PcCORE _pcCORE;
       public PcCORE GetCORE(){return _pcCORE;}
    [SerializeField] private Menu _pcMenu;
    public Menu GetMenu(){return _pcMenu;}
}
