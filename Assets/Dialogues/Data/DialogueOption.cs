using UnityEngine;

[System.Serializable]
public class DialogueOption
{
    public string optionText;          
    // [SerializeReference]   
    public DialogueNode nextNode;
    public string miniGameID;
    public bool rememberNode = false;
    public bool unlockMiniGame;

}

[System.Serializable]
public class DialogueNode
{
    [TextArea]
    public string npcText;

    // [SerializeReference]
    public DialogueOption[] options;
}