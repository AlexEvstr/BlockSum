using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public TileBoard board;

    public CanvasGroup gameOver;

    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;

    public TextMeshProUGUI gameoverCurrentScoreText;
    public TextMeshProUGUI gameoverHighScoreText;

    public int score;

    void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        SetScore(0);
        highScoreText.text = LoadHighScore().ToString();
        gameoverHighScoreText.text = LoadHighScore().ToString();

        gameOver.alpha = 0.0f;
        gameOver.interactable = false;

        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }

    public void GameOver()
    {
        board.enabled = false;
        gameOver.interactable = true;

        StartCoroutine(FadeGameOver(gameOver, 1.0f, 1.0f));
    }

    private IEnumerator FadeGameOver(CanvasGroup canvasGroup, float to, float delay)
    {
        // Delay fade by delay seconds
        yield return new WaitForSeconds(delay);

        // Set up variables for lerp animation
        float elapsed = 0.0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        // Keep shifting while the time has not surpassed the duration
        while (elapsed < duration)
        {
            // Change the opacity of canvasGroup by interpolation by elapsed / duration
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Set the final position of the canvasGroup
        canvasGroup.alpha = to;
    }

    // Used to increase score
    public void IncreaseScore(int points)
    {
        // set the score by addint points to it
        SetScore(score + points);
    }

    // Sets the score
    private void SetScore(int score)
    {
        // Set score and update the currentScoreText
        this.score = score;
        currentScoreText.text = this.score.ToString();
        gameoverCurrentScoreText.text = this.score.ToString();

        // Call saveHighScore to see if score needs to update
        SaveHighScore();
    }

    // Uploads and saves hichScore if necessary 
    private void SaveHighScore()
    {
        // Load the highscore from computer
        int highScore = LoadHighScore();

        // If the score of current game is higher than the 
        // highscore from the computer then update highscore
        if (score > highScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            highScoreText.text = score.ToString();
            gameoverHighScoreText.text = score.ToString();
        }
    }

    // Used to get highscore saved on the computer
    private int LoadHighScore()
    {
        return PlayerPrefs.GetInt("highScore", 0);
    }
}
