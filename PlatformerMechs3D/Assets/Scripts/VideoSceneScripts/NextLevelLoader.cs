using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelLoader : MonoBehaviour
{
    public void OnSkip()
    {
        SceneManager.LoadScene("TeachersRoom");
    }

}
