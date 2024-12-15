using System;
using TMPro;
using TTHUnityBase.Base.DesignPattern;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    private int currentScore = 0;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int scorePerTile = 10;
    [SerializeField] Slider timerSlider;
    [SerializeField] float gameTime = 10;
    [SerializeField] float timeCounter = 0;
    [SerializeField] float tapdisctanceCheck = 200;
    [SerializeField] GameEffect gameEffect;
    [SerializeField] Transform accuracyLine;

    bool isStartGame = false;
    public int CurrentScore { get => currentScore;}

    private void Start()
    {
        timerSlider.value = 0;
        timeCounter = 0;
        accuracyLine.gameObject.SetActive(false);
        MyGameEvent.Instance.OnGetPoint += AddScore;
        MyGameEvent.Instance.OnLosingGame += GameOver;
    }
    private void Update()
    {
        if (!isStartGame) return;
        if(timeCounter <= gameTime)
        {
            timerSlider.value = timeCounter/gameTime;
            timeCounter += Time.deltaTime;
        }
        else
        {
            CompleteLevel();
        }
    }
    void CompleteLevel()
    {
        EndGame();
        scoreText.text = $"YOU WIN \n Score: {currentScore}";
        gameEffect.PlayCompleteLevelVFX();
    }
    void GameOver(string message)
    {
        if (!isStartGame) return;
        scoreText.text = message;
        EndGame();
    }
    public void RestartGame()
    {
        accuracyLine.gameObject.SetActive(true);
        gameEffect.StopCompleteLevelVFX();
        isStartGame = true;
        timerSlider.value = 0;
        timeCounter = 0;
        MySFX.Instance.PlayBGSound();
        currentScore = 0;
        scoreText.text = "Score: " + currentScore;
    }
    void EndGame()
    {
        MyGameEvent.Instance.EndGame();
        accuracyLine.gameObject.SetActive(false);
        isStartGame = false;
    }
    void AddScore(bool isCenter, Tile tile)
    {
        if (!isStartGame) return;

        //Acuracy Check
        //Acuracy: Perfect, Good
        //Center tile tap check

        //Speed check
        bool isPerfectSpeed = false;
        float tapDistance = Math.Abs(accuracyLine.position.y - tile.transform.position.y);
        if(tapDistance <= tapdisctanceCheck) 
        {
            isPerfectSpeed = true;
        }

        //Add perfect score
        if (isPerfectSpeed && isCenter)
        {
            currentScore += scorePerTile;
        }

        gameEffect.OnGetScoreEffect(isPerfectSpeed && isCenter);

        currentScore += scorePerTile;
        scoreText.text = "Score: " + currentScore;
    }
    public void OnTapBackGround()
    {
        if (isStartGame)
        {
            GameOver("You missed the tile :(");
        }
    }
}
public class MyGame: SingletonMonoBehaviour<GamePlayManager> { }
