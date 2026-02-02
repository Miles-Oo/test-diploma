using UnityEngine;
using System;

public class energy : MonoBehaviour
{
    private int m_currentEnergy=0;
    private int m_maxEnergy=100;
 public event Action OnEnergyChange;
    void Start()
    {
        m_currentEnergy=m_maxEnergy;
         OnEnergyChange?.Invoke();
    }


    //currEnergy
    public void addEnergy(int energy){
        if (m_currentEnergy + energy > m_maxEnergy){
             m_currentEnergy=m_maxEnergy;
        }else if (m_currentEnergy + energy <=0)
        {
            m_currentEnergy=0;
        }
        else{
                m_currentEnergy+=energy;
        }
        OnEnergyChange?.Invoke();
    }
    public void subEnergy(int energy){


        if (m_currentEnergy- energy <=0){
             m_currentEnergy=0;
        }
        else{
                   m_currentEnergy-=energy;
        }

        OnEnergyChange?.Invoke();
    }
    public int getCurrEnergy(){return m_currentEnergy;}

    //maxEnergy
  
    public void addMaxEnergy(int maxenergy){
        m_maxEnergy+=maxenergy;
            OnEnergyChange?.Invoke();
    }
        public void subMaxEnergy(int maxenergy){
        m_maxEnergy-=maxenergy;
            OnEnergyChange?.Invoke();
    } 
     public int getMaxEnergy(){return m_maxEnergy;}
}
