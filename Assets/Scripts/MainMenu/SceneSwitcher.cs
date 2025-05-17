using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void NextScene(string targetScene)
    {
        SceneManager.LoadScene(targetScene);
    }
}