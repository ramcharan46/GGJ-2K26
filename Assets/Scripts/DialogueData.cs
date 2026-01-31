using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [Header("Spirit Info")]
    public string spiritName;
    public Sprite portrait;

    [Header("Dialogue Lines")]
    [TextArea(3, 6)]
    public string[] dialogueLines;
}
