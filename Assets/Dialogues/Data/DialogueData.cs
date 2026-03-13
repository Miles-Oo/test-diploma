using UnityEngine;

// Pojedyncza opcja, którą może wybrać gracz
[System.Serializable]
public class DialogueOption
{
    public string optionText;          // Tekst wyświetlany na przycisku

    // [SerializeReference]   // <- zamiast zwykłego DialogueNode
    public DialogueNode nextNode;      // Wskaźnik na kolejny węzeł dialogu
}

// Węzeł dialogowy NPC
[System.Serializable]
public class DialogueNode
{
    [TextArea]
    public string npcText;

    // [SerializeReference]   // <- tablica opcji jako referencje
    public DialogueOption[] options;
}