using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TileBoard board;

    public CanvasGroup gameOver;

    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;

    public TextMeshProUGUI gameoverCurrentScoreText;
    public TextMeshProUGUI gameoverHighScoreText;

    [SerializeField] private GameObject _leadersWindow;
    private GameAudioManager _gameAudioManager;

    public int score;

    void Start()
    {
        NewGame();
        _gameAudioManager = GetComponent<GameAudioManager>();
    }

    public void NewGame()
    {
        SetScore(0);
        int topScore = PlayerPrefs.GetInt("BestScore", 0);
        highScoreText.text = topScore.ToString();
        gameoverHighScoreText.text = topScore.ToString();

        gameOver.alpha = 0.0f;
        gameOver.interactable = false;
        gameOver.blocksRaycasts = false;

        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }

    public void GameOver()
    {
        board.enabled = false;
        gameOver.interactable = true;
        gameOver.blocksRaycasts = true;
        _gameAudioManager.PlayFinishSound();
        StartCoroutine(FadeGameOver(gameOver, 1.0f, 1.0f));
    }

    private IEnumerator FadeGameOver(CanvasGroup canvasGroup, float to, float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0.0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int points)
    {
        SetScore(score + points);
    }

    private void SetScore(int score)
    {
        this.score = score;

        currentScoreText.text = this.score.ToString();
        gameoverCurrentScoreText.text = this.score.ToString();

        int best = PlayerPrefs.GetInt("BestScore", 0);
        if (this.score > best)
        {
            highScoreText.text = this.score.ToString();
            gameoverHighScoreText.text = this.score.ToString();

            PlayerPrefs.SetInt("BestScore", this.score);
            PlayerPrefs.Save();
        }
    }

    private void SaveHighScore(int finalScore)
    {
        List<(int score, string date)> topScores = LoadTopScores();

        topScores.Add((finalScore, DateTime.Now.ToString("dd.MM.yyyy")));

        topScores.Sort((a, b) => b.score.CompareTo(a.score));

        if (topScores.Count > 5)
            topScores.RemoveRange(5, topScores.Count - 5);

        for (int i = 0; i < topScores.Count; i++)
        {
            PlayerPrefs.SetInt($"highScore_{i}", topScores[i].score);
            PlayerPrefs.SetString($"highScoreDate_{i}", topScores[i].date);
        }

        PlayerPrefs.Save();

        int bestSaved = PlayerPrefs.GetInt("highScore_0", 0);
        if (finalScore >= bestSaved)
        {
            highScoreText.text = finalScore.ToString();
            gameoverHighScoreText.text = finalScore.ToString();
        }
    }

    private List<(int score, string date)> LoadTopScores()
    {
        var scores = new List<(int score, string date)>();

        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.HasKey($"highScore_{i}"))
            {
                int savedScore = PlayerPrefs.GetInt($"highScore_{i}");
                string savedDate = PlayerPrefs.GetString($"highScoreDate_{i}");
                scores.Add((savedScore, savedDate));
            }
        }

        return scores;
    }

    public void OpenMenu()
    {
        StartCoroutine(ClickAndLoadScene());
    }

    private IEnumerator ClickAndLoadScene()
    {
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene("MenuScene");
    }

    public void RepeatGame()
    {
        StartCoroutine(ClickAndReload());
    }

    private IEnumerator ClickAndReload()
    {
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene("MainScene");
    }

    public void OpenLeaders()
    {
        StartCoroutine(SwitchWindows(null, false, _leadersWindow, true));
    }

    public void CloseLeaders()
    {
        StartCoroutine(SwitchWindows(_leadersWindow, false, null, true));
    }

    private IEnumerator SwitchWindows(GameObject toDisable, bool disableState, GameObject toEnable, bool enableState)
    {
        yield return new WaitForSeconds(0.2f);
        if (toDisable != null) toDisable.SetActive(disableState);
        if (toEnable != null) toEnable.SetActive(enableState);
    }

    private void OnDisable()
    {
        SaveHighScore(score);
    }
}
