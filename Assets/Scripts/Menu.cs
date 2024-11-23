using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
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
