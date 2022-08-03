using MonsterLove.StateMachine;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float rotationSpeed;

    [Header("References")]
    [SerializeField] private Transform cameraPlayer;

    [Header("Debug")]
    [SerializeField, ReadOnly] private CameraState currentState;

    public enum CameraState { None, Player, Map}

    private bool autoRotate;

    private StateMachine<CameraState> state;

    private Vector2 inputAxis;

    private void Awake()
    {
        state = StateMachine<CameraState>.Initialize(this);
        state.Changed += (currentState) => this.currentState = currentState;
        state.ChangeState(CameraState.Player);
    }

    private void ReadInputPlayer()
    {
        inputAxis.x = InputManager.GetAxis("Camera-Horizontal");
        inputAxis.y = InputManager.GetAxis("Camera-Vertical");

        if (inputAxis != Vector2.zero) autoRotate = false;
        else autoRotate = true;
    }

    private void MoveCamera()
    {
        Vector2 rotationPoint = inputAxis * rotationSpeed * Time.deltaTime;

        cameraPlayer.rotation = Quaternion.Euler(cameraPlayer.rotation.eulerAngles.x + rotationPoint.y, cameraPlayer.rotation.eulerAngles.y + rotationPoint.x, 0f);
    }


    public void RotateToPivot()
    {
        if (!autoRotate) return;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(Vector3.zero), Time.deltaTime * 2f);
    }
    #region States
    private void Player_Update()
    {
        ReadInputPlayer();

        MoveCamera();
    }
    #endregion
}
