using UnityEngine;

public class PlayerDeathMask : MonoBehaviour
{
    public MaskHealth maskHealth;
    public Transform respawnPoint;

    void Start()
    {
        maskHealth.onPlayerDeath.AddListener(Die);
    }

    void Die()
    {
        StartCoroutine(RespawnRoutine());
    }

    System.Collections.IEnumerator RespawnRoutine()
    {
        McMovement.canMove = false;

        yield return new WaitForSeconds(1f);

        transform.position = respawnPoint.position;
        maskHealth.HealFull();

        McMovement.canMove = true;
    }
}
