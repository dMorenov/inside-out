using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int CurrentFailedActionsCount = 0;
    public int MaxFailedActionsCount = 3;
    
    public static Action OnActionCountChanged;
    public void Awake()
    {
    }
}
