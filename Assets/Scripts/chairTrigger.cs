using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class chairTrigger : MonoBehaviour
{
    public Dialogue1 dialogueSystem;
    public GameObject MyDialogue;
    public GameObject guide;
    public TMP_Text interactionText;
    private bool isPlayerInRange = false;
    private bool isPress = false;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = MyDialogue.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component not found on MyDialogue!");
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !isPress)
        {
            isPress = true;
            SetCanvasOpacity(1.0f);
            dialogueSystem.ClearWords();
            dialogueSystem.StartDialogue();
            guide.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            isPlayerInRange = true;
            interactionText.gameObject.SetActive(true);
            MyDialogue.SetActive(true);
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

    void SetCanvasOpacity(float opacity)
    {
        if (canvasGroup)
        {
            canvasGroup.alpha = opacity;
        }
    }
}
