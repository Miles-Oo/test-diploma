using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "System Dialogow/Nowy Dialog")]
public class DialogueAsset : ScriptableObject
{
    [Header("Główny węzeł dialogu")]
    public DialogueNode startNode;
}