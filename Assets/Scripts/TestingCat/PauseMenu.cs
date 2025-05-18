using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private bool isGameOverScreen;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && !isGameOverScreen)
        {
            TogglePause();
        }
    }
    
    public void TogglePause()
    {
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            canvas.enabled = false;
        }
        else
        {
            Time.timeScale = 0f;
            canvas.enabled = true;
        }
    }

    public void ForcePause()
    {
        Time.timeScale = 0f;
        canvas.enabled = true;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}