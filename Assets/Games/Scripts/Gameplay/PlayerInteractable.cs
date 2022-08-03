using System.Collections;
using System.Collections.Generic;
using TomGustin.GameDesignPattern;
using TomGustin.Interface;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractable : MonoBehaviour, ITriggerCollider
{
    [SerializeField] protected Sprite[] icons;
    [SerializeField] protected Image iconButton;
    [SerializeField] protected Button buttonInput;
    [SerializeField] protected IInteractable interactable;

    private CameraGadget gadget;

    private void Awake()
    {
        buttonInput.onClick.AddListener(() => Interact(GetComponent<PlayerController>()));

        gadget = ServiceLocator.Resolve<CameraGadget>();
        ChangeIcon(InteractableType.None);
    }

    public void Interact(PlayerController playerController)
    {
        if (interactable != null) interactable.Interact(playerController);
        else Camera();
    }

    private void Camera()
    {
        if (gadget.IsEquipped()) return;
        gadget.EquipGadget();
    }

    void ITriggerCollider.OnEnter(GameObject obj)
    {
        if (obj.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            this.interactable = interactable;

            ChangeIcon(interactable.GetInteractType());
        }
    }

    void ITriggerCollider.OnExit(GameObject obj)
    {
        if (obj.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            this.interactable = null;

            ChangeIcon(InteractableType.None);
        }
    }

    void ITriggerCollider.OnStay(GameObject obj)
    {
        
    }

    private void ChangeIcon(InteractableType type)
    {
        switch (type)
        {
            case InteractableType.Camera:
                iconButton.sprite = icons[0];
                break;
            case InteractableType.Chat:
                iconButton.sprite = icons[1];
                break;
            case InteractableType.None:
                iconButton.sprite = icons[2];
                break;
        }
    }
}
