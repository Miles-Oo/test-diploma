using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(NpcSimpleTargetAI))]
public class SomsiadAI : MonoBehaviour
{
    private NpcSimpleTargetAI m_simpleTargetAI;
    private bool nearTarget=false;

    private List<Statystyka> m_ListaStatystyk=new List<Statystyka>();

    public List<Statystyka> m_ListaZadan=new List<Statystyka>();

    public List<Statystyka> GetListaStatystyk()
    {
        return m_ListaStatystyk;
    }
    public Statystyka ZnajdzStatystyke(string nazwa)
    {
        return m_ListaStatystyk.Find(x=>x.GetStat().getStatName()==nazwa);
    }
    public void UsunZadanie()
    {
        m_ListaZadan.RemoveAt(0);
    }
    void Awake()
    {
        m_simpleTargetAI=GetComponent<NpcSimpleTargetAI>();

       // m_npcStats.OnEnergyChange+=CheckOnStats;

        m_ListaStatystyk.AddRange( GetComponents<Statystyka>());
      

        foreach(Statystyka st in m_ListaStatystyk)
        {
           st.OnReportChange+=CheckOnStats;
        }
        //... 
        //...
        //inne modele
    }
    void Start()
    {
          m_simpleTargetAI.GetNPCPCAF().OnTest+=Tester;
    }

    void Update()
    {
    }
void Tester()
{
    var stat = m_ListaZadan[0];

    // stat.structStat.addToCurrStat(100);

    stat.GetMiejsceUzupelnienia().GetComponentInChildren<IInteractable>().Interact(gameObject,InteractorType.Npc);
        
    
    stat.czyZgloszone = false;

    m_ListaZadan.RemoveAt(0);
    wykonujeZadanie = false;

    MakeMeASandWitch(); // następne zadanie
}

    void CheckOnStats(Statystyka statystyka)
    {
        if(!m_ListaZadan.Contains(statystyka)){
        print("dodaje do kolejki");
        print("zadania przed: ");
        Wyswietl();
        m_ListaZadan.Add(statystyka);
        print("zadania po: ");
        Wyswietl();
        }
        MakeMeASandWitch();
    }   
    void Wyswietl()
    {
        for(int i = 0; i < m_ListaZadan.Count; i++)
        {
            print ("na "+i+" miejscu jest: "+m_ListaZadan[i].GetStat().getStatName());
        }
    }
void MakeMeASandWitch()
{
    if (m_ListaZadan.Count == 0) return;
    if (wykonujeZadanie) return;

    wykonujeZadanie = true;
    m_simpleTargetAI.GoToTarget(m_ListaZadan[0].GetMiejsceUzupelnienia());
}



    private bool wykonujeZadanie = false;

    public GameObject WhoInteract => gameObject;

    public InteractorType Type => InteractorType.Npc;

}
