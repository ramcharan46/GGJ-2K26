using UnityEngine;
using UnityEngine.UI;

public class MaskHealthUI : MonoBehaviour
{
    public MaskHealth playerMask;
    private Image fillImage;

    void Awake()
    {
        fillImage = GetComponent<Image>();
    }

    void Start()
    {
        playerMask.onHealthChanged.AddListener(UpdateBar);
    }

    void UpdateBar(int current, int max)
    {
        fillImage.fillAmount = (float)current / max;
    }
}
