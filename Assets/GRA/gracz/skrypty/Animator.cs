using System.Collections;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Sprite))]

public class Animator : MonoBehaviour
{
    [SerializeField] private Sprite spriteSheetNormal;
        [SerializeField] private Sprite spriteSheetUseFlashLight;

    private SpriteRenderer m_SpriteRenderer;
    private int m_sizeOfSpriteSheet;
    private Sprite[] m_AnimacjaNormal;
    private Sprite[] m_AnimacjaLight;
    private Sprite[] m_AnimacjaCurrent;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Latarka latarka;
    void Awake()
    {
        m_SpriteRenderer=GetComponent<SpriteRenderer>();
        m_sizeOfSpriteSheet=(int)spriteSheetNormal.rect.width/64;
        timeAnimChange=10/m_sizeOfSpriteSheet;
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
    }

    void Start()
    {
        latarkaon=latarka.IsFlashlightOn();
        latarka.OnFlashLightChange+=ChangeCurrAnimation;
        for(int i = 0; i < m_AnimacjaNormal.Count(); i++)
        {
            m_AnimacjaNormal[i]=Sprite.Create(spriteSheetNormal.texture,new Rect(i*64,0,64,64),new Vector2(.5f,.5f),32);
       m_AnimacjaLight[i]=Sprite.Create(spriteSheetUseFlashLight.texture,new Rect(i*64,0,64,64),new Vector2(.5f,.5f),32);
       
        }
        m_AnimacjaCurrent=m_AnimacjaNormal;
        m_SpriteRenderer.sprite=m_AnimacjaCurrent[0];
        StartCoroutine(enumerator());
    }


    // Update is called once per frame



private bool latarkaon=false;
private int j=0;
private float timeAnimChange=1;
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
       yield return new WaitForSeconds(0.06f);
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
               if(j>=m_AnimacjaCurrent.Count()){j=0;}
         m_SpriteRenderer.sprite=m_AnimacjaCurrent[j];
         j++;
    }
    void Update()
    {
       
    }
}
