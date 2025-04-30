using UnityEngine;

public class Arrow : MonoBehaviour
{
    private GameObject target;
    private Canvas canvas;
    private Camera mainCamera;
    private float borderOffset = 100f; // Увеличенный отступ от края экрана

    public void Initialize(GameObject targetObject, Canvas canvasObject)
    {
        target = targetObject;
        canvas = canvasObject;
        mainCamera = Camera.main;
    }

    public void CheckVisibilityAndMove()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(target.transform.position);
        bool isVisible = IsTargetVisible(viewportPoint);

        if (isVisible)
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
            MoveArrowToScreenEdge(viewportPoint);
        }
    }

    private bool IsTargetVisible(Vector3 viewportPoint)
    {
        return viewportPoint.z > 0 && viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
    }

    private void MoveArrowToScreenEdge(Vector3 viewportPoint)
    {
        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.transform.position);
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        // Преобразование позиции стрелки из мирового пространства в пространство Canvas
        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, mainCamera, out canvasPos);

        // Ограничение позиции стрелки границами Canvas с учетом отступа
        canvasPos.x = Mathf.Clamp(canvasPos.x, -canvasRect.sizeDelta.x / 2 + borderOffset, canvasRect.sizeDelta.x / 2 - borderOffset);
        canvasPos.y = Mathf.Clamp(canvasPos.y, -canvasRect.sizeDelta.y / 2 + borderOffset, canvasRect.sizeDelta.y / 2 - borderOffset);

        RectTransform arrowRect = GetComponent<RectTransform>();
        arrowRect.localPosition = canvasPos;

        Vector3 direction = (target.transform.position - mainCamera.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrowRect.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}
