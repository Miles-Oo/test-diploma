using UnityEngine;

public class UnlockMiniGameAfterDialogue : MonoBehaviour
{
    public NPCDialogue npc;                   // NPC, z którym musisz porozmawiać
    public MiniGameBezpiecznikiCore miniGame; // minigra do odblokowania

    private void Start()
    {
        if (npc == null || miniGame == null)
        {
            Debug.LogWarning("Brak referencji w UnlockMiniGameAfterDialogue");
            return;
        }

        // Subskrybujemy metodę EndDialogue
        DialogueManager.Instance.OnDialogueEnded += OnDialogueEnded;
    }

    private void OnDestroy()
    {
        if (DialogueManager.Instance != null)
            DialogueManager.Instance.OnDialogueEnded -= OnDialogueEnded;
    }

    private void OnDialogueEnded(GameObject endedNPC)
    {
        if (endedNPC == npc.gameObject)
        {
            miniGame.UnlockMiniGame();
        }
    }
}