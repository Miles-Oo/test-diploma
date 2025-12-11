using UnityEngine;
using System;
public class hunger : MonoBehaviour
{
 private int m_currentHunger=0;
    private int m_maxHunger=100;
 public event Action OnHungerChange;
    void Start()
    {
        m_currentHunger=m_maxHunger;
    }
    public void addHunger(int hunger)
    {
         m_currentHunger+=hunger;
              OnHungerChange?.Invoke();
    }
    public void subHunger(int hunger)
    {
        m_currentHunger-=hunger;
        OnHungerChange?.Invoke();
    }
    public int getCurrHunger()
    {
        return m_currentHunger;
    }
    public int getMaxHunger()
    {
        return m_maxHunger;
    }
}
