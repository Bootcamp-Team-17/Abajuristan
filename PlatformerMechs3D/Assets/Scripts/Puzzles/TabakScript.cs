using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabakScript : MonoBehaviour
{
    int tabakC = 0;
private void OnTriggerEnter(Collider other) 
{
    if(other.CompareTag("Tabak") && tabakC == 0)
    {
        tabakC++;
        FindObjectOfType<ScoreHolder>().EndPuzzle();
        Debug.Log("Score");
    }
}
}
