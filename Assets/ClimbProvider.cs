using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbProvider : MonoBehaviour
{
    public static event Action ClimbActive;
    public static event Action ClimbInactive;

    public CharacterController characterController;
    public InputActionProperty velocityRight;
    public InputActionProperty velocityLeft;

    private bool rightHandActive = false;
    private bool leftHandActive = false;

    private void Start()
    {
        XRDirectClimbInteractor.ClimbHandActivated += HandActivated;
        XRDirectClimbInteractor.ClimbHandDeactivated += HandDeactivated;
    }

    private void OnDestroy()
    {
        XRDirectClimbInteractor.ClimbHandActivated -= HandActivated;
        XRDirectClimbInteractor.ClimbHandDeactivated -= HandDeactivated;
    }

    private void Update() // Switch to FixedUpdate() if it causes problems
    {
        if (rightHandActive || leftHandActive)
            Climb();
    }

    private void HandActivated(string controllerName)
    {
        if (controllerName == "LeftHand Controller")
        {
            leftHandActive = true;
            rightHandActive = false;
        }
        else
        {
            leftHandActive = false;
            rightHandActive = true;
        }

        ClimbActive?.Invoke();
    }

    private void HandDeactivated(string controllerName)
    {
        if (rightHandActive && controllerName == "RightHand Controller")
        {
            rightHandActive = false;
            ClimbInactive?.Invoke();
        }
        else if (leftHandActive && controllerName == "LeftHand Controller")
        {
            leftHandActive = false;
            ClimbInactive?.Invoke();
        }
    }

    private void Climb()
    {
        Vector3 velocity = leftHandActive ? velocityLeft.action.ReadValue<Vector3>() : velocityRight.action.ReadValue<Vector3>();
        characterController.Move(characterController.transform.rotation * -velocity * Time.fixedDeltaTime);
    }
}
