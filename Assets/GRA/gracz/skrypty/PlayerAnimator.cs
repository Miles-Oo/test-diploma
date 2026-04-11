using System.Collections;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Sprite spriteSheetNormal;
    [SerializeField] private Sprite spriteSheetUseFlashLight;
    private SpriteRenderer m_SpriteRenderer;
    private int m_sizeOfSpriteSheet;
    private Sprite[] m_AnimacjaNormal;
    private Sprite[] m_AnimacjaLight;
    private Sprite[] m_AnimacjaCurrent;
    private PlayerMovement playerMovement;
    private Latarka latarka;
    [SerializeField] float m_changeSpriteSpeed =0.06f;
    void Awake()
    {
        m_SpriteRenderer=GetComponent<SpriteRenderer>();
        playerMovement=GetComponent<PlayerMovement>();
        latarka=GetComponentInChildren<Latarka>();
        m_sizeOfSpriteSheet=(int)spriteSheetNormal.rect.width/64;
        if(m_sizeOfSpriteSheet>-1){
            m_AnimacjaNormal=new Sprite[m_sizeOfSpriteSheet];
        }else{
            m_AnimacjaNormal=null;
            }
        if (m_sizeOfSpriteSheet > -1){
             m_AnimacjaLight=new Sprite[m_sizeOfSpriteSheet];
        }else{
            m_AnimacjaLight=null;
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
        latarkaon=latarka.IsFlashlightOn();
        latarka.OnFlashLightChange+=ChangeCurrAnimation;
        for(int i = 0; i < m_AnimacjaNormal.Length; i++)
        {
            m_AnimacjaNormal[i]=Sprite.Create(spriteSheetNormal.texture,new Rect(i*64,0,64,64),new Vector2(.5f,.5f),32);
       m_AnimacjaLight[i]=Sprite.Create(spriteSheetUseFlashLight.texture,new Rect(i*64,0,64,64),new Vector2(.5f,.5f),32);
       
        }
        m_AnimacjaCurrent=m_AnimacjaNormal;
        m_SpriteRenderer.sprite=m_AnimacjaCurrent[0];
        StartCoroutine(enumerator());
    }
private bool latarkaon=false;
private int j=0;
    IEnumerator enumerator()
    {

        while (true){

            if (playerMovement.IsMoving())
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

public void ChangeCurrAnimation()
    {latarkaon=latarka.IsFlashlightOn();
        if (latarkaon)
        {
             m_AnimacjaCurrent=m_AnimacjaLight;
        }
        else
        {
              m_AnimacjaCurrent=m_AnimacjaNormal;
        }
    }
private void AnimateNext()
    {
               if(j>=m_AnimacjaCurrent.Length){j=0;}
         m_SpriteRenderer.sprite=m_AnimacjaCurrent[j];
         j++;
    }
}
