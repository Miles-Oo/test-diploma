using System;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(NpcMovement))]
[RequireComponent(typeof(NpcRotation))]
[RequireComponent(typeof(NpcSimpleTargetAI))]

public class NpcPathCalculatorAndFinder : MonoBehaviour
{
    private NpcMovement m_NpcMovement;
    public NpcMovement GetNpcMovement(){return m_NpcMovement;}
    private NpcRotation m_NpcRotation;
    public NpcRotation GetNpcRotation(){return m_NpcRotation;}
   private GameObject target;
   private NpcSimpleTargetAI m_NpcSimpleTargetAI;
   public NpcSimpleTargetAI GetNpcSimpleTargetAI(){return m_NpcSimpleTargetAI;}
    public void SetTarget(GameObject gameObject){target = gameObject;}
    BoxCollider2D _boxCollider2D;
    private Vector2 wantedpos;
    private bool needToGo=true;
    void Awake()
    {
        m_NpcMovement=GetComponent<NpcMovement>();
        m_NpcRotation=GetComponent<NpcRotation>();
        _boxCollider2D=GetComponentInChildren<BoxCollider2D>();
        m_NpcSimpleTargetAI=GetComponent<NpcSimpleTargetAI>();
        _boxCollider2D=GetComponentInChildren<BoxCollider2D>();
    }
    void Start()
    {
      
    }

private bool m_wspXGit=false;
private bool m_wspYGit=false;
public void ResetXYgit()
    {
        m_wspXGit=false;
        m_wspYGit=false;
    }
private Coroutine moveTo;

public void MoveToTarget()
{
    needToGo = true;
    ResetXYgit();
    if (moveTo != null)
        StopCoroutine(moveTo);

    moveTo = StartCoroutine(DosMovementos());
}

    IEnumerator DosMovementos()
    {

        while(needToGo){
        Vector2 s=new Vector2(0,0);
        wantedpos=target.transform.position;


        if(Math.Abs(wantedpos.x - m_NpcMovement.getCurrPos().x)>1){
        if (wantedpos.x >m_NpcMovement.getCurrPos().x)
        {
           s.x++;
        }
        else if(wantedpos.x <m_NpcMovement.getCurrPos().x)
        {
          s.x--;
        }
        else
        {
            s.x=0;
        }
        }else{
                m_wspXGit=true;
            }
        if(Math.Abs(wantedpos.y - m_NpcMovement.getCurrPos().y)>1){
        if (wantedpos.y >m_NpcMovement.getCurrPos().y)
        {
           s.y++;
        }
        else if(wantedpos.y <m_NpcMovement.getCurrPos().y)
        {
          s.y--;
        }
        else
        {
            s.y=0;
        }
            }
            else
            {
                m_wspYGit=true;
            }
        m_NpcMovement.moveFor(s);
            if (m_wspXGit && m_wspYGit)
            {
                KoniecTrasy();
            }
        yield return null;
                }

    }

public event Action OnTest;


public void KoniecTrasy()
    { 
         print("jestem");
        needToGo=false;
         OnTest?.Invoke();
    }


}
