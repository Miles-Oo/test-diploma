using UnityEngine;
using System.Collections;
using System;

public class gamewordTimer : MonoBehaviour
{

    public event Action OnTimeChange;
    private int m_currentSeconds=0;
    private int m_SecondsInDay=60;
    [SerializeField] gamewordDate _gamewordData;
    void Start()
    {
        StartCoroutine(Tick());
    }

    IEnumerator Tick(){
        while(true){
            m_currentSeconds++;
            
        if (!(m_currentSeconds < m_SecondsInDay))
        {
            m_currentSeconds=0;
            _gamewordData.addDay();
        }
        OnTimeChange?.Invoke();
            yield return new WaitForSeconds(1f);
        }
    }
    public int getCurrentSeconds()
    {
        return m_currentSeconds;
    }
    public int getSecondsInOneDay()
    {
        return m_SecondsInDay;
    }
}
