using System.Collections;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public GameObject courierPrefab;
    public Transform spawnPoint;
    public Transform doorPoint;

    [SerializeField] private GameObject fridge;
    [SerializeField] private GameObject library;

    private bool isOrderActive = false;

    private enum TargetType
    {
        Fridge,
        Library
    }

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

        yield return new WaitForSeconds(2f);

        GameObject courier = Instantiate(courierPrefab, spawnPoint.position, Quaternion.identity);

        CourierBehaviour cb = courier.GetComponent<CourierBehaviour>();

        IInventoryTarget target = GetTarget(TargetType.Fridge);

        cb.SetDeliveryTarget(target);

        cb.Init(doorPoint, spawnPoint, OnCourierFinished);
    }

    private IInventoryTarget GetTarget(TargetType type)
    {
        switch (type)
        {
            case TargetType.Fridge:
                return fridge.GetComponentInChildren<IInventoryTarget>();

            case TargetType.Library:
                return library.GetComponentInChildren<IInventoryTarget>();

            default:
                return fridge.GetComponentInChildren<IInventoryTarget>();
        }
    }

    void OnCourierFinished()
    {
        isOrderActive = false;
    }
}