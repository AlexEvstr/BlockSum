using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs; // Префабы препятствий
    [SerializeField] private int numberOfObstacles = 40; // Количество препятствий
    [SerializeField] private float minDistance = 2.0f; // Минимальное расстояние между объектами
    public GameObject plane; // Ссылка на объект самолёта

    private List<GameObject> spawnedObjects = new List<GameObject>();

    public void Initialize(List<GameObject> letters, float minDistance)
    {
        this.minDistance = minDistance;
        // Добавляем уже существующие буквы в список объектов, чтобы избежать их перекрытия
        spawnedObjects.AddRange(letters);
        SpawnObstacles();
    }

    private void SpawnObstacles()
    {
        for (int i = 0; i < numberOfObstacles; i++)
        {
            GameObject newObstacle = null;
            Vector2 newPosition;
            bool positionIsValid = false;

            int attempt = 0;
            int maxAttempts = 100; // Ограничение количества попыток

            while (!positionIsValid && attempt < maxAttempts)
            {
                newPosition = new Vector2(Random.Range(-30, 30), Random.Range(-30, 30));
                positionIsValid = true;

                foreach (var obj in spawnedObjects)
                {
                    if (Vector2.Distance(newPosition, obj.transform.position) < minDistance)
                    {
                        positionIsValid = false;
                        break;
                    }
                }

                // Проверка на перекрытие с самолётом
                if (positionIsValid && plane != null && Vector2.Distance(newPosition, plane.transform.position) < minDistance)
                {
                    positionIsValid = false;
                }

                if (positionIsValid)
                {
                    newObstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], newPosition, Quaternion.identity);
                }

                attempt++;
            }

            if (newObstacle != null)
            {
                spawnedObjects.Add(newObstacle);
            }
        }
    }
}