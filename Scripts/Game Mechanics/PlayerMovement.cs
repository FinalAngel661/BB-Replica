using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Rags Jumped");
            jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            Debug.Log("Rags Crouched");
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            Debug.Log("Rags got back up");
            crouch = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        //Debug.Log("Raggy is movin");
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;

	}
}
