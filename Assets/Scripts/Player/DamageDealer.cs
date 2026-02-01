using UnityEngine;
using UnityEngine.UI;

public class DamageDealer : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        MaskHealth mask = other.GetComponent<MaskHealth>();
        if (mask != null)
        {
            mask.TakeDamage(damage);
            return;
        }

        MaskHealth normalHealth = other.GetComponent<MaskHealth>();
        if (normalHealth != null)
        {
            normalHealth.TakeDamage(damage);
        }
    }
}
