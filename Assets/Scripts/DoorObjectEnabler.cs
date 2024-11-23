using UnityEngine;

public class DoorObjectEnabler : MonoBehaviour
{
    public void Enable(bool isEnabled)
    {
        gameObject.SetActive(isEnabled);
    }
}
