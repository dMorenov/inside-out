using UnityEngine;
using UnityEngine.Serialization;

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
    
    public bool MoveOnX = false;
    public bool MoveOnZ = false;
    
    public GameObject disableObject;
    
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
        
        if (disableObject != null)
        {
            disableObject.SetActive(false);
            return;
        }

        if (MoveOnX && MoveOnZ)
        {
            var r = Random.Range(0, 10);
            if (r < 5)
            {
                MoveOnX = true;
                MoveOnZ = false;
            }
            else
            {
                MoveOnX = false;
                MoveOnZ = true;
            }
        }
        
        if (MoveOnX)
        {
            transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
            return;
        }
        
        if (MoveOnZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f);
            return;
        }
        
        var scaleRand = Random.Range(0.7f, 1.2f);
        transform.localScale = new Vector3(transform.localScale.x * scaleRand, transform.localScale.y, transform.localScale.z * scaleRand);
    }

    public bool IsAlreadyInteracted()
    {
        return AlreadyInteracted;
    }
}

public interface IInteractable
{
    InteractionResult Interact();
    void SetAsFakeObject();
    bool IsAlreadyInteracted();
}
