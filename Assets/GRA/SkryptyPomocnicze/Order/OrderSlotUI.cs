using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OrderSlotUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text amountText;
    private Przedmiot item;
    private int amount = 0;

    public void Setup(Przedmiot newItem)
    {
        item = newItem;

        if (itemImage != null)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.enabled = true;
            itemImage.sprite = item.GetSpriteNormal();
        }

        if (amountText != null)
        {
            amountText.gameObject.SetActive(true);
            amountText.enabled = true;
        }

        amount = 0;
        UpdateText();
    }

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