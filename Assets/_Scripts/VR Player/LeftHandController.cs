using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class LeftHandController : MonoBehaviour
{
    //Player
    //public PlayerVRMovement player;
    public PlayerVRMovementCC playerCC;

    //Controller Actions
    public InputActionReference triggerAction;
    public InputActionReference gripAction;

    void Start()
    {
        //Action SetUp
        triggerAction.action.started += TriggerActionStarted;
        triggerAction.action.canceled += TriggerActionCanceled;
        gripAction.action.started += GripActionStarted;
        gripAction.action.canceled += GripActionCanceled;
    }

    private void OnDestroy()
    {
        triggerAction.action.started -= TriggerActionStarted;
        triggerAction.action.canceled -= TriggerActionCanceled;
        gripAction.action.started -= GripActionStarted;
        gripAction.action.canceled -= GripActionCanceled;
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
        //player.CastSpecialAbility();
        playerCC.CastSpecialAbility();
    }

    void GripActionCanceled(InputAction.CallbackContext context)
    {

    }

    #endregion
}
