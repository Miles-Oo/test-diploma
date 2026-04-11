using System.Collections;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(NpcMovement))]

public class NpcAnimator : MonoBehaviour
{
    private NpcMovement npcMovement;
    [SerializeField] private Sprite spriteSheetNormal;
    private SpriteRenderer m_SpriteRenderer;
    private int m_sizeOfSpriteSheet;
    private Sprite[] m_AnimacjaNormal;
    private Sprite[] m_AnimacjaCurrent;
    [SerializeField] float m_changeSpriteSpeed =0.06f;
    private bool m_animacjaCanRun=true;
    public void SetAniamcjaRun(bool animacjaRun){m_animacjaCanRun=animacjaRun;}
    public bool IsAnimacja(){return m_animacjaCanRun;}
    void Awake()
    {
        m_SpriteRenderer=GetComponent<SpriteRenderer>();
        npcMovement=GetComponent<NpcMovement>();
        m_sizeOfSpriteSheet=(int)spriteSheetNormal.rect.width/64;
        if(m_sizeOfSpriteSheet>-1){
            m_AnimacjaNormal=new Sprite[m_sizeOfSpriteSheet];
        }else{
            m_AnimacjaNormal=null;
            }
        if (m_changeSpriteSpeed < 0.001f)
        {
            m_changeSpriteSpeed=2f;
        }
    }
public void EnableSpriteRenderer(bool czyWlaczyc)
    {
        if (czyWlaczyc)
        {
            m_SpriteRenderer.enabled=true;
        }
        else
        {
            m_SpriteRenderer.enabled=false;
        }
    }
    void Start()
    {
        for(int i = 0; i < m_AnimacjaNormal.Length; i++)
        {
            m_AnimacjaNormal[i]=Sprite.Create(spriteSheetNormal.texture,new Rect(i*64,0,64,64),new Vector2(.5f,.5f),32);       
        }
        m_AnimacjaCurrent=m_AnimacjaNormal;
        m_SpriteRenderer.sprite=m_AnimacjaCurrent[0];
        StartCoroutine(enumerator());
    }
private int j=0;
    IEnumerator enumerator()
    {
        while (true){

            if (npcMovement.IsMoving())
            {
                  AnimateNext();
            }
            else
            {
                m_SpriteRenderer.sprite=m_AnimacjaCurrent[0];
            }
       yield return new WaitForSeconds(m_changeSpriteSpeed);
        }
    }
private void AnimateNext()
    {
               if(j>=m_AnimacjaCurrent.Length){j=0;}
         m_SpriteRenderer.sprite=m_AnimacjaCurrent[j];
         j++;
    }
}
