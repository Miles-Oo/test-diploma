using UnityEngine;

[CreateAssetMenu(menuName = "Przedmioty/Produkt")]
public class Produkt:Przedmiot{

       [SerializeField] private int m_energia;
       [SerializeField] private int m_glod;

    public int getEnergia(){return m_energia;}
    public int getGlod(){return m_glod;}


    public override string GetText()
    {
      string na="Nazwa: "+GetNazwa()+
                    "\nOpis: "+GetOpis()+
                    "\nEnergia: "+getEnergia()+
                    "\nGłód: "+getGlod();;
      return na;
    }

public override void UsePrzedmiot()
{
    var gracz = FindAnyObjectByType<LodowkaInvetory>()
        .GetLodowkaCORE()
        .GetGracz();

    gracz.GetComponent<energy>().addEnergy(m_energia);
    gracz.GetComponent<hunger>().addHunger(m_glod);
    SubIloscWEQ(1);
}

}
