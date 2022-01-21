using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 12f;
    public float groundDistance = 0.4f;
    public float gravity = -9.81f;

    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;

    private Animator animator;
    private Rigidbody playerRb;

    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimateMovement();
    }

    private void AnimateMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);


        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        if (x > 0.3)
        {
            animator.SetFloat("velocityHorizontal", 0.5f);
        }
        else if (x < -0.3)
        {
            animator.SetFloat("velocityHorizontal", -0.5f);
        }
        else
        {
            animator.SetFloat("velocityHorizontal", 0.0f);
        }

        if (z > 0.3)
        {
            animator.SetFloat("velocityVertical", 0.5f);
        }
        else if (z < -0.3)
        {
            animator.SetFloat("velocityVertical", -0.5f);
        }
        else
        {
            animator.SetFloat("velocityVertical", 0.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("weapon"))
        {
            animator.Play("GetHit");
        }
    }
}
