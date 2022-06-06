using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFixer : MonoBehaviour
{


    [SerializeField] GameObject _camo;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) 
    {
         
        if(other.CompareTag("Player"))
        {
            
            _camo.SetActive(true);
        }

        
    }
}
