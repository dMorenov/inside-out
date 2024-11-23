using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int CurrentFailedActionsCount = 0;
    public int MaxFailedActionsCount = 3;
    public int CurrentSuccessActionsCount = 0;
    public int ActionsForWin = 3;

    public DoorManager DoorManager;
    public GameObject OriginalInteractableParent;
    public GameObject MirrorInteractableParent;

    public GameObject WinPopup;
    public GameObject LosePopup;

    public GameObject[] DisableOnWin;
    
    private InteractableObject[] _originalInteractables;
    private InteractableObject[] _mirrorInteractables;
    
    public void Awake()
    {
        PlayerController.OnInteractionResult += HandleInteractionResult;
        DoorManager.Reset();
        
        _originalInteractables = OriginalInteractableParent.GetComponentsInChildren<InteractableObject>();
        _mirrorInteractables = MirrorInteractableParent.GetComponentsInChildren<InteractableObject>();

        foreach (var interactable in _originalInteractables)
        {
            Destroy(interactable);
        }
        
        ShuffleArray(_mirrorInteractables);
        
        var intCount = 0;
        foreach (var interactable in _mirrorInteractables)
        {
            interactable.SetAsFakeObject();
            intCount++;
            if (intCount >= ActionsForWin) break;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void HandleInteractionResult(InteractionResult interaction)
    {
        switch (interaction)
        {
            case InteractionResult.None:
                break;
            case InteractionResult.Success:
                OnSuccess();
                break;
            case InteractionResult.Failure:
                OnFailure();
                break;
        }
    }

    private void OnSuccess()
    {
        Debug.Log($"Success!");
        DoorManager.OpenSlot();
        CurrentSuccessActionsCount++;
        if (CurrentSuccessActionsCount >= ActionsForWin)
        {
            WinGame();
        }
        
    }
    
    private void OnFailure()
    {
        Debug.Log($"Failure!");
        CurrentFailedActionsCount++;
        if (CurrentFailedActionsCount >= MaxFailedActionsCount)
        {
            Debug.Log("Game Over");
            LosePopup.SetActive(true);
        }
    }

    private void WinGame()
    {
        Debug.Log("You win!");

        foreach (var interactable in _originalInteractables)
        {
            interactable.AlreadyInteracted = true;
        }
        
        foreach (var go in DisableOnWin)
        {
            go.SetActive(false);
        }
        
        WinPopup.SetActive(true);
    }
    
    private void ShuffleArray<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, array.Length);
            (array[randomIndex], array[i]) = (array[i], array[randomIndex]);
        }
    }
}
