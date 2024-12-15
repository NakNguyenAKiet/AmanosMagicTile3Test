using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public float fallSpeed = 5f;
    private bool allowToMove = false;
    public event Action<Tile> OnTitleReturn;
    private RectTransform rectTransform;
    [SerializeField]private Button button, centerButton;
    [SerializeField] bool canTap = true;
    public RectTransform RectTransform { get => rectTransform;}

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (!allowToMove)
        {
            return;
        }
        // Move tile downward
        rectTransform.Translate(fallSpeed * Time.deltaTime * Vector3.down);

        // Destroy tile if it moves off-screen + tile size
        if (rectTransform.anchoredPosition.y < - Screen.height - 100)
        {
            if (canTap)
            {
                //Lose
                MyGameEvent.Instance.LosingGame("You missed tile :(");
            }
            OnTitleReturn?.Invoke(this);
        }
    }
    public void OnTap(bool isCenterTap)
    {
        canTap = false;
        SetButtonInterract();
        MyGameEvent.Instance.GetPoint(isCenterTap, this);
        MySFX.Instance.OnClickTileSound();
    }
    public void AllowToMove(bool isAllow)
    {
        canTap = true;
        SetButtonInterract();
        allowToMove = isAllow;
    }
    void SetButtonInterract()
    {
        centerButton.interactable = canTap;
        button.interactable = canTap;
    }
}
