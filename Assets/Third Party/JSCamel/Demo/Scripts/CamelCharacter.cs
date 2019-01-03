using UnityEngine;
using System.Collections;

public class CamelCharacter : MonoBehaviour {
    Animator camelAnimator;
    public bool jumpStart = false;
    public float groundCheckDistance = 0.6f;
    public float groundCheckOffset = 0.01f;
    public bool isGrounded = true;
    public float jumpSpeed = 1f;
    Rigidbody camelRigid;
    public float forwardSpeed;
    public float turnSpeed;
    public float walkMode = 1f;
    public float jumpStartTime = 0f;
    public float maxWalkSpeed = 1f;

    public bool canJump = true;
    public bool isLived = true;

    void Start()
    {
        camelAnimator = GetComponent<Animator>();
        camelRigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        CheckGroundStatus();
        Move();
        jumpStartTime += Time.deltaTime;
        maxWalkSpeed = Mathf.Lerp(maxWalkSpeed, walkMode, Time.deltaTime);
    }

    public void Attack()
    {
        camelAnimator.SetTrigger("Attack");
    }

    public void Hit()
    {
        camelAnimator.SetTrigger("Hit");
    }

    public void Death()
    {
        camelAnimator.SetBool("IsLived", false);
        isLived = false;
    }

    public void Rebirth()
    {
        camelAnimator.SetBool("IsLived", true);
        isLived = true;
    }

    public void EatStart()
    {
        camelAnimator.SetBool("IsEating", true);
        canJump = false;
    }
    public void EatEnd()
    {
        camelAnimator.SetBool("IsEating", false);
        canJump = true;
    }



    public void Gallop()
    {
        walkMode = 4f;
    }

    public void Canter()
    {
        walkMode = 3f;
    }

    public void Trot()
    {
        walkMode = 2f;
    }

    public void Walk()
    {
        walkMode = 1f;
    }

    public void Jump()
    {
        if (isGrounded && canJump && isLived)
        {
            camelAnimator.SetTrigger("Jump");
            jumpStart = true;
            jumpStartTime = 0f;
            isGrounded = false;
          }
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
        isGrounded = Physics.Raycast(transform.position + (transform.up * groundCheckOffset), Vector3.down, out hitInfo, groundCheckDistance);

        if (jumpStart)
        {
            if (jumpStartTime > .1f)
            {
                jumpStart = false;
                camelRigid.AddForce((transform.up + transform.forward * forwardSpeed * .3f) * jumpSpeed, ForceMode.Impulse);
                camelAnimator.applyRootMotion = false;
                camelAnimator.SetBool("IsGrounded", false);
            }
        }

        if (isGrounded && !jumpStart && jumpStartTime > .5f)
        {
            camelAnimator.applyRootMotion = true;
            camelAnimator.SetBool("IsGrounded", true);
        }
        else
        {
            if (!jumpStart)
            {
                camelAnimator.applyRootMotion = false;
                camelAnimator.SetBool("IsGrounded", false);
            }
        }
    }

    public void Move()
    {
        camelAnimator.SetFloat("Forward", forwardSpeed);
        camelAnimator.SetFloat("Turn", turnSpeed);
    }
}
