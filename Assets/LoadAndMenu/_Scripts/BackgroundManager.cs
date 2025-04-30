using UnityEngine;
using UnityEngine.UI;
using TMPro; // Импортируем TextMeshPro

public class BackgroundManager : MonoBehaviour
{
    [System.Serializable]
    public class Background
    {
        public string backgroundName;
        public int unlockLevel;
    }

    public Background[] backgrounds;
    public Button[] backgroundButtons;

    private int currentLevelIndex;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Sprite[] _backgroundSprites;

    void Start()
    {
        currentLevelIndex = PlayerPrefs.GetInt("levelIndex", 1);
        //currentLevelIndex = 16;
        _backgroundImage.sprite = _backgroundSprites[PlayerPrefs.GetInt("selectedBackgroundIndex", 0)];

        // Получаем индекс выбранного фона из PlayerPrefs
        int selectedBackgroundIndex = PlayerPrefs.GetInt("selectedBackgroundIndex", 0);

        for (int i = 0; i < backgrounds.Length; i++)
        {
            int index = i;
            Button button = backgroundButtons[index];

            if (currentLevelIndex >= backgrounds[index].unlockLevel)
            {
                button.GetComponentInChildren<TMP_Text>().text = "Select";
                button.onClick.AddListener(() => SelectBackground(index));
                if (selectedBackgroundIndex == index)
                {
                    button.GetComponentInChildren<TMP_Text>().text = "Selected";
                }
            }
            else
            {
                button.GetComponentInChildren<TMP_Text>().text = "Unlocks at level " + backgrounds[index].unlockLevel;
                button.interactable = false;
            }
        }
    }

    void SelectBackground(int index)
    {
        for (int i = 0; i < backgroundButtons.Length; i++)
        {
            backgroundButtons[i].GetComponentInChildren<TMP_Text>().text = currentLevelIndex >= backgrounds[i].unlockLevel ? "Select" : "Unlocks at level " + backgrounds[i].unlockLevel;
        }

        backgroundButtons[index].GetComponentInChildren<TMP_Text>().text = "Selected";
        PlayerPrefs.SetInt("selectedBackgroundIndex", index);
        _backgroundImage.sprite = _backgroundSprites[int.Parse(backgrounds[index].backgroundName)];
    }
}
