
using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
     public event Action OnTimeEnd;
     public event Action OnTimeChange;
    private float _TimeForJob;
    public float getTopTime(){return _TimeForJob;}
    public void SetTopTime(float time){
       _TimeForJob=time;
       DontMessUP();
       }
    private float currTime=0;
    private float end;
    public float GetCurrTime(){return currTime;}
    private Coroutine tick;
    public void CzasStart()
    {
     tick= StartCoroutine(Tick());
    }
    public void CzasStart(float time)
    {
        _TimeForJob=time;
        DontMessUP();
    }
    public void CzasStop()
    {
        StopCoroutine(tick);
    }
    void Awake()
    {
            DontMessUP();
    }
    private void DontMessUP()
    {
                if (_TimeForJob < 1)
        {
            _TimeForJob=1f;
        }
    }
    IEnumerator Tick(){
         end=Time.time+_TimeForJob;
        while(Time.time<end){
            currTime=end-Time.time;
            OnTimeChange?.Invoke();
            yield return null;
        }
        OnTimeEnd?.Invoke();
       
    }
    public void CzasReset()
    {
       end=Time.time+_TimeForJob;
        OnTimeChange?.Invoke();
    }
}
