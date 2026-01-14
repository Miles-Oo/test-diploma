using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Rendering;

public class gamewordTimer : MonoBehaviour
{
    public event Action OnTimeChange;
    [SerializeField] private int m_currentSeconds=40000;
    private int m_SecondsInDay=86400;
    [SerializeField] gamewordDate _gamewordData;

    private int m_howMuchAddSeconds=5;
    private float m_speedOfTime;
    private float m_normalSpeedOfTime=1;

    public void ChangeOfSpeed(float speed)
    {
        m_speedOfTime=1f/speed;
    }
    public void ChangeSpeedToNormal()
    {
        m_speedOfTime=m_normalSpeedOfTime;
    }
    void Start()
    {
        m_speedOfTime=m_normalSpeedOfTime;
        StartCoroutine(Tick());
    }

    IEnumerator Tick(){
        while(true){
            m_currentSeconds+=m_howMuchAddSeconds;
            
        if (!(m_currentSeconds < m_SecondsInDay))
        {
            m_currentSeconds=0;
            _gamewordData.addDay();
        }
        OnTimeChange?.Invoke();
            yield return new WaitForSeconds(m_speedOfTime);
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
