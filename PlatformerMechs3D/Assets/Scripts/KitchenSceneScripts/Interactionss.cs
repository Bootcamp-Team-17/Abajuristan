using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactionss : MonoBehaviour
{
    int _counter3 = 0;
    bool _isPlayer = false;
    Animator _anim;
    private void Start() {
        _anim = GetComponent<Animator>();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.F) && _counter3 == 0 && _isPlayer)
        {
            _anim.SetBool("isOpen2", true);
            _counter3++;
        }
        else if(Input.GetKeyDown(KeyCode.F) && _counter3 == 1 && _isPlayer)
        {
            _anim.SetBool("isOpen2", false);
            _counter3 = 0;   
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
         
        if(other.CompareTag("Player"))
        {
            
            _isPlayer = true;
        }

        
    }
    private void OnTriggerExit(Collider other) {
        _isPlayer = false;
    }
}
