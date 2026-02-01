using UnityEngine;
using UnityEngine.Events;

public class MaskHealth : MonoBehaviour
{
    [Header("Mask Settings")]
    public int maskStages = 5;
    public int healthPerStage = 5;

    private int currentStage;
    private int currentHealth;

    public UnityEvent<int> onStageBreak;          // sends stage index
    public UnityEvent<int, int> onHealthChanged;  // current HP, max HP
    public UnityEvent onPlayerDeath;

    void Start()
    {
        currentStage = maskStages;
        currentHealth = healthPerStage;
        onHealthChanged?.Invoke(currentHealth, healthPerStage);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth > 0)
        {
            onHealthChanged?.Invoke(currentHealth, healthPerStage);
            return;
        }

        BreakMaskStage();
    }

    void BreakMaskStage()
    {
        currentStage--;

        onStageBreak?.Invoke(currentStage);

        if (currentStage > 0)
        {
            // Refill HP for next stage
            currentHealth = healthPerStage;
            onHealthChanged?.Invoke(currentHealth, healthPerStage);
        }
        else
        {
            onPlayerDeath?.Invoke();
        }
    }

    public void HealFull()
    {
        currentStage = maskStages;
        currentHealth = healthPerStage;
        onHealthChanged?.Invoke(currentHealth, healthPerStage);
    }
}
