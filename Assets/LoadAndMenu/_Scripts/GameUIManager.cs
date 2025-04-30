using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _pause;
    [SerializeField] private GameObject _pauseBtn;

    private void Start()
    {
        Time.timeScale = 1;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public void PauseButton()
    {
        StartCoroutine(DelayedPause());
    }

    public void UnPauseButton()
    {
        Time.timeScale = 1;
        StartCoroutine(DelayedUnPause());
    }

    public void MenuButton()
    {
        Time.timeScale = 1;
        StartCoroutine(DelayedLoadScene("MenuScene"));
    }

    public void ReplayButton()
    {
        Time.timeScale = 1;
        StartCoroutine(DelayedLoadScene("GameScene"));
    }

    private IEnumerator DelayedPause()
    {
        yield return new WaitForSeconds(0.25f);
        _pause.SetActive(true);
        _pauseBtn.SetActive(false);
        Time.timeScale = 0;
    }

    private IEnumerator DelayedUnPause()
    {
        yield return new WaitForSeconds(0.25f);
        _pause.SetActive(false);
        _pauseBtn.SetActive(true);
        Time.timeScale = 1;
    }

    private IEnumerator DelayedLoadScene(string sceneName)
    {
        yield return new WaitForSecondsRealtime(0.25f);
        Time.timeScale = 1; // на случай, если из паузы
        SceneManager.LoadScene(sceneName);
    }
}
