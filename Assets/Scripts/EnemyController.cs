using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance; // Singleton instance
    public Transform startingPosition; // Set this in the inspector to the desired starting position
    public int maxHealth = 3;
    private int currentHealth;
    public float respawnDelay = 4f;
    // Flag to track if the enemy is in the staggered state and can take damage
    public Animator animator;  // Reference to the Animator component
    private bool isStaggered = false;
    private bool canTakeDamage = false;
    public GameObject Quad;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();  // Get the Animator component on the same GameObject

        // Trigger the approach animation
        animator.SetTrigger("MonsterApproach");

        // Set a random attack choice after the approach animation starts
        SetRandomAttackChoice();

        Instance = this;
    }

    void Update()
    {
        // Add logic for enemy behavior (e.g., movement, animation updates) here
    }
    
     public void EnableDamageForPlayer()
    {
        canTakeDamage = true;
        Debug.Log("Player can now deal damage to the enemy during staggered state.");
    }

    public void TakeDamage(int damage)
    {
        if (IsStaggered())
        {
            StaggeredTakeDamage(damage);
        }
        else
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        // Implement actions when the enemy dies
        gameObject.SetActive(false);
        FindObjectOfType<ScoreManager>().AddPointsOnEnemyDestroyed(100);
        ResetEnemy();
        animator.ResetTrigger("StaggerTrigger");
       // FindObjectOfType<ScoreManager>(),
       // Invoke("Respawn", 4f); // Respawn after 2 seconds (you can adjust this duration)
    

       // Respawn();
    }

    public bool IsStaggered()
    {
        return isStaggered;
    }
     
    public void SetCanTakeDamage(bool value)
    {
        canTakeDamage = value;
    }

    void SetStaggerTrigger()
    {
        animator.SetTrigger("StaggerTrigger");
    }

    void ResetStaggerTrigger()
    {
        animator.ResetTrigger("StaggerTrigger");
    }

    void StaggeredTakeDamage(int damage)
    {
        // Implement logic for handling damage when the enemy is staggered
        // For example, additional effects or instant defeat
        Die();
    }

    void Awake()
    {
        Instance = this;
    }
    
     // Called from animation events to check if the enemy is staggered
    public void IsStaggeredFromAnimationEvent()
    {
        // Implement logic to check if the enemy is staggered and transition to the staggered state
        if (IsStaggered())
        {
            animator.SetTrigger("MonsterStaggered");
        }
    }


    public void CheckStaggered()
    {
        // Implement logic to check if the enemy is staggered and transition to the staggered state
        if (IsStaggered())
        {
            animator.SetTrigger("MonsterStaggered");
        }
    }
 
     // Implement logic for enemy death during staggered state
    private void KillEnemy()
    {
        Debug.Log("Enemy killed during staggered state");
        // Additional logic for killing the enemy, e.g., play death animation, update score, etc.
        // ...

        // Respawn after a delay
       // StartCoroutine(RespawnAfterDelay());
    }

   // Coroutine to respawn the enemy after a delay
   // System.Collections.IEnumerator RespawnAfterDelay()
   // {
        // Wait for the specified delay
        //yield return new WaitForSeconds(respawnDelay);

        // Respawn the enemy at the starting position
       // Respawn();
   // }

     // Called from animation events at frame 7 of attack animations
    public void DealDamageToPlayer()
    {

         // Deal damage to the player (assumed method, replace it with your actual logic)
            PlayerHealth.Instance.TakeDamage(1);  // Replace with your player's health management logic
            
        if (canTakeDamage)
        {
           

            // Check if the enemy should be killed
            if (currentHealth <= 0)
            {
                KillEnemy();
            }
        }
    }

    // public void DealDamageToEnemy(int damage)
 //   {
        // Implement logic to deal damage to the enemy
   //     if (isStaggered && canTakeDamage)
  //      {
   //         currentHealth -= damage;

  //          if (currentHealth <= 0)
  //          {
  //              Die();
  //          }
  //      }
  //  }

      // Called from animation events to enter the staggered state
    public void EnterStaggeredState()
    {
        Debug.Log("EnterStaggeredState called");
        isStaggered = true;
        canTakeDamage = true;  // Set to true when entering the staggered state
        // Additional logic if needed

        // Trigger the staggered state in the Animator
        animator.SetTrigger("MonsterStaggered");
    }

    // Called from animation events to reset the staggered state
    public void ResetStaggeredState()
    {
        Debug.Log("ResetStaggeredState called");
        isStaggered = false;
        canTakeDamage = false;  // Set to false when resetting the staggered state
        // Additional logic if needed
    }
    
     // Method to check if the enemy can take damage
    public bool CanTakeDamage()
    {
        return canTakeDamage;
    }

      // Called from animation events to trigger the staggered state
    public void TriggerStaggerState()
    {
        Debug.Log("Stagger triggered");

        // Additional logic if needed before transitioning to staggered state

        // Set the animator trigger to transition to MonsterStaggered state
        animator.SetTrigger("StaggerTrigger");
    }

    void SetRandomAttackChoice()
    {
        // Set a random value between 1 and 3 for the RandomAttackChoice parameter
        int randomAttack = Random.Range(1, 4);
        animator.SetInteger("RandomAttackChoice", randomAttack);

        // Add other methods and functionality as needed
    }

 void ResetEnemy()
{
    // Reset health
    currentHealth = maxHealth;

    // Reset staggered state and set it to the initial state
    isStaggered = false;
    animator.ResetTrigger("StaggerTrigger");
    animator.ResetTrigger("MonsterStaggered");

    // Reset any other triggers or states that need to be initialized
    animator.ResetTrigger("AnyOtherTrigger");

    // Set the initial state or trigger
    animator.SetTrigger("MonsterApproach");

    // Reset position and rotation
    transform.position = startingPosition.position;
    transform.rotation = startingPosition.rotation;

    // Enable the existing enemy GameObject
    gameObject.SetActive(true);

    SetRandomAttackChoice();
}





    
}
