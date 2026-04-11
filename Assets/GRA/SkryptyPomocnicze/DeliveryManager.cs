using System.Collections;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public GameObject courierPrefab;
    public Transform spawnPoint;
    public Transform doorPoint;

    private bool isOrderActive = false;

    public void SpawnCourier(OrderData order)
    {
        if (isOrderActive) return;

        StartCoroutine(OrderCourier(order));
    }

    private IEnumerator OrderCourier(OrderData order)
    {
        isOrderActive = true;

        Debug.Log("Zamówiono kuriera...");

        yield return new WaitForSeconds(2f);

        GameObject courier = Instantiate(courierPrefab, spawnPoint.position, Quaternion.identity);

        CourierBehaviour cb = courier.GetComponent<CourierBehaviour>();

        cb.SetOrder(order);

        cb.Init(doorPoint, spawnPoint, OnCourierFinished);
    }

    void OnCourierFinished()
    {
        isOrderActive = false;
    }
}