using UnityEngine;
public class BiblioteczkaInvetory : Inventory
{
   [SerializeField] private BiblioteczkaCORE __biblioteczkaCORE;
       public BiblioteczkaCORE GetCORE(){return __biblioteczkaCORE;}
    [SerializeField] private Menu _biblioteczkaMenu;
    public Menu GetMenu(){return _biblioteczkaMenu;}
}
