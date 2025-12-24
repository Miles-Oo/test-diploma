using System.IO;
using UnityEngine;
[CreateAssetMenu(menuName = "Przedmioty/Ksiazka")]
public class Ksiazka : Przedmiot
{
  [TextArea] private string _zawartoscKsiazki;
  public string getZawartoscKsiazki()
    {
        return _zawartoscKsiazki;
    }

           public override string GetText()
    {
      string na="Nazwa: "+GetNazwa()+
                    "\nOpis: "+GetOpis()+
                    "\nEnergia: "+getZawartoscKsiazki();
      return na;
    }

    public override void UsePrzedmiot()
    {
        throw new System.NotImplementedException();
    }
}
