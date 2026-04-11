using UnityEngine;

public abstract class Przedmiot : ScriptableObject
{
        [SerializeField] string m_nazwa;
       [SerializeField] string m_opis;
       [SerializeField] private Sprite m_spriteHighLight;
       [SerializeField] private Sprite m_spriteNormal;

        [SerializeField] private int m_iloscWEQ;
    public string GetNazwa(){return m_nazwa;}
    public string GetOpis(){return m_opis;}
    public Sprite GetSpriteNormal(){return m_spriteNormal;}
    public Sprite GetSpriteHighLight(){return m_spriteHighLight;}

    public int GetIloscWEQ(){return m_iloscWEQ;}
    public void SetIloscWEQ(int ilosc){m_iloscWEQ=ilosc;}
    public void AddIloscWEQ(int ilosc){m_iloscWEQ+=ilosc;}
    public void SubIloscWEQ(int ilosc){m_iloscWEQ-=ilosc;}

    public abstract string GetText();
    public abstract void UsePrzedmiot();
}
