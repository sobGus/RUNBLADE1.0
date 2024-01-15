using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject uiPanel;

    private void Update()
    {
        // Check for mouse click or touch
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits a collider
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the collider belongs to the specific GameObject
                if (hit.collider.CompareTag("YourSpecificTag"))
                {
                    // Show the UI panel
                    ShowUIPanel();
                }
            }
        }
    }

    public void ShowUIPanel()
    {
        // Enable the specified UI panel
        if (uiPanel != null)
        {
            uiPanel.SetActive(true);
            // Pause the game by setting timescale to 0f
            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogWarning("No UI panel assigned to UIManager.");
        }
    }

    public void CloseUIPanel()
    {
        // Disable the specified UI panel
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
            // Resume the game by setting timescale back to 1f
            Time.timeScale = 1f;
        }
        else
        {
            Debug.LogWarning("No UI panel assigned to UIManager.");
        }
    }
}
