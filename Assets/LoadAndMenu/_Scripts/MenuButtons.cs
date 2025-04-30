using System.Collections;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject _menuWindow;
    [SerializeField] private GameObject _leadersWindow;
    [SerializeField] private GameObject _settingsWindow;
    [SerializeField] private GameObject _howToPlayWindow;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void ClickPlayBtn()
    {
        StartCoroutine(SwitchWindows(_menuWindow, false, _leadersWindow, true));
    }

    public void ClickCloseLevelsWindow()
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

    private IEnumerator SwitchWindows(GameObject toDisable, bool disableState, GameObject toEnable, bool enableState)
    {
        yield return new WaitForSeconds(0.2f);
        if (toDisable != null) toDisable.SetActive(disableState);
        if (toEnable != null) toEnable.SetActive(enableState);
    }
}
