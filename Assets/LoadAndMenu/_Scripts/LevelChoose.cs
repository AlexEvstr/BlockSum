using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChoose : MonoBehaviour
{
    private Button levelButton;
    private int levelNumber;

    private void Start()
    {
        CheckLevel();
        levelButton.onClick.AddListener(ChooseLevel);
    }

    private void CheckLevel()
    {
        levelButton = GetComponent<Button>();
        levelNumber = int.Parse(gameObject.name);

        int highestUnlockedLevel = PlayerPrefs.GetInt("bestLevel", 1);
        if (highestUnlockedLevel < levelNumber)
        {
            levelButton.enabled = false;
        }
        else
        {
            levelButton.enabled = true;
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void ChooseLevel()
    {
        StartCoroutine(DelayedLoad());
    }

    private IEnumerator DelayedLoad()
    {
        PlayerPrefs.SetInt("levelIndex", levelNumber);
        yield return new WaitForSecondsRealtime(0.25f);
        SceneManager.LoadScene("GameScene");
    }
}
