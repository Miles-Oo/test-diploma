using UnityEngine;
using System.Collections;

public class MegaTest : MonoBehaviour
{
    [SerializeField] hunger _hunger;
    [SerializeField] energy _energy;

    [SerializeField] leveling _leveling;
    [SerializeField] wallet _wallet; 

    void Start()
    {
        StartCoroutine(Tick());
    }

    IEnumerator Tick()
    {
        while (true)
        {
            _leveling.addExp(10);
            _energy.subEnergy(3);
            _hunger.subHunger(3);
            _wallet.addZlotowki(1);
            _wallet.addKrypto(1);
            yield return new WaitForSeconds(1f);
        }
    }
}
