using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    public GameObject[] hearts;
    private CountdownTimer _countdownTimer;

    private int currentLives;

    void Start()
    {
        _countdownTimer = GetComponent<CountdownTimer>();
        currentLives = hearts.Length; // Инициализация количества жизней
    }

    public void TakeDamage()
    {
        if (currentLives > 0)
        {
            currentLives--;
            hearts[currentLives].SetActive(false); // Выключаем объект сердечка

            if (currentLives <= 0)
            {
                _countdownTimer.LoseBehavior();
            }
        }
    }
}