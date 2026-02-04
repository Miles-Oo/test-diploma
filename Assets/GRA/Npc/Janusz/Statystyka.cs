using UnityEngine;
using System;
public class Statystyka : MonoBehaviour
{
  [SerializeField] public StructStat structStat;
   public bool czyZgloszone;
   public GameObject miejsceUzupelnienia;
public event Action<Statystyka> OnReportChange; 
    [SerializeField] private string nazwa;
    [SerializeField] private float maxStat;
    [SerializeField] private float naturalDegradator;
    void Awake()
    {
        //zabezpieczenia
        if (nazwa == null){nazwa="Brak NAZWY!!!";print("Brak Nazwy w: "+this);}
        if (maxStat<1){maxStat=1;print("Niepoprawna wartość dla max Stat w: "+this);}
        if (naturalDegradator<0){naturalDegradator=1;print("użyta ujemna wartość dla naturalDegradator: "+this);}
        //

        structStat=new StructStat(nazwa,maxStat,naturalDegradator);
        czyZgloszone=false;
        structStat.OnStatChange+=Zglos;
    }
    public void Zglos()
    {
        if (structStat.IsUnderNormal()&&!czyZgloszone)
        {
            print("o nie! moje: "+structStat.getStatName()+" jest poniżej 20%");

            //użycie action dla dodania tego do stosu 
            OnReportChange?.Invoke(this);
            czyZgloszone=true;
        }
    }
}
