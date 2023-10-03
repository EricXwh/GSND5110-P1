using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class interact : MonoBehaviour
{
    public PostProcessProfile cam;
    public TMP_Text interactionText;
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
            cam.GetSetting<ColorGrading>().saturation.value = cam.GetSetting<ColorGrading>().saturation.value - 20;
            Debug.Log("kkk");
            interactionText.gameObject.SetActive(false);
            Destroy(gameObject);
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
