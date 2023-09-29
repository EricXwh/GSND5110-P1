using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
 
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Animator anim;
    private bool isJumping = false;

    public float jumpHeight = 1.8f; 
    public float gravity = -10f;
    private bool isGrounded; 
    private Vector3 velocity; 

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        float rayLength = 0.15f; 
        Vector3 rayStart = transform.position + Vector3.up * 0.1f; 
        isGrounded = Physics.Raycast(rayStart, Vector3.down, rayLength);
        Debug.DrawRay(rayStart, Vector3.down * rayLength, Color.red); 
        

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }
          
        
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(Input.GetKey(KeyCode.LeftShift)){
            speed = 2f;
            anim.SetBool("isWalking",true);
        }
        else{
            speed = 6f;
            anim.SetBool("isWalking",false);
        }

        if(direction.magnitude >= 0.1f){
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle,0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle,0f)* Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            if(!isJumping){
               anim.SetBool("isMoving", true); 
            }
            
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded && !anim.GetCurrentAnimatorStateInfo(0).IsName("JUMP00B"))
        {
            Debug.Log("jump");
            isJumping = true;
            anim.SetBool("isJump",true);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); 
        }
        else if (isGrounded){
            isJumping = false;
            anim.SetBool("isJump",false);
        }
        

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    
       
    }      
}
