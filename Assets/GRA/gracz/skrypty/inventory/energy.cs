using UnityEngine;
using System;

public class energy : MonoBehaviour
{
    private int m_currentEnergy=0;
    private int m_maxEnergy=100;
 public event Action OnMoneyChange;
    void Start()
    {
        m_currentEnergy=m_maxEnergy;
    }
    public void addEnergy(int energy)
    {
         m_currentEnergy+=energy;
              OnMoneyChange?.Invoke();
    }
    public void subEnergy(int energy)
    {
        m_currentEnergy-=energy;
        OnMoneyChange?.Invoke();
    }
    public int getCurrEnergy()
    {
        return m_currentEnergy;
    }
    public int getMaxEnergy()
    {
        return m_maxEnergy;
    }
}
