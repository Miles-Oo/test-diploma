using System;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;
[RequireComponent(typeof(NpcSimpleTargetAI))]
public class SomsiadAI : MonoBehaviour
{
    private NpcSimpleTargetAI m_simpleTargetAI;
    private bool nearTarget=false;

    private Statystyka[] m_ListaStatystyk;

    private List<Statystyka> m_ListaZadan=new List<Statystyka>();
    void Awake()
    {
        m_simpleTargetAI=GetComponent<NpcSimpleTargetAI>();

       // m_npcStats.OnEnergyChange+=CheckOnStats;

        m_ListaStatystyk=GetComponents<Statystyka>();


        for(int i = 0; i < m_ListaStatystyk.Length; i++)
        {
            m_ListaStatystyk[i].OnReportChange+=CheckOnStats;
        }
        //... 
        //...
        //inne modele
    }
    void Start()
    {
        
    }

    void Update()
    {
    }

    void CheckOnStats(Statystyka statystyka)
    {
        if(!m_ListaZadan.Contains(statystyka))
        print("dodaje do kolejki");
        m_ListaZadan.Add(statystyka);
    }
}
