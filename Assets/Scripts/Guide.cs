using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{

    public void MakeAppear()
    {
        gameObject.SetActive(true);  
    }

    public void MakeDisappear()
    {
        gameObject.SetActive(false);  
    }
}
