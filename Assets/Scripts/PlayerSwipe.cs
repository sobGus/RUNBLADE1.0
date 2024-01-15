using UnityEngine;

public class PlayerSwipe : MonoBehaviour
{
    public Animator animator;
    private bool isCooldown = false;
    private Vector2 startPos;
    private int currentAnimationFrame;
   // private int successfulSwipes = 0;


    void Update()
    {
        if (!isCooldown)
        {
            DetectSwipe();
        }
    }

    void DetectSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Button Down");
            startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse Button Up");
            Vector2 endPos = Input.mousePosition;
            HandleSwipe(startPos, endPos);
        }
        else if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("Touch Began");
                    startPos = touch.position;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    Debug.Log("Touch Ended");
                    Vector2 endPos = touch.position;
                    HandleSwipe(startPos, endPos);
                }
            }
        }
    }

      void HandleSwipe(Vector2 startPos, Vector2 endPos)
        {
            string swipeDirection = DetermineSwipeDirection(startPos, endPos);

            // Set the trigger based on the swipe direction
            SetAnimatorTrigger(swipeDirection);

            // Check if the swipe is within the specified frames of the enemy's attack animation
            if (swipeDirection != "" && IsSwipeWithinFrameRange(currentAnimationFrame, 3, 4))
            {
                // Trigger the enemy's stagger state
                if (EnemyController.Instance != null)
                {
                    // Check if the enemy is already staggered to avoid triggering stagger repeatedly
                    if (!EnemyController.Instance.IsStaggered())
                    {
                        // Trigger the stagger state in the enemy controller
                        EnemyController.Instance.TriggerStaggerState();
                    }
                }
            }

            // Start the cooldown coroutine
            StartCoroutine(StartCooldown());
        }


   void SetAnimatorTrigger(string triggerName)
{
    Debug.Log($"Setting trigger: {triggerName}");
    
    if(animator != null)
    {
        animator.SetTrigger(triggerName);
    }
    else
    {
        Debug.LogError("Animator is not assigned!");
    }
}

     // Animation event method called from the Animator when dealing damage to the enemy
    //public void DealDamageToEnemy()
   // {
        // Check if the enemy controller instance is valid
    //    if (EnemyController.Instance != null)
    //    {
            // Log that DealDamageToEnemy is called
      //      Debug.Log("DealDamageToEnemy called");

            // Trigger the DealDamageToEnemy logic in the enemy controller
    //        EnemyController.Instance.DealDamageToEnemy(1);
    //    }
  //  }

    
   // public void KillEnemy()

    // Implement logic for enemy death
    // ...

    // Reset successful swipes counter
   // successfulSwipes = 0;



    System.Collections.IEnumerator StartCooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(0.5f);
        isCooldown = false;

        // Reset the boolean parameters after cooldown
        animator.SetBool("IsSwipeRight", false);
        animator.SetBool("IsSwipeLeft", false);
        animator.SetBool("IsSwipeUp", false);
    }

    string DetermineSwipeDirection(Vector2 startPos, Vector2 endPos)
{
    float swipeThreshold = 50f; // Adjust the threshold as needed

    // Calculate the swipe direction based on the difference in positions
    Vector2 swipeVector = endPos - startPos;

    // Check if the swipe is mostly vertical
    if (Mathf.Abs(swipeVector.y) > Mathf.Abs(swipeVector.x))
    {
        // Swipe is mostly vertical
        if (swipeVector.y > swipeThreshold)
        {
            return "IsSwipeUp";
        }
    }
    else
    {
        // Swipe is mostly horizontal
        if (swipeVector.x > swipeThreshold)
        {
            return "IsSwipeRight";
        }
        else if (swipeVector.x < -swipeThreshold)
        {
            return "IsSwipeLeft";
        }
    }

    // If no significant swipe is detected, return an empty string
    return "";
}


    bool IsSwipeWithinFrame(int startFrame, int endFrame)
    {
        // Assuming the animation runs at 30 frames per second
        float animationTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
        float frameTime = animationTime * animator.GetCurrentAnimatorClipInfo(0)[0].clip.frameRate;

        // Check if the animation is in the attack state and if the frame is within the specified range
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("MonsterRightAttack") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("MonsterLeftAttack") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("MonsterOverheadAttack"))
        {
            // Check for frames 3 and 4 in the attack animations
            return (frameTime >= startFrame && frameTime <= endFrame) || (frameTime >= 3 && frameTime <= 4);
        }

        return false;
    }

      // Animation event method called from the Animator when checking if the enemy is staggered
    public void IsStaggered()
    {
        // Check if the enemy controller instance is valid
        if (EnemyController.Instance != null)
        {
            // Log that IsStaggered is called
            Debug.Log("IsStaggered called");

            // Trigger the IsStaggered logic in the enemy controller
            EnemyController.Instance.CheckStaggered();
        }
    }

         bool IsSwipeWithinFrameRange(int currentFrame, int startFrame, int endFrame)
        {
            return currentFrame >= startFrame && currentFrame <= endFrame;
        }

        // Animation event method called from the Animator when updating the current animation frame
        public void UpdateAnimationFrame(int frame)
        {
            currentAnimationFrame = frame;
        }
}
