using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private Manager() { }
    private GameRules gameCondition = new GameRules();
    private static Manager instance;
    private HandGestures playerGesture = HandGestures.none, botGesture = HandGestures.none;
    private bool timeUp = false;
    private GameStateDisplay gameStatusDisplayRef;
    private float timeCounter = 0;
    private GameState currentState = GameState.menu;
    private int playerScore = 0;
    public static Manager Instance { get => instance; }

    public float maxTimeForPlayerInput = 1;
    public System.Action GameStart, GameEnd;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (gameStatusDisplayRef != null)
        {
            gameStatusDisplayRef.ShowMainMenu();
        }
        ChangeGameState(GameState.menu);
    }

    void Init()
    {
        timeCounter = maxTimeForPlayerInput;
        playerScore = 0;
    }

    void ResetTimer()
    {
        timeCounter = maxTimeForPlayerInput;
        ChangeGameState(GameState.running);
    }

    public void SetGameStatusDisplayRef(GameStateDisplay objRef)
    {
        gameStatusDisplayRef = objRef;
    }

    private void ChangeGameState(GameState newState)
    {
        currentState = newState;
        RunGame();
    }

    public void PlayButtonPressed()
    {
        Init();
        ChangeGameState(GameState.running);
        GameStart?.Invoke();
    }


    private void RunGame()
    {
        switch (currentState)
        {
            case GameState.menu:
                {
                    gameStatusDisplayRef.ShowMainMenu();
                    break;
                }
            case GameState.checkingResult:
                {
                    CheckForResult();
                    break;
                }
            case GameState.gameOver:
                {
                    StartCoroutine(ShowMainMenu());
                    break;
                }
        }
    }

    private IEnumerator ShowMainMenu()
    {
        yield return new WaitForSeconds(2.5f);
        gameStatusDisplayRef.ShowMainMenu();
    }
    private void DecrementTimer()
    {
        if (playerGesture == HandGestures.none)
        {
            timeCounter -= Time.deltaTime;
            if (timeCounter <= 0)
            {
                //Show time up
                gameStatusDisplayRef.ShowResult(GameResult.timeUp);
                gameStatusDisplayRef.ShowPlayerScore(playerScore);
                ChangeGameState(GameState.gameOver);

                GameEnd?.Invoke();
            }
            else
            {
                //Display timer
                gameStatusDisplayRef.SetTimer(timeCounter, maxTimeForPlayerInput);
            }
        }
    }

    private void Update()
    {
        if (currentState == GameState.running)
        {
            if (playerGesture == HandGestures.none)
            {
                DecrementTimer();
            }
        }
    }

    private void CheckForResult()
    {
        if (playerGesture == HandGestures.none || botGesture == HandGestures.none)
            return;

        int playerInput = (int)playerGesture;
        int botInput = (int)botGesture;
        playerGesture = HandGestures.none;
        botGesture = HandGestures.none;

        if (gameCondition.gameRules[playerInput, botInput] == 1)
        {
            //player wins
            gameStatusDisplayRef.ShowResult(GameResult.playerWin);
            playerScore++;
            StartCoroutine(ResetGame());
        }
        else if (gameCondition.gameRules[playerInput, botInput] == -1)
        {
            //player loses
            gameStatusDisplayRef.ShowResult(GameResult.playerLose);
            gameStatusDisplayRef.ShowPlayerScore(playerScore);
            ChangeGameState(GameState.gameOver);
            GameEnd?.Invoke();
        }
        else
        {
            //draw
            gameStatusDisplayRef.ShowResult(GameResult.draw);
            StartCoroutine(ResetGame());
        }
    }

    private IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(2.5f);
        ResetTimer();
        GameStart?.Invoke();
    }

    public void SetPlayerHandGesture(int gesture)
    {
        if (!timeUp)
        {
            playerGesture = (HandGestures)gesture;
            ShowGesturesOnUI();
            ChangeGameState(GameState.checkingResult);
        }
    }

    private void ShowGesturesOnUI()
    {
        gameStatusDisplayRef.ShowPlayerGesture(playerGesture);
        gameStatusDisplayRef.ShowBotGesture(botGesture);
    }

    public void SetBotHandGesture(int gestures)
    {
        botGesture = (HandGestures)gestures;
    }

}

public enum GameResult
{
    playerWin = 0,
    playerLose,
    draw,
    timeUp
}

public enum GameState
{
    menu,
    running,
    checkingResult,
    gameOver
}