using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private DeliveryManager deliveryManager;
    // [SerializeField] private List<OrderSlotUI> slots;
    [SerializeField] private Transform slotContainer;
    [SerializeField] private OrderUI orderUI;

    public void OnOrderButtonClicked()
    {
        if (deliveryManager == null || slotContainer == null)
            return;

        OrderData order = new OrderData();
        var slots = slotContainer.GetComponentsInChildren<OrderSlotUI>();

        foreach (var slot in slots)
        {
            if (slot == null)
            {
                continue;
            }

            int amount = slot.GetAmount();
            Przedmiot item = slot.GetItem();

            if (amount <= 0)
                continue;

            for (int i = 0; i < amount; i++)
            {
                order.items.Add(item);
            }
        }

        if (order.items.Count == 0)
        {
            return;
        }

        deliveryManager.SpawnCourier(order);

        foreach (var slot in slots)
        {
            slot.ResetAmount();
        }

        if (orderUI != null)
        {
            orderUI.Close();
        }
    }
}