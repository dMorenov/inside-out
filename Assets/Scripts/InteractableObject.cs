using UnityEngine;

public enum InteractionResult
{
    None,
    Success,
    Failure
}

public class InteractableObject : MonoBehaviour, IInteractable
{
    public bool AlreadyInteracted { get; set; } = true;
    
    public InteractionResult Interact()
    {
        if (!AlreadyInteracted) return InteractionResult.None;
        
        Debug.Log("Interacting with " + gameObject.name);

        return InteractionResult.Success;
    }
}

public interface IInteractable
{
    InteractionResult Interact();
}
