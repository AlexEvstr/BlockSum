using UnityEngine;
using TMPro;

public class TopScoresUI : MonoBehaviour
{
    [SerializeField] private TMP_Text[] scoreTexts;
    [SerializeField] private TMP_Text[] dateTexts;

    private void Start()
    {
        RefreshTopScores();
    }

    public void RefreshTopScores()
    {
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            if (PlayerPrefs.HasKey($"highScore_{i}"))
            {
                int score = PlayerPrefs.GetInt($"highScore_{i}");
                string date = PlayerPrefs.GetString($"highScoreDate_{i}");
                if (score > 0)
                {
                    scoreTexts[i].text = score.ToString();
                    dateTexts[i].text = date;
                }
                else
                {
                    scoreTexts[i].text = "----";
                    dateTexts[i].text = "----";
                }
                
            }
            else
            {
                scoreTexts[i].text = "----";
                dateTexts[i].text = "----";
            }
        }
    }

    public void ClearTopScores()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.DeleteKey($"highScore_{i}");
            PlayerPrefs.DeleteKey($"highScoreDate_{i}");
            PlayerPrefs.DeleteKey($"BestScore");

        }

        PlayerPrefs.Save();

        RefreshTopScores();
    }

}
