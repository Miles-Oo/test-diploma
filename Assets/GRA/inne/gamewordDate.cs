using UnityEngine;
using System.Collections;
using System;
public class gamewordDate : MonoBehaviour
{
    public event Action OnDateChange;

    private int m_days=0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void addDay()
    {
        m_days++;
        OnDateChange?.Invoke();
    }
    public int getDays()
    {
        return m_days;
    }
}
