using UnityEngine;

[System.Serializable]
public class DialogueOption
{
    public string optionText;          
    // [SerializeReference]   
    public DialogueNode nextNode;
    public string miniGameID;
    public bool rememberNode = false;
}

[System.Serializable]
public class DialogueNode
{
    [TextArea]
    public string npcText;

    // [SerializeReference]
    public DialogueOption[] options;
}