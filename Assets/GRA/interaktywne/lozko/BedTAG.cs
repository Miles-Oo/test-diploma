using UnityEngine;

public class BedTAG : MonoBehaviour, IInteractable
{
    public void TurnONInteract()
    {
        Debug.Log("Kładę się spać");
    }
    public void TurnOFFInteract()
    {
        Debug.Log("Wstaje");
    }
}
