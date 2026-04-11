using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimonMinigameLogic : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private List<Button> buttons = new(); // 4-6 buttonów

    [Header("Visual")]
    [SerializeField] private float flashOnSeconds = 0.35f;
    [SerializeField] private float flashOffSeconds = 0.12f;
    [SerializeField] private float betweenRoundsDelay = 0.25f;
    [SerializeField] private Color flashColor = Color.white; // nie ustawiaj jeśli wolisz tint domyślny
    [SerializeField] private float dimAlphaWhenInactive = 0.85f;

    [Header("Rules")]
    [SerializeField] private int startLength = 2;
    [SerializeField] private int targetLengthToWin = 8;
    [SerializeField] private int maxStrikes = 3;
    [SerializeField] private float timeLimitSeconds = 45f;

    [Header("UI (optional)")]
    [SerializeField] private TextMeshProUGUI timerText;   // albo TMP, jeśli używasz
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private TextMeshProUGUI strikesText;

    [Header("Audio (optional)")]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip beepOk;
    [SerializeField] private AudioClip beepFail;

    // Public callbacks
    public System.Action OnWin;
    public System.Action OnLose;

    // runtime
    private List<int> sequence = new();
    private int inputIndex = 0;
    private int strikes = 0;
    private float timeLeft;
    private bool running = false;
    private bool acceptingInput = false;
    private Coroutine showRoutine;

    private Color[] baseColors;

    public void StartRun()
    {
        if (buttons == null || buttons.Count < 4)
        {
            Debug.LogError("SimonMinigameLogic: potrzeba min. 4 buttonów.");
            return;
        }

        CacheBaseColors();
        HookButtons();

        strikes = 0;
        timeLeft = timeLimitSeconds;
        sequence.Clear();

        // startowa sekwencja
        for (int i = 0; i < startLength; i++)
            sequence.Add(Random.Range(0, buttons.Count));

        running = true;
        enabled = true;

        StartRound();
    }

    public void StopRun()
    {
        running = false;
        acceptingInput = false;

        if (showRoutine != null) StopCoroutine(showRoutine);
        showRoutine = null;

        UnhookButtons();
        enabled = false;
    }

    private void Update()
    {
        if (!running) return;

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0f)
        {
            Lose();
            return;
        }

        if (timerText != null)
            timerText.text = Mathf.CeilToInt(timeLeft).ToString();

        if (roundText != null)
            roundText.text = $"Len: {sequence.Count}/{targetLengthToWin}";

        if (strikesText != null)
            strikesText.text = $"X {strikes}/{maxStrikes}";
    }

    private void StartRound()
    {
        acceptingInput = false;
        inputIndex = 0;

        SetButtonsInteractable(false);

        if (showRoutine != null) StopCoroutine(showRoutine);
        showRoutine = StartCoroutine(ShowSequenceRoutine());
    }

    private IEnumerator ShowSequenceRoutine()
    {
        yield return new WaitForSeconds(betweenRoundsDelay);

        for (int i = 0; i < sequence.Count; i++)
        {
            int idx = sequence[i];
            yield return FlashButton(idx);
            yield return new WaitForSeconds(flashOffSeconds);
        }

        acceptingInput = true;
        SetButtonsInteractable(true);
    }

    private IEnumerator FlashButton(int buttonIndex)
    {
        var img = buttons[buttonIndex].GetComponent<Image>();
        if (img == null) yield break;

        Color original = baseColors[buttonIndex];

        // podświetlenie
        img.color = (flashColor.a <= 0f) ? original : flashColor;
        if (source != null && beepOk != null) source.PlayOneShot(beepOk, 0.35f);

        yield return new WaitForSeconds(flashOnSeconds);

        // powrót do normalnego
        img.color = original;
    }

    private void OnUserPressed(int buttonIndex)
    {
        if (!running || !acceptingInput) return;

        // poprawny klik?
        if (sequence[inputIndex] == buttonIndex)
        {
            inputIndex++;

            // runda skończona
            if (inputIndex >= sequence.Count)
            {
                // win?
                if (sequence.Count >= targetLengthToWin)
                {
                    Win();
                    return;
                }

                // dodaj 1 element i pokaż nową sekwencję
                sequence.Add(Random.Range(0, buttons.Count));
                StartRound();
            }
        }
        else
        {
            // błąd
            strikes++;
            if (source != null && beepFail != null) source.PlayOneShot(beepFail);

            if (strikes >= maxStrikes)
            {
                Lose();
                return;
            }

            // kara: pokaż sekwencję od nowa
            StartRound();
        }
    }

    private void Win()
    {
        running = false;
        acceptingInput = false;
        SetButtonsInteractable(false);
        OnWin?.Invoke();
    }

    private void Lose()
    {
        running = false;
        acceptingInput = false;
        SetButtonsInteractable(false);
        OnLose?.Invoke();
    }

    private void SetButtonsInteractable(bool v)
    {
        for (int i = 0; i < buttons.Count; i++)
            buttons[i].interactable = v;

        // opcjonalnie przygaszenie wizualne
        for (int i = 0; i < buttons.Count; i++)
        {
            var img = buttons[i].GetComponent<Image>();
            if (img == null) continue;

            var c = baseColors[i];
            c.a = v ? 1f : dimAlphaWhenInactive;
            img.color = c;
        }
    }

    private void CacheBaseColors()
    {
        baseColors = new Color[buttons.Count];
        for (int i = 0; i < buttons.Count; i++)
        {
            var img = buttons[i].GetComponent<Image>();
            baseColors[i] = img != null ? img.color : Color.white;
        }
    }

    private void HookButtons()
    {
        // ważne: usuń stare listener’y, żeby nie dublować po ponownym otwarciu
        for (int i = 0; i < buttons.Count; i++)
        {
            int idx = i;
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(() => OnUserPressed(idx));
        }
    }

    private void UnhookButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
            buttons[i].onClick.RemoveAllListeners();
    }
}