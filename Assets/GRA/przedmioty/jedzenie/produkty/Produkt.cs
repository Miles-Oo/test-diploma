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
                    "\nEnergia: "+GetModifiedEnergy()+
                    "\nGłód: "+GetModifiedHunger();
      return na;
    }

public override void UsePrzedmiot()
{
    var gracz = FindAnyObjectByType<LodowkaInvetory>()
        .GetCORE()
        .GetGracz();

    int energyValue = GetModifiedEnergy();
    int hungerValue = m_glod;

    if (PlayerSkills.Instance != null)
        hungerValue = PlayerSkills.Instance.ModifyHungerGain(m_glod);

    gracz.GetComponent<energy>().addEnergy(energyValue);
    gracz.GetComponent<hunger>().addHunger(hungerValue);

    SubIloscWEQ(1);
}

    private int GetModifiedHunger()
    {
        if (PlayerSkills.Instance != null)
            return PlayerSkills.Instance.ModifyHungerGain(m_glod);

        return m_glod;
    }

        private int GetModifiedEnergy()
    {
        if (PlayerSkills.Instance != null)
            return PlayerSkills.Instance.ModifyEnergyGain(m_energia);

        return m_energia;
    }

}
