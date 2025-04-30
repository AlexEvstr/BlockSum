using UnityEngine;
using TMPro;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject _losePanel;
    private GameAudioManager _gameAudioManager;
    private float timeRemaining;
    private bool timerIsRunning = false;

    private void Awake()
    {
        Time.timeScale = 1;
        _gameAudioManager = GetComponent<GameAudioManager>();
        timerIsRunning = true;
        timeRemaining = 59f;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining >= 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                timerText.text = "00";
                LoseBehavior();
            }
        }
    }

    private void UpdateTimerDisplay(float timeToDisplay)
    {
        timeToDisplay += 1;
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}", seconds);
    }

    public void LoseBehavior()
    {
        StartCoroutine(ShowLose());
    }


    private IEnumerator ShowLose()
    {
        yield return new WaitForSeconds(1.0f);
        _gameAudioManager.PlayLose();
        _losePanel.SetActive(true);
        Time.timeScale = 0;
    }
}