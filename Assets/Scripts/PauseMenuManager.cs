using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public GameObject triggerObject;
    public Animation LeverDown;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == triggerObject)
            {
                TogglePauseMenu();
               
         
            }
           
        }

    }

    private void TogglePauseMenu()
    {
        if (pauseMenuPanel != null)
        {
            bool isPaused = !pauseMenuPanel.activeSelf;

            pauseMenuPanel.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1f;
        }
        else
        {
            Debug.LogWarning("No PauseMenuPanel assigned to PauseMenuManager.");
        }
    }

    // Function for the ResumeButton click
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuPanel.SetActive(false);
    }

    // Function for the MainMenuButton click
    public void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
        Time.timeScale = 1f;
    }

    // Function for the QuitGameButton click
    public void QuitGame()
    {
        Application.Quit();
    }
}
