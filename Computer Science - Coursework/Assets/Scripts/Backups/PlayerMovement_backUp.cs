using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement_backUp : MonoBehaviour
{
    //references character controller
    public CharacterController2D controller;
    float horizontalMove = 0f;
    public float NormalSpeed = 20f;
    public float SprintSpeed = 40f;
    public float Speed = 20f;
    bool jump = false; //creates a boolean to know wether the player should jump
    bool crouch = false;
    float Stamina = 100f;
    float MaxStamina = 100f;
    bool running = false;
//animations
    public Animator Anim; //references the animator component
    // public RuntimeAnimatorController Idle;//references the idle animation
    // public RuntimeAnimatorController Walk;//references the walk animation
    // public RuntimeAnimatorController Run;//references the run animation
    // public RuntimeAnimatorController Jump;//references the jump animation
    // public RuntimeAnimatorController Crouch;//references crouch animation
    // //updates every frame
    void Update(){
        
        if(Input.GetButtonDown("Jump")){
            jump = true;
        }
        if(Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if(Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
        if(Input.GetButtonDown("Sprint") && !controller.m_wasCrouching)
        {
            Speed = SprintSpeed;
            running = true;
        }
        else if(Input.GetButtonUp("Sprint")){
            Speed = NormalSpeed;
            running = false;
        }
    }
    void FixedUpdate(){
        horizontalMove = Input.GetAxisRaw("Horizontal") * Speed;//sets the amount the player should move 
        //by multiplying the horizontal input by the movement speed
        //movement is used in fixed update
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);//time function is used to make sure the 
        //movement is consistant no matter how fast the cpu runs
        
        jump = false; //resets the jump bool
        
        //animation if statments controller
        // if(!controller.m_Grounded)
        // {//checks if the player is in the sky
        //     Anim.runtimeAnimatorController = Jump;//activates jump animation
        // }        
        // else if(crouch || controller.m_wasCrouching)
        // {
        //     Anim.runtimeAnimatorController = Crouch;//activaates when player is crouched
        // }
        // else if(horizontalMove == 0)//check if the player is staying still
        // {
        //     Anim.runtimeAnimatorController = Idle;//activates idle animation
        // }
        // else if(running)//checks if player is running
        // {
        //     Anim.runtimeAnimatorController = Run;//activates running animation
        // }
        // else{
        //     Anim.runtimeAnimatorController = Walk;//if no other condition is met 
        //     //then the player mus be walking and therefore the animation is activated
        // }
    }
}
