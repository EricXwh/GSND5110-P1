using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class nextLevel : MonoBehaviour
{
    public TMP_Text interactionText;
    private bool isPlayerNearby = false;
    public Dialogue1 dialogueSystem;
    public GameObject MyDialogue;
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
        if(isPlayerNearby && Input.GetKeyDown(KeyCode.E) )
        {
            if(PlayerMovement.count == 5 || PlayerMovement.count == 11){
                LoadNextScene();
            }
            else{
                if(MyDialogue.activeSelf){
                    SetCanvasOpacity(1.0f);
                    dialogueSystem.ClearWords();
                    dialogueSystem.StartDialogue();
                }
                
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interactionText.gameObject.SetActive(true);
            MyDialogue.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            interactionText.gameObject.SetActive(false);
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void SetCanvasOpacity(float opacity)
    {
        if (canvasGroup)
        {
            canvasGroup.alpha = opacity;
        }
    }
}
