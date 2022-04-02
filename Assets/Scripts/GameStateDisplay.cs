using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateDisplay : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI playerInputText;
    [SerializeField] TMPro.TextMeshProUGUI botInputText;
    [SerializeField] TMPro.TextMeshProUGUI gameStatusText;
    [SerializeField] TMPro.TextMeshProUGUI playerScoreText;
    [SerializeField] TMPro.TextMeshProUGUI timerText;
    [SerializeField] Slider timerSlider;
    [SerializeField] GameObject playbuttonParent;
    // Start is called before the first frame update
    void Start()
    {
        Manager.Instance.SetGameStatusDisplayRef(this);
        Manager.Instance.GameStart += Init;
    }

    private void OnDisable()
    {
        Manager.Instance.GameStart -= Init;
    }

    private void Init()
    {
        playerInputText.text = "";
        botInputText.text = "";
        gameStatusText.text = "";
        playerScoreText.text = "";
        timerText.text = "";
    }

    public void ShowPlayerGesture(HandGestures gestures)
    {
        playerInputText.text = "You played " + gestures;
        //StartCoroutine(HideText(playerInputText, 1f));
    }

    public void ShowBotGesture(HandGestures gestures)
    {
        botInputText.text = "Bot played " + gestures;
        //StartCoroutine(HideText(botInputText, 1f));
    }

    public void SetTimer(float timeLeft, float totalTime)
    {
        float timeToDisplay = timeLeft / totalTime;
        timerSlider.value = timeToDisplay;
        timerText.text = "Timer: " + (int)timeLeft + "s";
    }

    public void ShowResult(GameResult result)
    {
        if (result == GameResult.playerWin)
        {
            gameStatusText.text = "You Win";
            gameStatusText.color = Color.green;
            //StartCoroutine(HideText(gameStatusText));
        }
        else if (result == GameResult.playerLose)
        {
            gameStatusText.text = "You Lose";
            gameStatusText.color = Color.red;
        }
        else if (result == GameResult.timeUp)
        {
            gameStatusText.text = "Time up, you lose";
            gameStatusText.color = Color.red;
        }
        else if (result == GameResult.draw)
        {
            gameStatusText.text = "Draw";
            gameStatusText.color = Color.yellow;
            //StartCoroutine(HideText(gameStatusText));
        }
    }

    public void ShowPlayerScore(int score)
    {
        playerScoreText.text = "Your score " + score;
    }

    public void ShowMainMenu()
    {
        if (!playbuttonParent.activeInHierarchy)
        {
            playbuttonParent.SetActive(true);
        }
    }

    public void HideMainMenu()
    {
        if (playbuttonParent.activeInHierarchy)
        {
            playbuttonParent.SetActive(false);
        }
    }

    private IEnumerator HideText(TMPro.TextMeshProUGUI textToHide, float time = 2f)
    {
        yield return new WaitForSeconds(time);
        textToHide.text = "";
    }

    #region ButtonInputs
    public void OnPlayButtonPressed()
    {
        Manager.Instance.PlayButtonPressed();
        HideMainMenu();
    }
    #endregion
}
