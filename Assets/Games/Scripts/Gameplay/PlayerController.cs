using MonsterLove.StateMachine;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TomGustin.GameDesignPattern;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float turnSpeed;

    [Header("References")]
    [SerializeField] private PlayerInteractable playerInteractable;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform playerIcon;

    [Header("Debug")]
    [SerializeField, ReadOnly] private PlayerState currentState; 

    private StateMachine<PlayerState> state;

    private Vector2 inputAxis;
    private Vector3 iconAngleBase;

    private CameraController cameraController;

    public enum PlayerState { None, Normal, InDialogue}

    private void Awake()
    {
        cameraController = ServiceLocator.Resolve<CameraController>();
        state = StateMachine<PlayerState>.Initialize(this);
        state.Changed += (currentState) => this.currentState = currentState;
        iconAngleBase = playerIcon.eulerAngles;
    }

    public void InitPlayer()
    {
        state.ChangeState(PlayerState.Normal);
    }

    public void Pause(bool pause)
    {
        if (pause)
        {
            state.ChangeState(PlayerState.InDialogue);
        } else
        {
            state.ChangeState(PlayerState.Normal);
        }
    }

    private void ReadInput()
    {
        inputAxis.x = InputManager.GetAxis("Player-Horizontal");
        inputAxis.y = InputManager.GetAxis("Player-Vertical");

        if (InputManager.GetButtonDown("Interact"))
        {
            playerInteractable.Interact(this);
        }
    }

    private void Move()
    {
        if (inputAxis.sqrMagnitude >= .01f)
        {
            Vector2 rotatePoint = (inputAxis * turnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + rotatePoint.x, 0f);

            Vector3 movementPoint = (inputAxis.y * walkSpeed * Time.deltaTime) * transform.forward;
            bool isValid = NavMesh.SamplePosition(transform.position + movementPoint, out NavMeshHit hit, 0.3f, NavMesh.AllAreas);
            
            if (isValid) transform.position = hit.position;
            if (inputAxis.y > 0) cameraController.RotateToPivot();

            playerIcon.eulerAngles = iconAngleBase;
        }
    }

    private void Animation()
    {
        anim.SetFloat("state", inputAxis.magnitude * 0.5f);
    }

    #region States
    private void Normal_Update()
    {
        ReadInput();

        Move();

        Animation();
    }

    private void InDialogue_Enter()
    {
        inputAxis = Vector2.zero;
        anim.SetFloat("state", 0f);
    }

    #endregion
}
