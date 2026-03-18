using UnityEngine;

[System.Serializable]
public class DialogueOption
{
    public string optionText;          
    // [SerializeReference]   
    public DialogueNode nextNode;
    public MiniGameBezpiecznikiCore unlockMiniGame;
    public bool rememberNode = false;
}

// Węzeł dialogowy NPC
[System.Serializable]
public class DialogueNode
{
    [TextArea]
    public string npcText;

    // [SerializeReference]
    public DialogueOption[] options;
}