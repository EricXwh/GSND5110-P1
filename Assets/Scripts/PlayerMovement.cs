using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
 
    public CharacterController controller;
    public Transform cam;
    public float speed = 5f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Animator anim;
    private bool isJumping = false;
    public static int count = 0;

    

    public CinemachineFreeLook thirdPersonCam;
    public CinemachineVirtualCamera firstPersonCam;
    public GameObject characterModel;
    public SkinnedMeshRenderer[] playerSkinnedRenderers;
    public MeshRenderer[] playerRenderers;

    public float jumpHeight = 1.8f; 
    public float gravity = -10f;
    private bool isGrounded; 
    private Vector3 velocity; 
    private bool isFirstPerson = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        SetPlayerVisibility(true);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Count" + count);
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
        if(isFirstPerson){
            float mouseHorizontal = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up * mouseHorizontal);
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
         if (Input.GetKeyDown(KeyCode.V))
        {
            SwitchCameraView();
        }
        

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    
       
    }
    void SwitchCameraView()
    {
        if (!isFirstPerson)
        {
            thirdPersonCam.Priority = 10; 
            firstPersonCam.Priority = 20; 
            Invoke("HidePlayer", 1.5f);
            isFirstPerson = true;
            
        }
        else
        {
            thirdPersonCam.Priority = 20;
            firstPersonCam.Priority = 10;
            SetPlayerVisibility(true);
            isFirstPerson = false;
        }
    }
    private void SetPlayerVisibility(bool isVisible)
    {
        foreach (SkinnedMeshRenderer renderer in playerSkinnedRenderers)
            {
                renderer.enabled = isVisible;
            }
            foreach (MeshRenderer renderer in playerRenderers)
            {
                renderer.enabled = isVisible;
            }
    }      
    private void HidePlayer()
    {
        SetPlayerVisibility(false);
    }
}
