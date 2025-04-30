using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmotionDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _emotionObject;
    [SerializeField] private Image _emotionImage;
    [SerializeField] private Sprite[] _emotionSprites;
    [SerializeField] private TMP_Text _emotionBaseText;
    [SerializeField] private string[] _allEmotionTexts;

    [SerializeField] private float _emotionDuration = 1.7f;

    private int _currentEmotionIndex = 0;
    private Coroutine _activeRoutine;

    public void ShowNextEmotion()
    {
        if (_emotionSprites.Length == 0 || _allEmotionTexts.Length == 0)
        {
            Debug.LogWarning("Emotion data is not assigned!");
            return;
        }

        if (_activeRoutine != null)
        {
            return;
        }

        _activeRoutine = StartCoroutine(ShowEmotion());
    }

    private IEnumerator ShowEmotion()
    {
        // Установка текущего спрайта и текста
        _emotionImage.sprite = _emotionSprites[_currentEmotionIndex % _emotionSprites.Length];
        _emotionBaseText.text = _allEmotionTexts[_currentEmotionIndex % _allEmotionTexts.Length];

        _emotionObject.SetActive(true);

        _currentEmotionIndex++;

        yield return new WaitForSeconds(_emotionDuration);

        _emotionObject.SetActive(false);
        _activeRoutine = null;
    }
}
