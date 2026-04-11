using UnityEngine;
using UnityEngine.Rendering.Universal;
using System;
public class SunSimulation : MonoBehaviour
{
    [SerializeField] gamewordTimer _time;
    public event Action OnSunChange;
    private Light2D _sun;
    private float _dlugoscDoby;

    void Start()
    {
        _time.OnTimeChange += ChangePosition;
        _sun=GetComponent<Light2D>();
        ChangePosition();
        _dlugoscDoby=_time.getSecondsInOneDay();
    }

    public int godzinaNaSekundy(int godzina)
    {
        //zwraca daną godzinę w sekundach(w czasie jako długość doby)
        return (int)(godzina*_dlugoscDoby)/24;
    }


        void ChangePosition()
    {   
        //na sztywno ustawia noc od 22 do 6
        //na sztywno ustawia dzień od 10 do 18
        //wygładza przejścice z nocy do dnia 
        //wygładza przejście z dnia do nocy

        //noc
        if (_time.getCurrentSeconds() < godzinaNaSekundy(6) || _time.getCurrentSeconds() > godzinaNaSekundy(22))
        {
            _sun.intensity=0;
        }
        //rośnie
        else if (_time.getCurrentSeconds() > godzinaNaSekundy(6) && _time.getCurrentSeconds() < godzinaNaSekundy(10))
        {
            _sun.intensity=1f/Math.Abs(godzinaNaSekundy(6)-godzinaNaSekundy(10))*((float)_time.getCurrentSeconds()-godzinaNaSekundy(6));       
        }
        //dzień
        else if (_time.getCurrentSeconds() > godzinaNaSekundy(10) && _time.getCurrentSeconds() < godzinaNaSekundy(18))
        {
            _sun.intensity=1;
        }
        //spada
        else if (_time.getCurrentSeconds() > godzinaNaSekundy(18) && _time.getCurrentSeconds() < godzinaNaSekundy(22))
        {
            _sun.intensity=1f/Math.Abs(godzinaNaSekundy(18)-godzinaNaSekundy(22))*(godzinaNaSekundy(22)-(float)_time.getCurrentSeconds());       

        }
        
      OnSunChange?.Invoke();
    }
}
