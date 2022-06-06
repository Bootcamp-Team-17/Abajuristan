using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BardakScript : MonoBehaviour
{
    int counterB = 0;
private void OnTriggerEnter(Collider other) 
{
    if(other.CompareTag("Bardak") && counterB == 0 )
    {
        counterB++;
        FindObjectOfType<ScoreHolder>().EndPuzzle();
        Debug.Log("Score");
    }
}
}
