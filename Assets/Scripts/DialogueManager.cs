using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public UnityEngine.UI.Image portraitImage;
    

    public float typingSpeed = 0.03f; // Smaller = faster typing

    private string[] lines;
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
    if (IsDialogueActive) return; // 🚫 Prevent re-triggering

    if (data == null || data.dialogueLines.Length == 0) return;

    IsDialogueActive = true;
    McMovement.canMove = false;

    lines = data.dialogueLines;
    index = 0;

    nameText.text = data.spiritName;
    portraitImage.sprite = data.portrait;

    dialoguePanel.SetActive(true);
    StartTyping(lines[index]);
}


    void Update()
    {
        if (!IsDialogueActive) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                // Finish line instantly if still typing
                StopCoroutine(typingCoroutine);
                dialogueText.text = lines[index];
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

        if (index < lines.Length)
        {
            StartTyping(lines[index]);
        }
        else
        {
            EndDialogue();
        }
    }

    void StartTyping(string line)
    {
        typingCoroutine = StartCoroutine(TypeLine(line));
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

    IsDialogueActive = false; // ✅ Now dialogue can start again
    McMovement.canMove = true;
}

}
