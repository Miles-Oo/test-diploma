using System.Linq;
using UnityEngine;

public class OwnLayerVertical : MonoBehaviour
{
   [SerializeField] private GameObject[] _listaObiektow;
   [SerializeField] private Vector2 _pozycjaStartowa;
   [SerializeField] private Vector2 _odstęp;

    void Start(){
        for(int i =0;i<_listaObiektow.Count();i++){
        _listaObiektow[i].GetComponent<RectTransform>().anchoredPosition= new Vector2(_pozycjaStartowa.x,_pozycjaStartowa.y+(i*_odstęp.y));
        }
    }
    public void Tasowanie()
    {
        Shuffle(_listaObiektow);
        
        for(int i =0;i<_listaObiektow.Count();i++){
        _listaObiektow[i].GetComponent<RectTransform>().anchoredPosition= new Vector2(_pozycjaStartowa.x,_pozycjaStartowa.y+(i*_odstęp.y));
        }
    }
public static void Shuffle( GameObject[] list)
{
    System.Random rng = new System.Random();
    int n = list.Count();
    while (n > 1)
    {
        n--;
        int k = rng.Next(n + 1);
        GameObject value = list[k];
        list[k] = list[n];
        list[n] = value;
    }
}
   
       
    void Update(){ 
        #if UNITY_EDITOR
                for(int i =0;i<_listaObiektow.Count();i++){
        _listaObiektow[i].GetComponent<RectTransform>().anchoredPosition= new Vector2(_pozycjaStartowa.x,_pozycjaStartowa.y+(i*_odstęp.y));
        }
        #endif
    } 

}
