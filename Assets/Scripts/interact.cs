using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class interact : MonoBehaviour
{
    public GameObject player;
    public PostProcessProfile cam;
    public TMP_Text interactionText;
    public float total_num;
    public float pitch_adj;
    private bool isInRange = false; 
    private PostProcessVolume vol;

    void Start()
    {
        cam.GetSetting<ColorGrading>().saturation.value = 0;
        interactionText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            player.GetComponent<AudioSource>().pitch = player.GetComponent<AudioSource>().pitch - (pitch_adj / total_num); ;
            cam.GetSetting<ColorGrading>().saturation.value = cam.GetSetting<ColorGrading>().saturation.value - (100/total_num);
            Debug.Log("kkk");
            interactionText.gameObject.SetActive(false);
            Destroy(gameObject);
            PlayerMovement.count += 1;
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  
        {
            Debug.Log("yyy");
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
