using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public List<DoorObjectEnabler> _doors = new();
    
    private int _currentDoorIndex = 0;
    
    public void Awake()
    {
        Reset();
    }

    public void OpenSlot()
    {
        if (_doors.Count <= _currentDoorIndex)
        {
            Debug.LogError($"cant open more slots at index {_currentDoorIndex+1}");
            return;
        }

        Debug.Log($"openslot");
        _doors[_currentDoorIndex++].Enable(true);
    }

    public void Reset()
    {
        _currentDoorIndex = 0;
        foreach (var door in _doors)
        {
            door.Enable(false);
        }
    }
}
