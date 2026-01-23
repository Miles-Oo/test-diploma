using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MiniGameKabelkiRULE : MonoBehaviour{  
    
      public event Action OnLicznikForCanvasChange;
      
    [SerializeField] private OwnLayerVertical lewa;  
    [SerializeField] private OwnLayerVertical prawa;
    [SerializeField] private KabelekCore[] _kabelki;
    private int m_currentLicznik;
    private int m_licznikKabli;
    public int GetCurrLicznik(){return m_currentLicznik;}
    public int GetLicznik(){return m_licznikKabli;}
    [SerializeField] private Timer _timer;
    private int m_Score;

    public int GetScore(){return m_Score;}
   

    public static readonly DiffConf[] m_DiffConf={new DiffConf(Difficulty.Low,0,3f,40,10),new DiffConf(Difficulty.Medium,3,1f,30,25),new DiffConf(Difficulty.High,7,0.6f,50,100),new DiffConf(Difficulty.Max,10,0.2f,100,300)};
    
    private DiffConf currDiffConf;
    public Difficulty GetDifficulty(){return currDiffConf.GetDifficulty();}
    void Start()
    {
        m_currentLicznik=0;
        m_licznikKabli=0;
        m_Score=0;
        currDiffConf=m_DiffConf[0];
         foreach(KabelekCore kabel in _kabelki)
        {
            m_licznikKabli++;
           kabel.GetRuchomyKabel().OnlicznikChange+=Adder;
        }
        _timer.OnTimeEnd+=OutOfTime;
        _timer.SetTopTime(currDiffConf.GetMaxCzas());
        OnLicznikForCanvasChange?.Invoke();
        UpdateRoles();
    //   ReBuild();
    }

    private void Adder(){
       
        m_currentLicznik++; 
        OnLicznikForCanvasChange?.Invoke();
        if (m_currentLicznik >= m_licznikKabli){

            AfterWin();
            ReBuild();
        }
    }
    private void AfterWin()
    {
        m_Score++;
       UpDiff();
       UpdateRoles();
        _timer.SetTopTime(currDiffConf.GetMaxCzas());
    }
    private void UpdateRoles()
    {
        foreach(KabelekCore kabel in _kabelki)
        {
            kabel.SetMaxPredkosc(currDiffConf.GetMaxPredkosc());
        }
    }
    private void OutOfTime()
    {            m_Score=0; 

            ReBuild();
           
             OnLicznikForCanvasChange?.Invoke();
    }



    public void ReBuild(){
        Canvas.ForceUpdateCanvases();
        lewa.Tasowanie();
        prawa.Tasowanie();
        Canvas.ForceUpdateCanvases();
        foreach(KabelekCore kabel in _kabelki)
        {
            kabel.NewReset();
        }
       m_currentLicznik=0;
       UpDiff();
        UpdateRoles();
        _timer.SetTopTime(currDiffConf.GetMaxCzas());
        OnLicznikForCanvasChange?.Invoke();
        _timer.CzasReset();
        _timer.CzasStart();
    }
    public void StopALL()
    {
        _timer.CzasReset();
        _timer.CzasStop();
        m_Score=0;
    }
       public enum Difficulty{Low,Medium,High,Max}
        public readonly struct DiffConf{
        private readonly Difficulty m_difficulty;
        private readonly float m_maxPredkosc;
        private readonly float m_maxCzas;
        private readonly int m_wartosc;
        private readonly int m_prog;
        
        public Difficulty GetDifficulty(){return m_difficulty;}
        public float GetMaxPredkosc(){return m_maxPredkosc;}
        public float GetMaxCzas(){return m_maxCzas;}
        public int GetWartosc(){return m_wartosc;}
        public int GetProg(){return m_prog;}
        public DiffConf(Difficulty diff,int prog,float maxPredkosc,float maxCzas, int wartosc)
        {
            m_difficulty=diff;
            m_maxPredkosc=maxPredkosc;
            m_maxCzas=maxCzas;
            m_wartosc=wartosc;
            m_prog=prog;
        }
    }
    
    public void UpDiff()
    {

        for(int i = m_DiffConf.Length-1; i >=0 ; i--)
        {
        if(m_Score>=m_DiffConf[i].GetProg())
            {
                currDiffConf=m_DiffConf[i];
                return;
            }
        }

    }
}