using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using System;

public class inventory:MonoBehaviour{
    private int m_zlotowki=0;
    private int m_krypto=0;

     public event Action OnGetMoney;

    public void Start()
    {
        OnGetMoney?.Invoke();
    }




    //ZLOTOWKI
    public void addZlotowki(int zlotowki)
    {
            m_zlotowki+=zlotowki;
              OnGetMoney?.Invoke();
    }
    public void subZlotowki(int zlotowki)
    {
        m_zlotowki-=zlotowki;
        OnGetMoney?.Invoke();
    }
    public int getZlotowki()
    {
        return m_zlotowki;
    }

    //KRYPTO
        public void addKrypto(int krypto)
    {
            m_krypto+=krypto;
              OnGetMoney?.Invoke();
    }
    public void subKrypto(int krypto)
    {
        m_krypto-=krypto;
        OnGetMoney?.Invoke();
    }
    public int getKrypto()
    {
        return m_krypto;
    }
}
