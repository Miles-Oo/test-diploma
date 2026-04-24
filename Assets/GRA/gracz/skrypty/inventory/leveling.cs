using UnityEngine;
using System;
using Unity.VisualScripting;
public class leveling:MonoBehaviour{
    
    private int m_lvl=1;

    private int m_skillPoints=0;
    private int m_currentExp=0;
    private int m_nextLvlExpCap=100;
    public event Action OnExpChange;
    public event Action OnSkillPointsChanged;

    public void Start()
    {
        OnExpChange?.Invoke();
        OnSkillPointsChanged?.Invoke();
    }
    public void toNextLvl(){
        //TUTAJ ZAMIENIĆ NA PLIK Z ZOGIKĄ NEXT LVL'i
        if (m_currentExp >= m_nextLvlExpCap * m_lvl)
        {
            m_currentExp-=m_nextLvlExpCap*m_lvl;
            m_lvl++;
            m_skillPoints+=1;
            OnSkillPointsChanged?.Invoke();
        }
    }
    //potem wywalić update, nie jest on do niczego potrzebny
    //START___TYLKO DO TESTÓW
    public void Update()
    {
       // addExp(1);
    }
     //END___TYLKO DO TESTÓW
    public void addExp(int exp){
        if (exp > 0){
         m_currentExp+=exp;
        }
        toNextLvl();
        OnExpChange?.Invoke();
    }
    
    public bool SpendSkillPoint(int amount)
    {
        if (m_skillPoints < amount)
            return false;

        m_skillPoints -= amount;
        OnSkillPointsChanged?.Invoke();
        return true;
    }
    public int getLvl(){return m_lvl;}
    public int getSkillPoints(){return m_skillPoints;}
    public int getCurrentExp(){return m_currentExp;}
    public int getNextLvlExpCap(){return m_nextLvlExpCap*m_lvl;}


}
