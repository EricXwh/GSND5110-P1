using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class books : MonoBehaviour
{
    public Dialogue1 dialogueSystem;
    public GameObject MyDialogue;
    public TMP_Text interactionText;
    private bool isPlayerInRange = false;

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            MyDialogue.SetActive(true);
            dialogueSystem.ClearWords();
            dialogueSystem.StartDialogue();
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
        }
    }
}
