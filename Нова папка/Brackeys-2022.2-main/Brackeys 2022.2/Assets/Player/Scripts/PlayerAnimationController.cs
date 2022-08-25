using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator controller;

    [SerializeField] private PlayerMovement pm;

    void Update()
    {
        //Switches between Idle and Walking based on player speed
        if (pm.IsMoving)
            controller.SetFloat("Speed", pm.transform.GetComponent<Rigidbody2D>().velocity.magnitude);
        else
            controller.SetFloat("Speed", 0);

        controller.SetBool("Grounded", pm.isGrounded);

        //Jump animation
        if (Input.GetButtonDown("Jump"))
            controller.SetTrigger("Jump");
    }
}