using UnityEngine;

public enum InteractionResult
{
    None,
    Success,
    Failure
}

public class InteractableObject : MonoBehaviour, IInteractable
{
    public bool IsFakeObject = false;
    public bool AlreadyInteracted = false;
    
    
    
    public InteractionResult Interact()
    {
        if (AlreadyInteracted) return InteractionResult.None;
        
        Debug.Log("Interacting with " + gameObject.name);

        if (!IsFakeObject)
        {
            return InteractionResult.Failure;
        }
        
        AlreadyInteracted = true;
        return InteractionResult.Success;
    }

    public void SetAsFakeObject()
    {
        IsFakeObject = true;

        var rand = Random.Range(0, 100);
        if (rand < 50)
        {
            transform.Rotate(Vector3.up, 45f);
        }
        else
        {
            transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
        }
    }
}

public interface IInteractable
{
    InteractionResult Interact();
    void SetAsFakeObject();
}
