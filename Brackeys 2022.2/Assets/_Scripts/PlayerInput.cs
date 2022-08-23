using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    bool doubleJump = false;

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            BroadcastMessage("Jump");

        //double jump

        if (Input.GetButtonDown("Fire1"))
            BroadcastMessage("Hit");
    }
}