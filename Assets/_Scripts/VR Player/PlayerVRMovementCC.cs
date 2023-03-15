using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerVRMovementCC : MonoBehaviour
{
    [Header("Hero")]
    [SerializeField] private HeroScriptableObject hero;

    [Header("VR References")]
    [SerializeField] private GameObject locomotionSystem;

    private XROrigin xrOrigin;

    [Header("Player Hurtbox")]
    [SerializeField] private CapsuleCollider playerHurtbox;

    [Header("Imput References")]
    [SerializeField] private XRNode inputSource;

    private Vector2 inputAxis;

    [Header("Player Movement")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float gravity = -9.81f;

    private float fallingSpeed;

    [Header("Jump")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float jumpTime;
    [SerializeField] private float jumpGravity;

    private bool isGrounded;
    private bool canJump;
    private bool isJumping;

    [Header("Dash")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float activeTime;
    [SerializeField] private float cooldownTime;

    private bool canDash;

    void Start()
    {
        GetStats();
        SetUp();
    }

    private void Update()
    {
        GetPlayerMovementInput();
        MovePlayer();

        isGrounded = GroundCheck();

        UpdateColliderToPlayer();
        UpdateCCToPlayer();
    }

    #region SetUp
    private void GetStats()
    {
        moveSpeed = hero.moveSpeed;
    }

    private void SetUp()
    {
        //Hurtbox
        xrOrigin = GetComponent<XROrigin>();

        //Jump
        canJump = true;
        isJumping = false;

        //Dash
        canDash = true;
    }
    #endregion

    #region Player Movement
    private void GetPlayerMovementInput()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    private void MovePlayer()
    {
        Quaternion headYaw = Quaternion.Euler(0, xrOrigin.Camera.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        characterController.Move(direction * Time.fixedDeltaTime * moveSpeed);

        if (isGrounded && !isJumping)
        {
            fallingSpeed = 0;
            characterController.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
        }
        else
        {
            fallingSpeed += gravity * Time.fixedDeltaTime;
            characterController.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
        }
    }
    #endregion

    #region Colliders
    private void UpdateColliderToPlayer()
    {
        var center = xrOrigin.CameraInOriginSpacePos;

        playerHurtbox.center = new Vector3(center.x, playerHurtbox.height / 2, center.z);
        playerHurtbox.height = Mathf.Clamp(xrOrigin.CameraInOriginSpaceHeight, 1.0f, 2.0f);
    }

    private void UpdateCCToPlayer()
    {
        Vector3 center = xrOrigin.CameraInOriginSpacePos;

        characterController.center = new Vector3(center.x, characterController.height / 2, center.z);
        characterController.height = Mathf.Clamp(xrOrigin.CameraInOriginSpaceHeight, 1.0f, 2.0f);
    }
    #endregion

    #region Jump
    private bool GroundCheck()
    {
        Vector3 ray = transform.TransformPoint(characterController.center);
        float rayLenght = characterController.center.y + 0.02f;
        bool hit = Physics.SphereCast(ray, characterController.radius, Vector3.down, out RaycastHit hitInfo, rayLenght, whatIsGround);
        return hit;
    }

    public void Jump()
    {
        if (isGrounded && canJump)
            StartCoroutine(Jumping());
    }

    IEnumerator Jumping()
    {
        float currentGravity = gravity;

        canJump = false;
        isJumping = true;
        gravity = jumpGravity;

        yield return new WaitForSeconds(jumpTime);

        canJump = true;
        isJumping = false;
        gravity = currentGravity;
    }
    #endregion

    #region Dash
    public void CastSpecialAbility()
    {
        if (canDash)
            StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        moveSpeed = dashSpeed;
        canDash = false;

        yield return new WaitForSeconds(activeTime);

        moveSpeed = hero.moveSpeed;

        yield return new WaitForSeconds(cooldownTime);

        canDash = true;
    }
    #endregion

    // Utility Methods --------------------------------------------

    IEnumerator Cooldown(float cd)
    {
        yield return new WaitForSeconds(cd);
    }
}
