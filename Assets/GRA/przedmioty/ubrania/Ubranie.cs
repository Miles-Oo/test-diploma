using UnityEngine;
using UnityEngine.InputSystem.Interactions;
[CreateAssetMenu(menuName = "Przedmioty/Ubranie")]
public class Ubranie : Przedmiot
{

    public override string GetText()
    {
        return "Nazwa: " + GetNazwa() +
               "\nOpis: " + GetOpis();

    }

    public override void UsePrzedmiot()
    {
        var gracz = GameObject.FindWithTag("Player");

        Debug.Log("Założono ubranie: " + GetNazwa());
    }
}