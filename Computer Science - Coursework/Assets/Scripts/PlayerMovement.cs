using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    //references character controller
    public CharacterController2D controller;
    float horizontalMove = 0f;
    public float NormalSpeed = 20f;
    public float SprintSpeed = 40f;
    public float Speed = 20f;
    float Stamina = 100f;
    float MaxStamina = 100f;
    bool running = false;
    bool jump = false;
    bool crouch = false;
    bool Air = false;

//animations
    public Animator Anim; //references the animator component

    //updates every frame
    void Update(){
        
        if(Input.GetButtonDown("Jump")){
            jump = true;
        }
        if(Input.GetButtonDown("Crouch"))
        {
            Anim.SetBool("crouching",true);
            crouch = true;
            Speed = NormalSpeed;
            Anim.SetBool("running",false);//making sure that the speed is normal speed when crouching
        }
        else if(Input.GetButtonUp("Crouch")&& controller.m_wasCrouching)
        {
            //Anim.SetBool("crouching",false);
            crouch = false;
        }
        if(Input.GetButtonDown("Sprint") && !controller.m_wasCrouching)
        {
            Speed = SprintSpeed;
            running = true;
            Anim.SetBool("running",true);
        }
        else if(Input.GetButtonUp("Sprint")){
            Speed = NormalSpeed;
            running = false;
            Anim.SetBool("running",false);
        }
        if(!controller.m_Grounded && !Air){ //testing if it was in the air before before setting the air trigger
            Air = true;
            Anim.SetTrigger("air");
        }else if(controller.m_Grounded && Air){ //testing if it was in the air before before setting the onGround trigger
            Air = false;
            Anim.SetTrigger("onGround");
        }
    }
    void FixedUpdate(){
        horizontalMove = Input.GetAxisRaw("Horizontal") * Speed;//sets the amount the player should move 
        if (horizontalMove != 0){
            Anim.SetBool("walking", true);
        }else{
            Anim.SetBool("walking",false);
        }
        //by multiplying the horizontal input by the movement speed
        //movement is used in fixed update
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);//time function is used to make sure the 
        //movement is consistant no matter how fast the cpu runs
        
        jump = false; //resets the jump bool
        
        
    }
}