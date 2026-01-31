using UnityEngine;

public class SpiritInteraction : MonoBehaviour
{
    private bool playerInRange = false;

    public DialogueData dialogueData;      // Unique dialogue per spirit
    public DialogueManager dialogueManager;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !dialogueManager.IsDialogueActive)
{
    dialogueManager.StartDialogue(dialogueData);
}

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
