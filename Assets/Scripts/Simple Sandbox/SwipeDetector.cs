using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeDetector : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 startPosition;
    private Vector2 endPosition;
    public Vector2 swipeDelta;

    public Transform PlayerCamera;
    public static SwipeDetector instance;

    private void Start()
    {
        instance = this;
    }
    public void OnBeginDrag(PointerEventData data)
    {
        startPosition = data.position;
    }

    public void OnDrag(PointerEventData data)
    {
        swipeDelta = (data.position - startPosition).normalized;
    }

    public void OnEndDrag(PointerEventData data)
    {
        endPosition = data.position;

        Vector2 swipe = endPosition - startPosition;

        if (swipe.magnitude > 50)
        {
            if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y)) 
            {
                if (swipe.x < 0) 
                {
                    Debug.Log("Swipe Left");
                }
                else
                {
                    Debug.Log("Swipe Right");
                }
            }
            else 
            {
                if (swipe.y < 0) 
                {
                    Debug.Log("Swipe Down");
                }
                else
                {
                    Debug.Log("Swipe Up");
                }
            }
        }
        swipeDelta = Vector2.zero;
    }
}