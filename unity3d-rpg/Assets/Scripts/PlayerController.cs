using NUnit.Framework;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Joystick joystickLeft, joystickRight;
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] Animator animator;
    public Vector3 jumpDir;
    public float dirZ;
    public float jumpForce;
    public Rigidbody rb;
    public bool isGrounded;
    public bool isSwordEquiped;
    public float lastAttackTime;
    public bool isAttacking;
    void FixedUpdate()
    {
        float angleY = joystickRight.Horizontal * turnSpeed * Time.fixedDeltaTime;
        float dirZ = joystickLeft.Vertical;
        transform.Rotate(new Vector3(0f, angleY, 0f));
    }
    void Update()
    {
        isAttacking =
            animator.GetCurrentAnimatorStateInfo(0).IsName("Sword_Attack_R");

        if (isGrounded)
        {
            animator.SetTrigger("isLanded");
            Move(dirZ, "isWalkForward", "isWalkBack");
            Sprint();
            Dodge();
        }
        else
        {
            //MoveInAir();
        }

        if (isSwordEquiped && Time.time > lastAttackTime + 5f)
        {
            animator.Play("Holster_Sword");
        }
    }
    private void Move(float dir, string parameterName, string altParameterName)
    {
        if (dir > 0.3f)
        {
            animator.SetBool(parameterName, true);
        }
        else if (dir < -0.3f)
        {
            animator.SetBool(altParameterName, true);
        }
        else
        {
            animator.SetBool(parameterName, false);
            animator.SetBool(altParameterName, false);
        }
    }
    private void Dodge()
    {
        if (joystickLeft.Horizontal < -0.8f)
        {
            animator.Play("Sword_Dodge_Left");
        }
        else if (joystickLeft.Horizontal > 0.8f)
        {
            animator.Play("Sword_Dodge_Right");
        }
    }
    private void Sprint()
    {
        if (joystickLeft.Vertical > 0.9f || joystickLeft.Vertical < -0.9f)
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }
    }
    public void Jump()
    {
        if (isGrounded)
        {
            animator.Play("JumpStart");
            animator.applyRootMotion = false;
            jumpDir = new Vector3(0f, jumpForce, dirZ * jumpForce / 2);
            jumpDir = transform.TransformDirection(jumpDir);
            rb.AddForce(jumpDir, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    public void OnButtonAttackClicked()
    {
        if (isGrounded)
        {
            if (!isSwordEquiped)
            {
                animator.Play("Equip_Sword");
                lastAttackTime = Time.time;
            }
            else
            {
                //Attack();
                lastAttackTime = Time.time;
            }
        }

    }
}
