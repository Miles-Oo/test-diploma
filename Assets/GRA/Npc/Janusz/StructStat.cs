using System;
public class StructStat
{

    public event Action OnStatChange; 
    private string m_StatName;
    private float m_CurrStat;
    private float m_MaxStat;

    //o ile ma się zmniejszyć w 1 ticku
    private float m_naturalDegradator;

    public StructStat(string nazwa,float maxStat,float naturalDegradator)
    {
        m_StatName=nazwa;
        m_MaxStat=maxStat;
        m_CurrStat=m_MaxStat;
        m_naturalDegradator=naturalDegradator;
    }
    public void DoNaturalDegradation()
    {
         if (m_CurrStat- m_naturalDegradator <=0){m_CurrStat=0;}
        else{m_CurrStat-=m_naturalDegradator;}
         OnStatChange?.Invoke();
    }
    public bool IsUnderNormal()
    {
       return m_CurrStat < m_MaxStat * 0.2;
    }

    public void addToCurrStat(int stat){
        if (m_CurrStat + stat > m_MaxStat){m_CurrStat=m_MaxStat;}
        else{m_CurrStat+=stat;}
        OnStatChange?.Invoke();}
    public void subToCurrStat(int stat){
        if (m_CurrStat- stat <=0){m_CurrStat=0;}
        else{m_CurrStat-=stat;}
        OnStatChange?.Invoke();}

    public float getCurrStat(){return m_CurrStat;}
    public float getMaxStat(){return m_MaxStat;}
    public string getStatName(){return m_StatName;}
}
