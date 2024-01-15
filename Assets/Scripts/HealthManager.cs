using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image hpBarImage;
    public int maxHits = 3;

    private int currentHits;

    private void Start()
    {
        currentHits = maxHits;
        UpdateHPBar();
    }

    // Function to handle damage
    public void TakeDamage()
    {
        if (currentHits > 0)
        {
            currentHits--;
            UpdateHPBar();

            if (currentHits == 0)
            {
                // Handle player death or any other relevant logic
                Debug.Log("Player has run out of HP!");
            }
        }
    }

    // Function to update the HP bar visually
    private void UpdateHPBar()
    {
        float fillAmount = (float)currentHits / maxHits;
        hpBarImage.fillAmount = fillAmount;
    }
}
