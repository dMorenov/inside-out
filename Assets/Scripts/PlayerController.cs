using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static event Action<InteractionResult> OnInteractionResult;
    public static event Action OnWinCollider;
    
    [SerializeField] private float rayDistance = 5f;
    [SerializeField] private Sprite[] idleCursors;
    [SerializeField] private Sprite[] intCursors;
    [SerializeField] private Image cursorImage;
    
    private IInteractable _currentInteractable;
    private Coroutine _co;
    
    private bool _cursorIsAnimating = false;
    
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
            if (_co != null)
            {
                StopCoroutine(_co);
                _co = null;
            }
            _co = StartCoroutine(ChangeCursorDelayed());
            
            if (_currentInteractable != null)
            {
                var result = _currentInteractable.Interact();
                OnInteractionResult?.Invoke(result);
            }
        }
    }

    private IEnumerator ChangeCursorDelayed()
    {
        var interactable = _currentInteractable != null && !_currentInteractable.IsAlreadyInteracted();
        var spriteArray = interactable ? intCursors : idleCursors;
        
        cursorImage.sprite = spriteArray[1];

        _cursorIsAnimating = true;
        yield return new WaitForSeconds(0.2f);

        ChangeCursor();
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
        if (_currentInteractable != null && !_currentInteractable.IsAlreadyInteracted())
        {
            cursorImage.sprite = intCursors[0];
        }
        else
        {
            cursorImage.sprite = idleCursors[0];

        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WinCollider"))
        {
            OnWinCollider?.Invoke();
        }
    }
}
