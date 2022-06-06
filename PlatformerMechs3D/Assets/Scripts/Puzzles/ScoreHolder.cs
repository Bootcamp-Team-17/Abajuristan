using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHolder : MonoBehaviour
{
    int score = 0;
    [SerializeField] Animator _anim;
    [SerializeField] GameObject _camo;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(score == 3)
        {
            _anim.SetBool("isOpenD", true);
            _camo.SetActive(false);
            Debug.Log("KAPAT");
        }
    }

    public void EndPuzzle()
    {
        score++;
    }
}
