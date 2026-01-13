
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
[CreateAssetMenu(menuName = "Przedmioty/Ksiazka")]
public class Ksiazka : Przedmiot
{
  [TextArea] private string _zawartoscKsiazki;
  [SerializeField] private TextAsset _fileZTrescia;
  public string GetZawartoscKsiazki()
    {
        return _zawartoscKsiazki;
    }
  public void LoadFileToString()
    {
      if(_fileZTrescia!=null){
      _zawartoscKsiazki=_fileZTrescia.text;
      Debug.Log("czytanie: ");
        Debug.Log(_zawartoscKsiazki);
        }
    }

    public override string GetText()
    {
      string na="Nazwa: "+GetNazwa()+
                    "\nOpis: "+GetOpis()+
                    "\nEnergia: "+GetZawartoscKsiazki();
      return na;
    }

    public override void UsePrzedmiot()
    {
        throw new System.NotImplementedException();
    }
}
