using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject _menuWindow;
    [SerializeField] private GameObject _leadersWindow;
    [SerializeField] private GameObject _settingsWindow;
    [SerializeField] private GameObject _howToPlayWindow;
    [SerializeField] private GameObject _exitWindow;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void ClickOpenLeadersBtn()
    {
        StartCoroutine(SwitchWindows(_menuWindow, false, _leadersWindow, true));
    }

    public void ClickCloseLeaderssWindow()
    {
        StartCoroutine(SwitchWindows(_leadersWindow, false, _menuWindow, true));
    } 

    public void ClickSettingsBtn()
    {
        StartCoroutine(SwitchWindows(_menuWindow, false, _settingsWindow, true));
    }

    public void ClickCloseSettingsBtn()
    {
        StartCoroutine(SwitchWindows(_settingsWindow, false, _menuWindow, true));
    }

    public void OpenHowToPlay()
    {
        StartCoroutine(SwitchWindows(_menuWindow, false, _howToPlayWindow, true));
    }

    public void CloseHowToPlay()
    {
        StartCoroutine(SwitchWindows(_howToPlayWindow, false, _menuWindow, true));
    }

    public void OpenExitWindow()
    {
        StartCoroutine(SwitchWindows(_menuWindow, false, _exitWindow, true));
    }

    public void CloseExitWindow()
    {
        StartCoroutine(SwitchWindows(_exitWindow, false, _menuWindow, true));
    }

    public void ExitApp()
    {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID
        Application.Quit();
#endif
    }

    public void OpenNewGame()
    {
        StartCoroutine(ClickAndLoadScene());
    }

    private IEnumerator ClickAndLoadScene()
    {
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene("MainScene");
    }

    private IEnumerator SwitchWindows(GameObject toDisable, bool disableState, GameObject toEnable, bool enableState)
    {
        yield return new WaitForSeconds(0.2f);
        if (toDisable != null) toDisable.SetActive(disableState);
        if (toEnable != null) toEnable.SetActive(enableState);
    }
}
