using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class RightHandController : MonoBehaviour
{
    //Player
    //public PlayerVRMovement player;
    public PlayerVRMovementCC playerCC;

    //Controller Actions
    public InputActionReference triggerAction;
    public InputActionReference gripAction;

    //Jump Action
    [SerializeField] private InputActionReference jumpAction;

    void Start()
    {
        //Action SetUp
        triggerAction.action.started += TriggerActionStarted;
        triggerAction.action.canceled += TriggerActionCanceled;
        gripAction.action.started += GripActionStarted;
        gripAction.action.canceled += GripActionCanceled;

        jumpAction.action.started += JumpActionStarted;
        jumpAction.action.canceled += JumpActionStarted;
    }

    private void OnDestroy()
    {
        triggerAction.action.started -= TriggerActionStarted;
        triggerAction.action.canceled -= TriggerActionCanceled;
        gripAction.action.started -= GripActionStarted;
        gripAction.action.canceled -= GripActionCanceled;

        jumpAction.action.started -= JumpActionStarted;
        jumpAction.action.canceled -= JumpActionStarted;
    }

    #region Controller Action Callbacks

    void TriggerActionStarted(InputAction.CallbackContext context)
    {

    }

    void TriggerActionCanceled(InputAction.CallbackContext context)
    {

    }

    void GripActionStarted(InputAction.CallbackContext context)
    {

    }

    void GripActionCanceled(InputAction.CallbackContext context)
    {

    }

    void JumpActionStarted(InputAction.CallbackContext context)
    {
        //player.Jump();
        playerCC.Jump();
    }

    void JumpActionCanceled(InputAction.CallbackContext context)
    {
        
    }

    #endregion
}
