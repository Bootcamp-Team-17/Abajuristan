using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactionsss : MonoBehaviour
{
    int _counter4 = 0;
    bool _isPlayer = false;
    Animator _anim;
    private void Start() {
        _anim = GetComponent<Animator>();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.F) && _counter4 == 0 && _isPlayer)
        {
            _anim.SetBool("isOpen3", true);
            _counter4++;
        }
        else if(Input.GetKeyDown(KeyCode.F) && _counter4 == 1 && _isPlayer)
        {
            _anim.SetBool("isOpen3", false);
            _counter4 = 0;   
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
