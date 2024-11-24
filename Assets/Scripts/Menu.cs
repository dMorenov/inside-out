using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
