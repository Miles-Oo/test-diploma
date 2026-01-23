using UnityEngine;

public class HoldToStabilizeMinigame : MonoBehaviour
{
    [Header("Gauge")]
    [Range(0f, 1f)] public float level = 0.5f;
    public float pushRate = 0.6f;
    public float drainRate = 0.25f;
    public KeyCode actionKey = KeyCode.Mouse0;

    [Header("Green Zone")]
    [Range(0f, 1f)] public float greenMin = 0.58f;
    [Range(0f, 1f)] public float greenMax = 0.74f;

    [Header("Countdown win")]
    public float greenCountdownStart = 10f;
    [Tooltip("Jeśli true, to gdy wyjdziesz z zielonego, odliczanie resetuje się do 10s. Jeśli false, zatrzymuje się.")]
    public bool resetCountdownWhenOut = false;

    public float greenCountdown;
    private bool ended;

    public float Level01 => level;
    public bool IsInGreen => level >= greenMin && level <= greenMax;

    public float GreenCountdown => greenCountdown;                 // ile sekund zostało
    public float GreenCountdownProgress01 => 1f - Mathf.Clamp01(greenCountdown / greenCountdownStart);

    public System.Action OnWin;

    public void ResetRound()
    {
        ended = false;
        greenCountdown = greenCountdownStart;
        // level = 0.5f; // opcjonalnie
    }

    void Update()
    {
        if (ended) return;

        float dt = Time.deltaTime;

        bool holding = Input.GetKey(actionKey);

        // zawsze spada
        level -= drainRate * dt;

        // rośnie przy trzymaniu
        if (holding) level += pushRate * dt;

        level = Mathf.Clamp01(level);

        // ODLICZANIE TYLKO W ZIELONYM
        if (IsInGreen)
        {
            greenCountdown -= dt;
            if (greenCountdown <= 0f)
            {
                greenCountdown = 0f;
                ended = true;
                OnWin?.Invoke();
            }
        }
        else
        {
            if (resetCountdownWhenOut)
                greenCountdown = greenCountdownStart; // trudniejszy wariant
        }
    }
}
