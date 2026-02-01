using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MaskStageUI : MonoBehaviour
{
    public MaskHealth playerMask;
    public Image[] maskIcons; // 5 icons, full to empty sprites
    public Sprite brokenSprite;

    void Start()
    {
        playerMask.onStageBreak.AddListener(UpdateMaskIcons);
    }

    void UpdateMaskIcons(int remainingStages)
    {
        int brokenIndex = remainingStages;

        if (brokenIndex >= 0 && brokenIndex < maskIcons.Length)
        {
            maskIcons[brokenIndex].sprite = brokenSprite;
            StartCoroutine(ShakeIcon(maskIcons[brokenIndex].transform));
        }
    }

    IEnumerator ShakeIcon(Transform icon)
    {
        Vector3 originalPos = icon.localPosition;
        float time = 0.3f;

        while (time > 0)
        {
            icon.localPosition = originalPos + (Vector3)Random.insideUnitCircle * 5f;
            time -= Time.deltaTime;
            yield return null;
        }

        icon.localPosition = originalPos;
    }
}
