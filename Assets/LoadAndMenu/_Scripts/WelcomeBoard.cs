using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WelcomeBoard : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Sprite[] _backgroundSprites;
    [SerializeField] private SpriteRenderer _plane;
    [SerializeField] private Sprite[] _planeSprites;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        _backgroundImage.sprite = _backgroundSprites[PlayerPrefs.GetInt("selectedBackgroundIndex", 0)];
        _plane.sprite = _planeSprites[PlayerPrefs.GetInt("selectedAirplaneIndex", 0)];
    }

    private void Start()
    {
        StartCoroutine(LoadMenuScene());
    }

    private IEnumerator LoadMenuScene()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("MenuScene");
    }
}