using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static event Action<InteractionResult> OnInteractionResult;
    
    [SerializeField] private float rayDistance = 5f;
    [SerializeField] private Sprite normalCursorTexture;
    [SerializeField] private Sprite interactingCursorTexture;
    [SerializeField] private Image cursorImage;
    
    private IInteractable _currentInteractable;
    
    private void Update()
    {
        var interactable = GetInteractable();
        if (interactable != _currentInteractable)
        {
            _currentInteractable = interactable;
            ChangeCursor();
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentInteractable != null)
            {
                var result = _currentInteractable.Interact();
                OnInteractionResult?.Invoke(result);
            }
        }
    }

    private IInteractable GetInteractable()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            return hit.collider.GetComponent<IInteractable>();
        }

        return null;
    }

    private void ChangeCursor()
    {
        if (_currentInteractable == null)
        {
            cursorImage.sprite = normalCursorTexture;
        }
        else
        {
            cursorImage.sprite = interactingCursorTexture;
        }
    }
}
