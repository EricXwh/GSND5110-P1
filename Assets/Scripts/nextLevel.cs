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
    private int isPress = 0;

    private void Update()
    {
        if(isPlayerNearby && Input.GetKeyDown(KeyCode.E)  && isPress < 2)
        {
            if(PlayerMovement.diary && PlayerMovement.medicine){
                LoadNextScene();
            }
            else{
                isPress += 1;
                MyDialogue.SetActive(true);
                dialogueSystem.ClearWords();
                dialogueSystem.StartDialogue();
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interactionText.gameObject.SetActive(true);
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
}
