using UnityEngine;
using UnityEngine.UI;
using TMPro; // Импортируем TextMeshPro

public class AirplaneManager : MonoBehaviour
{
    [System.Serializable]
    public class Airplane
    {
        public string airplaneName;
        public int unlockLevel;
    }

    public Airplane[] airplanes;
    public Button[] airplaneButtons;

    private int currentLevelIndex;

    void Start()
    {
        currentLevelIndex = PlayerPrefs.GetInt("levelIndex", 1);
        //currentLevelIndex = 16;

        // Получаем индекс выбранного самолета из PlayerPrefs
        int selectedAirplaneIndex = PlayerPrefs.GetInt("selectedAirplaneIndex", 0);

        for (int i = 0; i < airplanes.Length; i++)
        {
            int index = i;
            Button button = airplaneButtons[index];

            if (currentLevelIndex >= airplanes[index].unlockLevel)
            {
                button.GetComponentInChildren<TMP_Text>().text = "Select";
                button.onClick.AddListener(() => SelectAirplane(index));
                if (selectedAirplaneIndex == index)
                {
                    button.GetComponentInChildren<TMP_Text>().text = "Selected";
                }
            }
            else
            {
                button.GetComponentInChildren<TMP_Text>().text = "Unlocks at level " + airplanes[index].unlockLevel;
                button.interactable = false;
            }
        }
    }

    void SelectAirplane(int index)
    {
        for (int i = 0; i < airplaneButtons.Length; i++)
        {
            airplaneButtons[i].GetComponentInChildren<TMP_Text>().text = currentLevelIndex >= airplanes[i].unlockLevel ? "Select" : "Unlocks at level " + airplanes[i].unlockLevel;
        }

        airplaneButtons[index].GetComponentInChildren<TMP_Text>().text = "Selected";
        PlayerPrefs.SetInt("selectedAirplaneIndex", index);
    }
}
