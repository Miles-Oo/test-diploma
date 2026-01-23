using UnityEngine;

public class HoldToStabilizeMinigameDual : MonoBehaviour
{
    [Header("Gauge A (left)")]
    [Range(0f, 1f)] public float levelA = 0.5f;
    public float pushRateA = 0.6f;
    public float drainRateA = 0.25f;
    public KeyCode keyA = KeyCode.A;   // trzymasz A -> podbija A

    [Header("Gauge B (right)")]
    [Range(0f, 1f)] public float levelB = 0.5f;
    public float pushRateB = 0.6f;
    public float drainRateB = 0.25f;
    public KeyCode keyB = KeyCode.L;   // trzymasz L -> podbija B

    [Header("Green Zone (shared)")]
    [Range(0f, 1f)] public float greenMin = 0.58f;
    [Range(0f, 1f)] public float greenMax = 0.74f;

    [Header("Countdown win")]
    public float greenCountdownStart = 10f;
    public bool resetCountdownWhenOut = false;

    public float greenCountdown;
    private bool ended;

    public float LevelA01 => levelA;
    public float LevelB01 => levelB;

    public bool IsAInGreen => levelA >= greenMin && levelA <= greenMax;
    public bool IsBInGreen => levelB >= greenMin && levelB <= greenMax;

    public bool BothInGreen => IsAInGreen && IsBInGreen;

    public float GreenCountdown => greenCountdown;
    public float GreenCountdownProgress01 => 1f - Mathf.Clamp01(greenCountdown / greenCountdownStart);

    public System.Action OnWin;

    public void ResetRound()
    {
        ended = false;
        greenCountdown = greenCountdownStart;
        // levelA = 0.5f; levelB = 0.5f; // opcjonalnie
    }

    private void Update()
    {
        if (ended) return;

        float dt = Time.deltaTime;

        // --- Gauge A ---
        levelA -= drainRateA * dt;
        if (Input.GetKey(keyA)) levelA += pushRateA * dt;
        levelA = Mathf.Clamp01(levelA);

        // --- Gauge B ---
        levelB -= drainRateB * dt;
        if (Input.GetKey(keyB)) levelB += pushRateB * dt;
        levelB = Mathf.Clamp01(levelB);

        // --- Countdown only when BOTH are in green ---
        if (BothInGreen)
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
                greenCountdown = greenCountdownStart;
        }
    }
}
