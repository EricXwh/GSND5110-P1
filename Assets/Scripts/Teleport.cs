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

    public GameObject quest1;
    public GameObject quest2;

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
            Guide disappearQ1 = quest1.GetComponent<Guide>();
            if (disappearQ1 != null)
            {
                disappearQ1.MakeDisappear();
                Guide ActiveQ2 = quest2.GetComponent<Guide>();
                if (ActiveQ2 != null){
                    ActiveQ2.MakeAppear();
                }
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
