using UnityEngine;
using UnityEngine.Animations;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator controller;

    [SerializeField] private PlayerMovement pm;

    void Update()
    {
        controller.SetFloat("Speed", pm.transform.GetComponent<Rigidbody2D>().velocity.magnitude);
    }
}