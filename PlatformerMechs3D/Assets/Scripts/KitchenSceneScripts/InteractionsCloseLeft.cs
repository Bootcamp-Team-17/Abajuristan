using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsCloseLeft : MonoBehaviour
{
    int _counter6 = 0;
    bool _isPlayer = false;
    Animator _anim;
    [SerializeField] GameObject _cam;

    private void Start() {
        _anim = GetComponent<Animator>();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.F) && _counter6 == 0 && _isPlayer)
        {
            _anim.SetBool("isOpen4", true);
            _cam.SetActive(false);
            _counter6++;
        }
        else if(Input.GetKeyDown(KeyCode.F) && _counter6 == 1 && _isPlayer)
        {
            _anim.SetBool("isOpen4", false);
            _cam.SetActive(true);
            _counter6 = 0;   
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
