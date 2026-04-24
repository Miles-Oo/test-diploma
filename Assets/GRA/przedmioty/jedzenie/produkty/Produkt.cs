using UnityEngine;

[CreateAssetMenu(menuName = "Przedmioty/Produkt")]
public class Produkt:Przedmiot{

       [SerializeField] private int m_energia;
       [SerializeField] private int m_glod;

    public int getEnergia(){return m_energia;}
    public int getGlod(){return m_glod;}


    public override string GetText()
    {
        int modified = GetModifiedHunger();
      string na="Nazwa: "+GetNazwa()+
                    "\nOpis: "+GetOpis()+
                    "\nEnergia: "+getEnergia()+
                    "\nGłód: "+GetModifiedHunger();
      return na;
    }

public override void UsePrzedmiot()
{
    var gracz = FindAnyObjectByType<LodowkaInvetory>()
        .GetCORE()
        .GetGracz();

    int hungerValue = m_glod;

    if (PlayerSkills.Instance != null)
        hungerValue = PlayerSkills.Instance.ModifyHungerGain(m_glod);

    gracz.GetComponent<energy>().addEnergy(m_energia);
    gracz.GetComponent<hunger>().addHunger(hungerValue);
        Debug.Log($"🍔 Zjedzono: {GetNazwa()} | Base hunger: {m_glod} | Final hunger: {hungerValue}");

    SubIloscWEQ(1);
}

    private int GetModifiedHunger()
    {
        if (PlayerSkills.Instance != null)
            return PlayerSkills.Instance.ModifyHungerGain(m_glod);

        return m_glod;
    }

}
