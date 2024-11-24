using System;
using System.Collections.Generic;
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

    public Popup WinPopup;
    public Popup LosePopup;
    public Popup MenuPopup;

    public GameObject[] DisableOnWin;
    public GameObject[] EnableOnWin;

    public GameObject actionsPrefab;
    public Transform actionsContainer;

    private List<GameObject> _actions = new();
    
    private InteractableObject[] _originalInteractables;
    private InteractableObject[] _mirrorInteractables;
    public InteractableObject[] _forcedInteractables;
    
    private bool _allSolved = false;
    
    public void Awake()
    {
        PlayerController.OnInteractionResult += HandleInteractionResult;
        PlayerController.OnWinCollider += ShowWinPopup;
        DoorManager.Reset();
        
        _originalInteractables = OriginalInteractableParent.GetComponentsInChildren<InteractableObject>();
        _mirrorInteractables = MirrorInteractableParent.GetComponentsInChildren<InteractableObject>();

        _actions.Clear();
        for (int i = 0; i < MaxFailedActionsCount; i++)
        {
            var item = Instantiate(actionsPrefab, actionsContainer);
            item.SetActive(true);
            _actions.Add(item);
        }

        foreach (var interactable in _originalInteractables)
        {
            Destroy(interactable);
        }
        
        foreach (var interactable in _forcedInteractables)
        {
            Destroy(interactable);
        }
        
        ShuffleArray(_mirrorInteractables);
        
        var intCount = 2;
        foreach (var interactable in _mirrorInteractables)
        {
            interactable.SetAsFakeObject();
            intCount++;
            if (intCount >= ActionsForWin) break;
        }
    }

    private void Reset()
    {
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuPopup.Set(true);
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
        AudioManager.Instance.PlaySuccess();
        DoorManager.OpenSlot();
        CurrentSuccessActionsCount++;
        if (CurrentSuccessActionsCount >= ActionsForWin)
        {
            OnAllItemsSolved();
        }
    }
    
    private void OnFailure()
    {
        AudioManager.Instance.PlayClip(AudioManager.Instance.failClip);
        Debug.Log($"Failure! {CurrentFailedActionsCount}, _actions.Count: {_actions.Count}");
        Destroy(_actions[0]);
        _actions.RemoveAt(0);
        
        CurrentFailedActionsCount++;
        if (CurrentFailedActionsCount >= MaxFailedActionsCount)
        {
            Debug.Log("Game Over");
            LosePopup.Set(true);
            AudioManager.Instance.PlayEnding(AudioManager.Instance.badEnding);
        }
    }

    private void OnAllItemsSolved()
    {
        Debug.Log("You win!");

        foreach (var interactable in _originalInteractables)
        {
            interactable.AlreadyInteracted = true;
        }
        
        foreach (var interactable in _mirrorInteractables)
        {
            interactable.AlreadyInteracted = true;   
        }
        
        foreach(var go in EnableOnWin)
        {
            go.SetActive(true);
        }
    }

    public void ShowWinPopup()
    {
        foreach (var go in DisableOnWin)
        {
            go.SetActive(false);
        }
        
        WinPopup.Set(true);
        AudioManager.Instance.PlayEnding(AudioManager.Instance.goodEnding);
    }
    
    public void ReturnToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    private void OnDestroy()
    {
        PlayerController.OnInteractionResult -= HandleInteractionResult;
        PlayerController.OnWinCollider -= ShowWinPopup;
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
