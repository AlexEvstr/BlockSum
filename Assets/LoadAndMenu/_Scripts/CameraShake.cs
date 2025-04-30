using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public Camera targetCamera; // Сериализованное поле для камеры
    public float shakeDuration = 0.5f; // Длительность тряски
    public float shakeMagnitude = 0.1f; // Амплитуда тряски

    private Vector3 initialPosition;

    void Start()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main; // Если камера не назначена, используем основную камеру
        }
    }

    public void TriggerShake()
    {
        initialPosition = targetCamera.transform.localPosition; // Сохраняем начальную позицию камеры перед началом тряски
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < shakeDuration)
        {
            float xOffset = Random.Range(-1f, 1f) * shakeMagnitude;
            float yOffset = Random.Range(-1f, 1f) * shakeMagnitude;

            targetCamera.transform.localPosition = new Vector3(initialPosition.x + xOffset, initialPosition.y + yOffset, initialPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Возвращаем камеру в начальную позицию
        targetCamera.transform.localPosition = initialPosition;
    }
}
