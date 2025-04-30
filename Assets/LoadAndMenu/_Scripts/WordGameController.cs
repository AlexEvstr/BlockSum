using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System.Collections;

public class WordGameController : MonoBehaviour
{
    [SerializeField] private WordLevel[] _wordLevel;
    [SerializeField] private TMP_Text _currentWord;
    [SerializeField] private GameObject _win;
    private ObstacleSpawner obstacleSpawner;
    [SerializeField] private float minDistance = 2.0f;
    [SerializeField] private GameObject plane;
    [SerializeField] private GameObject _winEffect;
    private HashSet<int> coloredIndices = new HashSet<int>();
    private GameAudioManager _gameAudioManager;
    public static int LevelIndex;

    private List<GameObject> spawnedLetters = new List<GameObject>();

    private void Start()
    {
        _gameAudioManager = GetComponent<GameAudioManager>();
        obstacleSpawner = GetComponent<ObstacleSpawner>();
        LevelIndex = PlayerPrefs.GetInt("levelIndex", 1);
        _currentWord.text = _wordLevel[LevelIndex - 1].ShowWord();

        GameObject[] letters = _wordLevel[LevelIndex - 1].SetLetterPrefabs();

        foreach (var letter in letters)
        {
            if (letter != null)
            {
                Vector2 newPosition = GenerateUniquePosition();
                GameObject newLetter = Instantiate(letter, newPosition, Quaternion.identity);
                spawnedLetters.Add(newLetter);
            }
        }

        obstacleSpawner.plane = plane;
        obstacleSpawner.Initialize(spawnedLetters, minDistance);
    }

    private Vector2 GenerateUniquePosition()
    {
        Vector2 newPosition;
        bool positionIsValid;

        do
        {
            newPosition = new Vector2(Random.Range(-30, 30), Random.Range(-30, 30));
            positionIsValid = true;

            foreach (var obj in spawnedLetters)
            {
                if (Vector2.Distance(newPosition, obj.transform.position) < minDistance)
                {
                    positionIsValid = false;
                    break;
                }
            }

            if (positionIsValid && plane != null && Vector2.Distance(newPosition, plane.transform.position) < minDistance)
            {
                positionIsValid = false;
            }
        } while (!positionIsValid);

        return newPosition;
    }

    public void UnlockNewLetter()
    {
        string currentLetter = PlayerPrefs.GetString("CurrentLetter", "");
        if (string.IsNullOrEmpty(currentLetter) || currentLetter.Length != 1)
        {
            return;
        }

        string text = StripColorTags(_currentWord.text);
        char letterToFind = currentLetter[0];
        int nextIndexToColor = -1;

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == letterToFind && !coloredIndices.Contains(i))
            {
                nextIndexToColor = i;
                break;
            }
        }

        if (nextIndexToColor != -1)
        {
            coloredIndices.Add(nextIndexToColor);
        }

        UpdateColoredText(text);

        if (coloredIndices.Count == text.Length)
        {
            StartCoroutine(WinBehavior());
        }
    }

    private void UpdateColoredText(string text)
    {
        string coloredText = "";

        for (int i = 0; i < text.Length; i++)
        {
            if (coloredIndices.Contains(i))
            {
                coloredText += $"<color=#FF0200>{text[i]}</color>";
            }
            else
            {
                coloredText += text[i];
            }
        }

        _currentWord.text = coloredText;
    }

    private IEnumerator WinBehavior()
    {
        yield return new WaitForSeconds(1f);
        GameObject winEffect = Instantiate(_winEffect, plane.transform);
        winEffect.transform.position = plane.transform.position;
        _gameAudioManager.PlayWin();
        yield return new WaitForSeconds(1f);
       
        
        LevelIndex++;
        PlayerPrefs.SetInt("levelIndex", LevelIndex);
        if (PlayerPrefs.GetInt("bestLevel", 1) < LevelIndex)
        {
            PlayerPrefs.SetInt("bestLevel", LevelIndex);
        }
        _win.SetActive(true);
        Time.timeScale = 0;
    }

    private string StripColorTags(string input)
    {
        return Regex.Replace(input, "<.*?>", string.Empty);
    }
}