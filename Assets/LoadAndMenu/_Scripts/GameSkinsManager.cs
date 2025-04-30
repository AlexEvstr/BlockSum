using UnityEngine;
using UnityEngine.UI;

public class GameSkinsManager : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Sprite[] _backgroundSprites;
    [SerializeField] private SpriteRenderer _plane;
    [SerializeField] private Sprite[] _planeSprites;

    private void Awake()
    {
        _backgroundImage.sprite = _backgroundSprites[PlayerPrefs.GetInt("selectedBackgroundIndex", 0)];
        _plane.sprite = _planeSprites[PlayerPrefs.GetInt("selectedAirplaneIndex", 0)];
    }
}