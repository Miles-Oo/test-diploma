using System.Collections;
using UnityEngine;

[RequireComponent(typeof(NpcPathCalculatorAndFinder))]
public class CourierBehaviour : MonoBehaviour
{
    private NpcPathCalculatorAndFinder path;

    private Transform doorTarget;
    private Transform exitTarget;

    private System.Action onFinish;

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

    public void Init(Transform door, Transform exit, System.Action callback)
    {
        doorTarget = door;
        exitTarget = exit;
        onFinish = callback;

        path.OnTest += OnReachedTarget;

        GoToDoor();
    }

    void GoToDoor()
    {
        currentState = State.GoingToDoor;

        path.SetTarget(doorTarget.gameObject);
        path.MoveToTarget();
    }

    void OnReachedTarget()
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

    IEnumerator Deliver()
    {
        currentState = State.Delivering;

        Debug.Log("Dostarczam paczkę");

        // tutaj możesz odpalić animację
        yield return new WaitForSeconds(2f);

        SpawnPackage();

        GoBack();
    }

    void GoBack()
    {
        currentState = State.GoingBack;

        path.SetTarget(exitTarget.gameObject);
        path.MoveToTarget();
    }

    void SpawnPackage()
    {
        Debug.Log("📦 Paczka dostarczona");

        // tu możesz Instantiate paczki
    }

    void Finish()
    {
        currentState = State.Finished;

        StartCoroutine(FadeAndDestroy());
    }

    IEnumerator FadeAndDestroy()
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