using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPos : MonoBehaviour
{
    private int slotsCount = 4;
    private void Awake()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float currentHeight = rectTransform.sizeDelta.y;
        rectTransform.sizeDelta = new Vector2(Screen.width/ slotsCount, currentHeight);
    }
}
