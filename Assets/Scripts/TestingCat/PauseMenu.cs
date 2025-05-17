using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
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
}