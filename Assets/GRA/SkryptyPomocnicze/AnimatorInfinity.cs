using System.Collections;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class AnimatorInfinity : MonoBehaviour
{
    [SerializeField] private Sprite spriteSheetNormal;
    private SpriteRenderer m_SpriteRenderer;
    private int m_sizeOfSpriteSheet;
    private Sprite[] m_AnimacjaCurrent;
    [SerializeField] float m_changeSpriteSpeed =0.06f;
    private int m_cFrame=0;
    private IEnumerator m_enumerator;
    private bool m_czyUruchomic=true;

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
        m_enumerator=enumerator();
    }
    void Start(){
        for(int i = 0; i < m_AnimacjaCurrent.Length; i++)
        {
            m_AnimacjaCurrent[i]=Sprite.Create(spriteSheetNormal.texture,new Rect(i*64,0,64,64),new Vector2(.5f,.5f),32);       
        }
        m_SpriteRenderer.sprite=m_AnimacjaCurrent[0];
        
        //wyzucic po testach
       //  RunAnimation(true);

         RunAnimation(false);
    }
    IEnumerator enumerator()
    {
                while (m_czyUruchomic){
            AnimateNext();
       yield return new WaitForSeconds(m_changeSpriteSpeed);
        }
        m_SpriteRenderer.sprite=m_AnimacjaCurrent[0];
    }
    public void EnableSpriteRenderer(bool czyWlaczyc){m_SpriteRenderer.enabled=czyWlaczyc;}
   
   
    //używany zewnętrzne do uruchomienia animacji
    private bool m_AnimatorIsWorking=false;
     public void RunAnimation(bool startCzyStop){

        if (startCzyStop)
        {
            if(m_AnimatorIsWorking){return;}
            else
            {
                StartCoroutine(m_enumerator);
                m_AnimatorIsWorking=true;
            }
              
        }
        else
        {
            m_cFrame=0;
            m_SpriteRenderer.sprite=m_AnimacjaCurrent[m_cFrame];
            StopCoroutine(m_enumerator);
            m_AnimatorIsWorking=false;
        }
      
        
    }
private void AnimateNext()
    {
               if(m_cFrame>=m_AnimacjaCurrent.Length){m_cFrame=0;}
         m_SpriteRenderer.sprite=m_AnimacjaCurrent[m_cFrame];
         m_cFrame++;
    }
}
