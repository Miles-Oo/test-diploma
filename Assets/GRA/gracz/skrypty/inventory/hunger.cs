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
         OnHungerChange?.Invoke();
    }
    //currHunger
    public void addHunger(int hunger){
        if (m_currentHunger + hunger > m_maxHunger){
              m_currentHunger=m_maxHunger;
        }
        else{
             m_currentHunger+=hunger;
        }
              OnHungerChange?.Invoke();
    }
    public void subHunger(int hunger){

        if (m_currentHunger -hunger <= 0){
              m_currentHunger=0;
        }
        else{
              m_currentHunger-=hunger;
        }
      
        OnHungerChange?.Invoke();
    }
    public int getCurrHunger(){return m_currentHunger;}

    //maxHunger

    public void addMaxHunder(int maxhun){
        m_maxHunger+=maxhun;
            OnHungerChange?.Invoke();
    }    
    public void subMaxHunder(int maxhun){
        m_maxHunger-=maxhun;
            OnHungerChange?.Invoke();
    }
     public int getMaxHunger(){return m_maxHunger;}
}
