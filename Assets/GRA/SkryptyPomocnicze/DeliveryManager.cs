using System.Collections;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public GameObject courierPrefab;
    public Transform spawnPoint;
    public Transform doorPoint;

    private bool isOrderActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && !isOrderActive)
        {
            StartCoroutine(OrderCourier());
        }
    }

IEnumerator OrderCourier()
{
    isOrderActive = true;

    Debug.Log("Zamówiono kuriera...");

    yield return new WaitForSeconds(10f);

    GameObject courier = Instantiate(courierPrefab, spawnPoint.position, Quaternion.identity);

    // 🔹 debug przed Init
    Debug.Log("doorPoint: " + doorPoint + ", spawnPoint: " + spawnPoint);
    Debug.Log("courier prefab: " + courier);

    CourierBehaviour cb = courier.GetComponent<CourierBehaviour>();
    if(cb == null)
        Debug.LogError("Brak CourierBehaviour na prefabie!");

    cb.Init(doorPoint, spawnPoint, OnCourierFinished);
}

    void OnCourierFinished()
    {
        isOrderActive = false;
    }
}