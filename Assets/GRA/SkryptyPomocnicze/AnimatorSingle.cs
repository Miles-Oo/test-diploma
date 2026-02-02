using System.Collections;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class AnimatorSingle : MonoBehaviour
{
    [SerializeField] private Sprite spriteSheetNormal;
    private SpriteRenderer m_SpriteRenderer;
    private int m_sizeOfSpriteSheet;
    private Sprite[] m_AnimacjaCurrent;
    [SerializeField] float m_changeSpriteSpeed =0.06f;
    private int m_cFrame=0;
    private bool m_haveNextFrame=true;

    //wyrzucic po testach serializefield 
    [SerializeField] private int m_ileRazy=0;
    void Awake(){
        m_SpriteRenderer=GetComponent<SpriteRenderer>();
        m_sizeOfSpriteSheet=(int)spriteSheetNormal.rect.width/64;

        if(m_sizeOfSpriteSheet>-1){
            m_AnimacjaCurrent=new Sprite[m_sizeOfSpriteSheet];
        }else{
            m_AnimacjaCurrent=null;
            }
        //zabezpieczenie
        if (m_changeSpriteSpeed < 0.001f){m_changeSpriteSpeed=2f;}
        if (m_ileRazy < 1){m_ileRazy=1;}
    }
    void Start(){
        for(int i = 0; i < m_AnimacjaCurrent.Length; i++)
        {
            m_AnimacjaCurrent[i]=Sprite.Create(spriteSheetNormal.texture,new Rect(i*64,0,64,64),new Vector2(.5f,.5f),32);       
        }
        m_SpriteRenderer.sprite=m_AnimacjaCurrent[0];
        
        //wyzucic po testach
        // RunAnimation(m_ileRazy);
    }

    private bool m_AnimatorIsWorking=false;
    IEnumerator enumerator(int ileRazy)
    {
m_AnimatorIsWorking=true;
        for (int i =0;i<ileRazy;i++){ 
        m_haveNextFrame=true;
        while (m_haveNextFrame){
            AnimateNext();
       yield return new WaitForSeconds(m_changeSpriteSpeed);
        }
        }
        m_AnimatorIsWorking=false;
        m_SpriteRenderer.sprite=m_AnimacjaCurrent[0];
    }
    public void EnableSpriteRenderer(bool czyWlaczyc){m_SpriteRenderer.enabled=czyWlaczyc;}
   
   
    //używany zewnętrzne do uruchomienia animacji
     public void RunAnimation(int ileRazy){
        
        if(m_AnimatorIsWorking){return;}
        StartCoroutine(enumerator(ileRazy));
    }
    private void AnimateNext()
    {
        if(m_cFrame==m_AnimacjaCurrent.Length){m_haveNextFrame=false; m_cFrame=0;}
         m_SpriteRenderer.sprite=m_AnimacjaCurrent[m_cFrame];
         m_cFrame++;
    }
}
