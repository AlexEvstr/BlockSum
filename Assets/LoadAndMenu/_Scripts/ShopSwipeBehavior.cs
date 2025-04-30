using UnityEngine;

public class ShopSwipeBehavior : MonoBehaviour
{
    public GameObject[] objects;
    public RectTransform swipeArea;
    public float minSwipeDistance = 50f;

    private int currentIndex = 0;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isSwiping;

    void Start()
    {
        UpdateActiveObject();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector2 localPoint;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(swipeArea, touch.position, Camera.main, out localPoint))
                {
                    if (swipeArea.rect.Contains(localPoint))
                    {
                        isSwiping = true;
                        startTouchPosition = touch.position;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended && isSwiping)
            {
                endTouchPosition = touch.position;
                isSwiping = false;
                DetectSwipe();
            }
        }
    }

    void DetectSwipe()
    {
        if (Vector2.Distance(startTouchPosition, endTouchPosition) >= minSwipeDistance)
        {
            Vector2 direction = endTouchPosition - startTouchPosition;
            Vector2 swipeDirection = direction.normalized;

            if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
            {
                if (swipeDirection.x > 0)
                {
                    OnSwipeLeft();
                }
                else
                {
                    OnSwipeRight();
                }
            }
        }
    }

    void OnSwipeRight()
    {
        currentIndex = (currentIndex + 1) % objects.Length;
        UpdateActiveObject();
    }

    void OnSwipeLeft()
    {
        currentIndex = (currentIndex - 1 + objects.Length) % objects.Length;
        UpdateActiveObject();
    }

    void UpdateActiveObject()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i == currentIndex);
        }
    }
}
