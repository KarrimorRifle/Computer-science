using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    //references character controller
    public CharacterController2D controller;
    public GunController gun;
    float horizontalMove = 0f;
    public float NormalSpeed = 20f;
    public float SprintSpeed = 40f;
    public float Speed = 20f;
    public float Stamina = 100f;
    public float MaxStamina = 100f;
    bool running = false;
    bool jump = false;
    bool crouch = false;
    bool Air = false;

//animations
    public Animator Anim; //references the animator component
    bool speedreset;
    //updates every frame
    void Update(){
        
        if(!gun.turret)
        {
            if(!speedreset)
                Speed = NormalSpeed;
                speedreset = true;
            Anim.SetBool("turret",false);
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
            if(Input.GetButtonDown("Sprint") && !controller.m_wasCrouching && Stamina >= 10)
            {
                Debug.Log("movement: test");
                Speed = SprintSpeed;
                running = true;
                Anim.SetBool("running",true);
            }
            else if(Input.GetButtonUp("Sprint") || Stamina <= 0){
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
        }else
        {
            Speed = 0;
            Anim.SetBool("turret",true);
            crouch = false;
            speedreset = false;
        }
    }
    public float staminaConsumption = 10;
    public float staminaRecovery = 15f;
    void FixedUpdate(){
        if(running)
            Stamina -= staminaConsumption *Time.deltaTime;
        else if(!running && Stamina <= MaxStamina)
            Stamina += staminaRecovery * Time.deltaTime;
        if(!gun.turret){
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
}
