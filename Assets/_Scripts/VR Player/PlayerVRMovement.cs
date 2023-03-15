using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerVRMovement : MonoBehaviour
{
    [Header("Hero")]
    [SerializeField] private HeroScriptableObject hero;

    [Header("VR References")]
    [SerializeField] private GameObject locomotionSystem;

    [Header("Player Movement")]
    [SerializeField] private float moveSpeed;

    private ActionBasedContinuousMoveProvider continuousPlayerMovement; //For locomotion system movement
    
    [Header("Slope Movement")]
    [SerializeField] private float maxSlopeAngle;
    [SerializeField] private float angleSpeed;
    [SerializeField] private float angleDownForce;

    private RaycastHit slopeHit; 

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private bool canJump;
    [SerializeField] private LayerMask whatIsGround;

    private XROrigin xrOrigin;
    private CapsuleCollider capsuleCollider;
    private Rigidbody body;

    private bool isGrounded => Physics.Raycast(new Vector2(transform.position.x, transform.position.y + 2.0f), Vector3.down, 2.0f + 0.2f, whatIsGround);

    [Header("Player Hurtbox")]
    [SerializeField] private CapsuleCollider playerHurtbox;

    [Header("Dash")]
    public float dashSpeed;
    public float activeTime;
    public float cooldownTime;
    private bool canDash;

    void Start()
    {
        GetStats();
        SetUp();
    }

    private void Update()
    {
        UpdateColliderToPlayer();
    }

    private void GetStats()
    {
        moveSpeed = hero.moveSpeed;
    }

    private void SetUp()
    {
        //Player Movement
        continuousPlayerMovement = locomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>();
        continuousPlayerMovement.moveSpeed = moveSpeed;

        //Jump
        xrOrigin = GetComponent<XROrigin>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        body = GetComponent<Rigidbody>();

        //Dash
        canDash = true;
    }

    private void UpdateColliderToPlayer()
    {
        var center = xrOrigin.CameraInOriginSpacePos;
        capsuleCollider.center = new Vector3(center.x, capsuleCollider.center.y, center.z);
        capsuleCollider.height = Mathf.Clamp(xrOrigin.CameraInOriginSpaceHeight, 2.0f, 2.0f);

        playerHurtbox.center = new Vector3(center.x, playerHurtbox.center.y, center.z);
        playerHurtbox.height = Mathf.Clamp(xrOrigin.CameraInOriginSpaceHeight, 2.0f, 2.0f);
    }

    #region Jump
    public void Jump()
    {
        if (isGrounded && canJump)
        {
            canJump = false;

            body.velocity = new Vector3(body.velocity.x, 0f, body.velocity.z);   //Reset Y velocity to ensure jumping the same height
            body.AddForce(transform.up * jumpForce);

            Invoke(nameof(JumpReset), jumpCooldown);
        }
    }

    private void JumpReset()
    {
        canJump = true;
    }
    #endregion

    #region Slope Movement
    private void HandleSlopeMovement()
    {
        if (OnSlope())
        {
            body.AddForce(GetSlopeDirection() * angleSpeed);

            if (body.velocity.y != 0)
                body.AddForce(Vector3.down * angleDownForce);
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(new Vector2(transform.position.x, transform.position.y + 2.0f), Vector3.down, out slopeHit, 2.0f + 0.2f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeDirection()
    {
        return Vector3.ProjectOnPlane(transform.forward, slopeHit.normal).normalized;
    }
    #endregion

    public void CastSpecialAbility()
    {
        if (canDash)
            StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        continuousPlayerMovement.moveSpeed = dashSpeed;
        canDash = false;

        yield return new WaitForSeconds(activeTime);

        continuousPlayerMovement.moveSpeed = moveSpeed;

        yield return new WaitForSeconds(cooldownTime);

        canDash = true;
    }
}
