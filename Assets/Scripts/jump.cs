using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class jump : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    public Animator anim;
    public PostProcessProfile cam;
    private bool isJumping = false;
    public GameObject characterModel;
    public TMP_Text interactionText;

    public float jumpHeight = 1.8f;
    public float gravity = -10f;
    private bool isGrounded;
    private Vector3 velocity;
    private bool isFirstPerson = true;
    private bool ifJumped = false;

    void Start()
    {
        cam.GetSetting<ColorGrading>().brightness.overrideState = true;
        cam.GetSetting<ColorGrading>().brightness.value = -100f;
        interactionText.gameObject.SetActive(true);
        anim = GetComponent<Animator>();
        ifJumped = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        float rayLength = 0.15f;
        Vector3 rayStart = transform.position + Vector3.up * 0.1f;
        isGrounded = Physics.Raycast(rayStart, Vector3.down, rayLength);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (Input.GetKey(KeyCode.Space))
        {
            ifJumped = true;
            interactionText.gameObject.SetActive(false);
        }
        if (ifJumped)
        {
            if (cam.GetSetting<ColorGrading>().brightness.value > -100)
            {
                cam.GetSetting<ColorGrading>().brightness.value = cam.GetSetting<ColorGrading>().brightness.value - 0.1f;
            }
            speed = 3f;
            anim.SetBool("isWalking", false);
            Vector3 move = new Vector3(-0.5f, 0, 0.5f);
            controller.Move(move * speed * Time.deltaTime);
            if (!isJumping)
            {
                anim.SetBool("isMoving", true);
            }
        }
        else
        {
            if (cam.GetSetting<ColorGrading>().brightness.value < 0)
            {
                cam.GetSetting<ColorGrading>().brightness.value = cam.GetSetting<ColorGrading>().brightness.value + 0.1f;
            }
            anim.SetBool("isMoving", false);
        }

        if (ifJumped && isGrounded && !anim.GetCurrentAnimatorStateInfo(0).IsName("JUMP00B"))
        {
            Debug.Log("jump");
            isJumping = true;
            anim.SetBool("isJump", true);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else if (isGrounded)
        {
            isJumping = false;
            anim.SetBool("isJump", false);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
