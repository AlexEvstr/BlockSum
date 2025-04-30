using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaler : MonoBehaviour
{
    public float scaleMultiplier = 0.5f; // уменьшение
    public float animationDuration = 0.1f;

    public void ScaleCallingButton()
    {
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;
        if (clickedObject != null)
        {
            StartCoroutine(ScaleButtonEffect(clickedObject.transform));
        }
    }

    private System.Collections.IEnumerator ScaleButtonEffect(Transform buttonTransform)
    {
        Vector3 originalScale = buttonTransform.localScale;
        Vector3 targetScale = originalScale * scaleMultiplier;
        float timer = 0f;

        // Уменьшение
        while (timer < animationDuration)
        {
            buttonTransform.localScale = Vector3.Lerp(originalScale, targetScale, timer / animationDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        buttonTransform.localScale = targetScale;

        // Возврат
        timer = 0f;
        while (timer < animationDuration)
        {
            buttonTransform.localScale = Vector3.Lerp(targetScale, originalScale, timer / animationDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        buttonTransform.localScale = originalScale;
    }
}
