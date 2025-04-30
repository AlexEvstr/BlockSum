using UnityEngine;

[System.Serializable]
public class WordLevel
{
    [SerializeField] private string _wordName;
    [SerializeField] private string[] _letters;
    [SerializeField] private GameObject[] _letterPrefabs;
    private string _word;

    public string ShowWord()
    {
        foreach (var letter in _letters)
        {
            _word += letter;    
        }
        return _word;
    }

    public GameObject[] SetLetterPrefabs()
    {
        return _letterPrefabs;
    }
}