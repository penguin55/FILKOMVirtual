using UnityEngine;
using TomGustin.Interface;
using TomGustin.GameDesignPattern;
using UnityEngine.Events;

public class CameraTaskField : MonoBehaviour, IInteractable
{
    [SerializeField] private PenugasanDataValidator dataValidator;
    [SerializeField] private UnityEvent onClearCapture;
    [SerializeField] private Transform targetLookup;

    private CameraGadget _cameraGadget;
    private CameraGadget cameraGadget 
    {
        get
        {
            if (_cameraGadget == null) _cameraGadget = ServiceLocator.Resolve<CameraGadget>();
            return _cameraGadget;
        }
    }

    public void ClearCapture()
    {
        onClearCapture?.Invoke();
        dataValidator.Clear();
    }

    InteractableType IInteractable.GetInteractType()
    {
        return InteractableType.Camera;
    }

    void IInteractable.Interact(PlayerController controller)
    {
        if (!dataValidator.Active || dataValidator.Cleared) return;
        cameraGadget.EquipGadget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!dataValidator.Active || dataValidator.Cleared) return;
        if (other.TryGetComponent<ITriggerCollider>(out ITriggerCollider triggerCollider))
        {
            triggerCollider.OnEnter(gameObject);
            cameraGadget.SetCameraTargetDetection(targetLookup, this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ITriggerCollider>(out ITriggerCollider triggerCollider))
        {
            triggerCollider.OnExit(gameObject);
            cameraGadget.SetCameraTargetDetection(null, null);
        }
    }
}
