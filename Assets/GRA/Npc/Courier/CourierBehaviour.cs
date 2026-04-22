using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NpcPathCalculatorAndFinder))]
public class CourierBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject paczkaPrefab;

    private NpcPathCalculatorAndFinder path;
    private Transform doorTarget;
    private Transform exitTarget;
    private System.Action onFinish;

    private OrderData order;

    private IInventoryTarget lodowka;
    private IInventoryTarget biblioteczka;

    private enum State
    {
        GoingToDoor,
        Delivering,
        GoingBack,
        Finished
    }

    private State currentState;

    void Awake()
    {
        path = GetComponent<NpcPathCalculatorAndFinder>();
    }

    public void SetOrder(OrderData newOrder)
    {
        order = newOrder;
    }

    public void SetTargets(IInventoryTarget lod, IInventoryTarget bib)
    {
        lodowka = lod;
        biblioteczka = bib;
    }

    public bool Init(Transform door, Transform exit, System.Action callback)
    {
        if (door == null || exit == null)
            return false;

        doorTarget = door;
        exitTarget = exit;
        onFinish = callback;

        path.OnTest += OnReachedTarget;

        return GoToDoor();
    }

    private bool GoToDoor()
    {
        currentState = State.GoingToDoor;

        path.SetTarget(doorTarget.gameObject);
        path.MoveToTarget();

        return true;
    }

    private void OnReachedTarget()
    {
        switch (currentState)
        {
            case State.GoingToDoor:
                StartCoroutine(Deliver());
                break;

            case State.GoingBack:
                Finish();
                break;
        }
    }

    private IEnumerator Deliver()
    {
        currentState = State.Delivering;

        yield return new WaitForSeconds(2f);

        SpawnPackage();

        GoBack();
    }

    private void SpawnPackage()
    {
        if (paczkaPrefab == null)
        {
            return;
        }

        if (order == null)
        {
            return;
        }

        GameObject obj = Instantiate(paczkaPrefab, doorTarget.position, Quaternion.identity);

        paczka p = obj.GetComponent<paczka>();

        if (p == null)
        {
            return;
        }

        p.SetItems(order.items);

        p.SetTargets(lodowka, biblioteczka);

    }

    private bool GoBack()
    {
        currentState = State.GoingBack;

        path.SetTarget(exitTarget.gameObject);
        path.MoveToTarget();

        return true;
    }

    private void Finish()
    {
        currentState = State.Finished;
        path.OnTest -= OnReachedTarget;

        StartCoroutine(FadeAndDestroy());
    }

    private IEnumerator FadeAndDestroy()
    {
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime;

            Color c = sr.color;
            c.a = 1 - t;
            sr.color = c;

            yield return null;
        }

        onFinish?.Invoke();
        Destroy(gameObject);
    }
}