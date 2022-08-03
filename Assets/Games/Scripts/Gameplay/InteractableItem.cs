using System.Collections;
using System.Collections.Generic;
using TomGustin.Interface;
using UnityEngine;
using UnityEngine.Events;

public class InteractableItem : MonoBehaviour, IInteractable
{
    [SerializeField] private PenugasanDataValidator dataValidator;
    [SerializeField] private InteractableType interactType;
    [SerializeField] private UnityEvent onInteract;
    [SerializeField] private UnityEvent onInteractTriggerPOI;

    InteractableType IInteractable.GetInteractType()
    {
        return interactType;
    }

    void IInteractable.Interact(PlayerController controller)
    {
        onInteract?.Invoke();

        if (!dataValidator.Active || dataValidator.Cleared) return;
        onInteractTriggerPOI?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ITriggerCollider>(out ITriggerCollider triggerCollider))
        {
            triggerCollider.OnEnter(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ITriggerCollider>(out ITriggerCollider triggerCollider))
        {
            triggerCollider.OnExit(gameObject);
        }
    }
}
