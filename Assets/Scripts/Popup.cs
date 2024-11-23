using UnityEngine;
public class Popup : MonoBehaviour
{
    public void Set(bool isActive)
    {
        gameObject.SetActive(isActive);
        // showcursor
        Cursor.visible = isActive;
        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
