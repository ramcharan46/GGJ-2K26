using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [Header("Spirit Info")]
    public string spiritName;
    public Sprite spiritPortrait;

    [Header("Player Info")]
    public string playerName = "You";
    public Sprite playerPortrait;

    [Header("Dialogue")]
    [TextArea(3, 6)]
    public string[] dialogueLines;
}
