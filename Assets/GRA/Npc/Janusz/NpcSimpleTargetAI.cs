using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(NpcPathCalculatorAndFinder))]
public class NpcSimpleTargetAI : MonoBehaviour
{

 private NpcPathCalculatorAndFinder m_npcPathCalculatorAndFinder;
 public NpcPathCalculatorAndFinder GetNPCPCAF(){return m_npcPathCalculatorAndFinder;}

 private SomsiadAI m_SomsiadAI;
 public SomsiadAI GetSomsiadAI(){ return m_SomsiadAI;}

public void GoToTarget(GameObject gameObject21)
    {
        m_npcPathCalculatorAndFinder.SetTarget(gameObject21);
        m_npcPathCalculatorAndFinder.MoveToTarget();
    }

    void Awake()
    {
        m_npcPathCalculatorAndFinder=GetComponent<NpcPathCalculatorAndFinder>();
        m_SomsiadAI=GetComponent<SomsiadAI>();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
