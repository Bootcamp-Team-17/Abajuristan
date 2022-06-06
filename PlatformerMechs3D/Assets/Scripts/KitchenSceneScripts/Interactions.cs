using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    int _counter = 0;
    bool _isPlayer = false;
    Animator _anim;
    private void Start() {
        _anim = GetComponent<Animator>();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.F) && _counter == 0 && _isPlayer)
        {
            _anim.SetBool("isOpen1", true);
            _counter++;
        }
        else if(Input.GetKeyDown(KeyCode.F) && _counter == 1 && _isPlayer)
        {
            _anim.SetBool("isOpen1", false);
            _counter = 0;   
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
