using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class Enter : MonoBehaviour
{
public GameObject player;
    public Transform playerTransform;  
    public TMP_Text interactionText;
    [SerializeField]
    private Vector3 teleportLocation = new Vector3(35.8f, 0f, 58.96012f);  
    [SerializeField]
    private Vector3 teleportBack = new Vector3(33.34f, 0f, 58.96012f);
    private bool isInRange = false; 
    private bool isBack = false;


    void Start()
    {
        interactionText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            player.SetActive(false);
            if(isBack == false){
                playerTransform.position = teleportLocation;
                isBack = true;
            }
            else{
                playerTransform.position = teleportBack;
                isBack = false;
            }
            
            player.SetActive(true);
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I Entered: " + other.gameObject.name);
        if (other.CompareTag("Player"))  
        {
            isInRange = true;
            interactionText.gameObject.SetActive(true);
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("I Exited: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            interactionText.gameObject.SetActive(false); 
        }
    }
}
