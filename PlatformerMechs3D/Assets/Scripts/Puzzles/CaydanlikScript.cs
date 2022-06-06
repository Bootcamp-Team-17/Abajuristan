using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaydanlikScript : MonoBehaviour
{
    int counterC = 0;
private void OnTriggerEnter(Collider other) 
{
    if(other.CompareTag("Caydanlik") && counterC == 0)
    {
        counterC++;
        FindObjectOfType<ScoreHolder>().EndPuzzle();
        Debug.Log("Score");
    }
}
}
