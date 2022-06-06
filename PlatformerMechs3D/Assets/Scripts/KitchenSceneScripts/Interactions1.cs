using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions1 : MonoBehaviour
{
    int _counter2 = 0;
    bool _isPlayer = false;
    Animator _anim;
    private void Start() {
        _anim = GetComponent<Animator>();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.F) && _counter2 == 0 && _isPlayer)
        {
            _anim.SetBool("isOpen5", true);
            _counter2++;
        }
        else if(Input.GetKeyDown(KeyCode.F) && _counter2 == 1 && _isPlayer)
        {
            _anim.SetBool("isOpen5", false);
            _counter2 = 0;   
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
