using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNameManager : MonoBehaviour
{
    [SerializeField] private GameObject inputPanel;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button submitButton;
    [SerializeField] private TMP_Text _welcomeText;
    [SerializeField] private GameObject _menuWindow;
    [SerializeField] private GameObject _onBoardCanvas;
    [SerializeField] private GameObject _board_1;
    [SerializeField] private GameObject _board_2;
    [SerializeField] private GameObject _board_3;
    [SerializeField] private GameObject _board_4;

    private const string PlayerNameKey = "PlayerName";

    private void Start()
    {
        string playerName = PlayerPrefs.GetString(PlayerNameKey);

        if (PlayerPrefs.HasKey(PlayerNameKey))
        {
            _welcomeText.text = "welcome back!";
            _menuWindow.SetActive(true);
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        else
        {
            _menuWindow.SetActive(false);
            _onBoardCanvas.SetActive(true);
            _board_1.SetActive(true);
            Screen.orientation = ScreenOrientation.Portrait;
        }

        submitButton.onClick.AddListener(OnSubmitButtonClicked);
    }

    private void OnSubmitButtonClicked()
    {

        StartCoroutine(ClickOkButton());
    }

    private IEnumerator ClickOkButton()
    {
        yield return new WaitForSeconds(0.25f);
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        inputPanel.SetActive(false);
        _onBoardCanvas.SetActive(false);
        _menuWindow.SetActive(true);
        _welcomeText.text = "Hello!";
        PlayerPrefs.SetString(PlayerNameKey, "hello");
    }

    public void OpenBoard_2()
    {
        StartCoroutine(SwitchBoard(_board_1, _board_2));
    }

    public void OpenBoard_3()
    {
        StartCoroutine(SwitchBoard(_board_2, _board_3));
    }

    public void OpenBoard_4()
    {
        StartCoroutine(SwitchBoard(_board_3, _board_4));
    }

    public void OpenNameWindow()
    {
        //StartCoroutine(SwitchBoard(_board_4, inputPanel));
        OnSubmitButtonClicked();
    }

    private IEnumerator SwitchBoard(GameObject toDisable, GameObject toEnable)
    {
        yield return new WaitForSeconds(0.25f);
        if (toDisable != null) toDisable.SetActive(false);
        if (toEnable != null) toEnable.SetActive(true);
    }
}
