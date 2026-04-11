using UnityEngine;
using TMPro;

public class OrderSlotUI : MonoBehaviour
{
    public TMP_Text amountText;

    [SerializeField] private Przedmiot item;

    private int amount = 0;

    public Przedmiot GetItem()
    {
        return item;
    }

    public int GetAmount()
    {
        return amount;
    }

    public void Increase()
    {
        amount++;
        Debug.Log("AMOUNT = " + amount);
        UpdateText();
    }

    public void Decrease()
    {
        if (amount > 0)
            amount--;

        UpdateText();
    }

    public void ResetAmount()
    {
        amount = 0;
        UpdateText();
    }

    private void UpdateText()
    {
        amountText.text = amount.ToString();
    }
}