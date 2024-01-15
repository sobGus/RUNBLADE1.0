using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    public int maxHealth = 3;
    private int currentHealth;

    public HealthBar healthBar; // Reference to the HealthBar script

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar(); // Update the health bar at the start
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if the player is defeated
        if (currentHealth <= 0)
        {
            Die();
        }

        UpdateHealthBar(); // Update the health bar after taking damage
    }

    private void Die()
    {
        Debug.Log("Player Defeated!");
        // Additional logic for player defeat, e.g., game over screen, level reset, etc.
        ResetHealth(); // Reset health for demonstration purposes

        SceneManager.LoadScene("TitleScene"); // Replace "TitleScene" with your actual title scene name
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth; // Reset health to maximum
        UpdateHealthBar(); // Update the health bar after resetting health
    }

    private void UpdateHealthBar()
    {
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }
}
