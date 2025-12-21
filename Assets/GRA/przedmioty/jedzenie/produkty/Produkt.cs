using UnityEngine;

[CreateAssetMenu(fileName = "Produkt", menuName = "Scriptable Objects/Produkt")]
public class Produkt:ScriptableObject{

       [SerializeField] private int m_energia;
       [SerializeField] private int m_glod;
       [SerializeField] string m_nazwa;
       [SerializeField] string m_opis;
       [SerializeField] private int m_ileMaGracz;
       [SerializeField] private Sprite m_spriteHighLight;
       [SerializeField] private Sprite m_spriteNormal;

    public int getEnergia(){return m_energia;}
    public int getGlod(){return m_glod;}
    public string getNazwa(){return m_nazwa;}
    public string getOpis(){return m_opis;}
    public int getIleMaGracz(){return m_ileMaGracz;}
    public void setIleMaGracz(int ilosc){m_ileMaGracz=ilosc;}
    public void addIleMaGracz(int ilosc){m_ileMaGracz+=ilosc;}
    public void subIleMaGracz(int ilosc){m_ileMaGracz-=ilosc;}
    public Sprite getSpriteNormal(){return m_spriteNormal;}
    public Sprite getSpriteHighLight(){return m_spriteHighLight;}

}
