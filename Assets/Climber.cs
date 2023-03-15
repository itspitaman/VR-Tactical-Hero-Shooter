using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climber : MonoBehaviour
{
    private CharacterController characterController;
    private bool isClimbing = false;

    private PlayerVRMovementCC playerMovementCC;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerMovementCC = GetComponent<PlayerVRMovementCC>();

        ClimbProvider.ClimbActive += ClimbActive;
        ClimbProvider.ClimbInactive += ClimbInactive;
    }

    private void OnDestroy()
    {
        ClimbProvider.ClimbActive -= ClimbActive;
        ClimbProvider.ClimbInactive -= ClimbInactive;
    }

    private void Update()
    {
        if (!characterController.isGrounded && !isClimbing)
        {
            //Dont need this probs?? also dont need boolean
        }
    }

    private void ClimbActive()
    {
        isClimbing = true;
        playerMovementCC.enabled = false;
    }

    private void ClimbInactive()
    {
        isClimbing = false;
        playerMovementCC.enabled = true;
    }
}
