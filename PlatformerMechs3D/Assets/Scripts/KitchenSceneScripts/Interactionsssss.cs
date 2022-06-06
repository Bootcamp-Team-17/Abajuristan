using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactionsssss : MonoBehaviour
{
    int _counter5 = 0;
    bool _isPlayer = false;
    Animator _anim;
    private void Start() {
        _anim = GetComponent<Animator>();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.F) && _counter5 == 0 && _isPlayer)
        {
            _anim.SetBool("isOpen6", true);
            _counter5++;
        }
        else if(Input.GetKeyDown(KeyCode.F) && _counter5 == 1 && _isPlayer)
        {
            _anim.SetBool("isOpen6", false);
            _counter5 = 0;   
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
