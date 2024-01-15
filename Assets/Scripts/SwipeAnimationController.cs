using UnityEngine;

public class SwipeAnimationController : MonoBehaviour
{
    public GameObject controlledObject;
    public Animator animator;

    private Vector2 startPos;
    public float minSwipeDistance = 50f;
    public float maxSwipeAngle = 30f;
    public float minSwipeUpAngle = 60f;
    public float maxSwipeUpAngle = 120f;

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 swipeDelta = touch.position - startPos;

                if (swipeDelta.magnitude > minSwipeDistance)
                {
                    float angle = Vector2.Angle(swipeDelta, Vector2.right);

                    if (angle < maxSwipeAngle)
                    {
                        // Swipe to the right
                        EnableAndPlayAnimation("SwordRight");
                    }
                    else if (angle > (180 - maxSwipeAngle))
                    {
                        // Swipe to the left
                        EnableAndPlayAnimation("SwordLeft");
                    }
                    else if (angle > minSwipeUpAngle && angle < maxSwipeUpAngle)
                    {
                        // Swipe upwards
                        EnableAndPlayAnimation("SwordUp");
                    }
                }
            }
        }

        // Check for mouse input in the Unity Editor
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Vector2 swipeDelta = (Vector2)Input.mousePosition - startPos;

            if (swipeDelta.magnitude > minSwipeDistance)
            {
                float angle = Vector2.Angle(swipeDelta, Vector2.right);

                if (angle < maxSwipeAngle)
                {
                    // Swipe to the right
                    EnableAndPlayAnimation("SwordRight");
                }
                else if (angle > (180 - maxSwipeAngle))
                {
                    // Swipe to the left
                    EnableAndPlayAnimation("SwordLeft");
                }
                else if (angle > minSwipeUpAngle && angle < maxSwipeUpAngle)
                {
                    // Swipe upwards
                    EnableAndPlayAnimation("SwordUp");
                }
            }
        }
    }

    void EnableAndPlayAnimation(string animationName)
    {
        // Enable the controlled object
        controlledObject.SetActive(true);

        // Check if the Animator component is assigned
        if (animator != null)
        {
            // Trigger the respective animation
            animator.SetTrigger(animationName);
        }
        else
        {
            Debug.LogError("Animator not assigned to the script.");
        }
    }
}
