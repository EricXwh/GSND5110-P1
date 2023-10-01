using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Teleport : MonoBehaviour
{
    public GameObject player;
    public Transform playerTransform;  
    public TMP_Text interactionText;
    private Vector3 teleportLocation = new Vector3(28.121f, 0f, 58.96012f);  
    private Vector3 teleportBack = new Vector3(29.858f, 0f, 58.96012f);
    private bool isInRange = false; 
    private bool isBack = false;

    void Start()
    {
        interactionText.gameObject.SetActive(false);
    }

    void Update()
    {
       
        Debug.Log(isInRange);
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(playerTransform.position);
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
        if (other.CompareTag("Player"))  
        {
            isInRange = true;
            interactionText.gameObject.SetActive(true);
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            interactionText.gameObject.SetActive(false); 
        }
    }
}
