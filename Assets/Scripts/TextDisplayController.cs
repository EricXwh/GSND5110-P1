using UnityEngine;
using TMPro;
using System.Collections;

public class TextDisplayController : MonoBehaviour
{
    public GameObject blackScreenPanel; 
    public float displayDuration = 3.0f;  

    private void Start()
    {
        StartCoroutine(DisplayTomorrowText());
    }

    private IEnumerator DisplayTomorrowText()
    {
        blackScreenPanel.SetActive(true);
        yield return new WaitForSeconds(displayDuration);
        blackScreenPanel.SetActive(false);
    }
}
