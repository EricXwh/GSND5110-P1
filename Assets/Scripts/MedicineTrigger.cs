using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MedicineTrigger : MonoBehaviour
{
    public Dialogue1 dialogueSystem;
    public GameObject MyDialogue;
    public GameObject guide;
    public TMP_Text interactionText;
    private bool isPlayerInRange = false;
    private bool isPickup = false;
    private int isPress = 0;

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && isPress < 2)
        {
            isPress += 1;
            MyDialogue.SetActive(true);
            dialogueSystem.ClearWords();
            dialogueSystem.StartDialogue();
            guide.SetActive(false);
            isPickup = true;
            PlayerMovement.medicine = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            isPlayerInRange = true;
            interactionText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactionText.gameObject.SetActive(false);
            if(isPickup){
                gameObject.SetActive(false);
            }
        }
    }
}
