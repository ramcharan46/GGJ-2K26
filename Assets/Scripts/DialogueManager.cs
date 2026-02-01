using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public Image portraitImage;

    public float typingSpeed = 0.03f;

    private DialogueData currentData;
    private int index;
    public bool IsDialogueActive { get; private set; }

    private bool isTyping = false;
    private Coroutine typingCoroutine;

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    public void StartDialogue(DialogueData data)
    {
        if (IsDialogueActive) return;
        if (data == null || data.dialogueLines.Length == 0) return;

        IsDialogueActive = true;
        McMovement.canMove = false;

        currentData = data;
        index = 0;

        dialoguePanel.SetActive(true);
        ShowLine();
    }

    void Update()
    {
        if (!IsDialogueActive) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = GetLineWithoutPrefix(currentData.dialogueLines[index]);
                isTyping = false;
            }
            else
            {
                NextLine();
            }
        }
    }

    void NextLine()
    {
        index++;

        if (index < currentData.dialogueLines.Length)
        {
            ShowLine();
        }
        else
        {
            EndDialogue();
        }
    }

    void ShowLine()
    {
        string rawLine = currentData.dialogueLines[index];

        bool isPlayerSpeaking = rawLine.StartsWith("P:");
        bool isSpiritSpeaking = rawLine.StartsWith("S:");

        if (isPlayerSpeaking)
        {
            nameText.text = currentData.playerName;
            portraitImage.sprite = currentData.playerPortrait;
        }
        else if (isSpiritSpeaking)
        {
            nameText.text = currentData.spiritName;
            portraitImage.sprite = currentData.spiritPortrait;
        }

        string cleanLine = GetLineWithoutPrefix(rawLine);
        typingCoroutine = StartCoroutine(TypeLine(cleanLine));
    }

    string GetLineWithoutPrefix(string line)
    {
        if (line.Length > 2 && line[1] == ':')
            return line.Substring(2).Trim();
        return line;
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in line)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        nameText.text = "";
        portraitImage.sprite = null;

        IsDialogueActive = false;
        McMovement.canMove = true;
    }
}
