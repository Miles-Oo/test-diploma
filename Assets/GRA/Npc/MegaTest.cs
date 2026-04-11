using UnityEngine;
using System.Collections;

public class MegaTestNPCSTATSSIMULATOR : MonoBehaviour
{
    [SerializeField] Statystyka[] statystyki;

    void Awake()
    {
    }
    void Start()
    {
        StartCoroutine(Tick());
    }

    IEnumerator Tick()
    {
        while (true)
        {
            foreach(Statystyka sd in statystyki)
            {
                 sd.GetStat().DoNaturalDegradation();
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
