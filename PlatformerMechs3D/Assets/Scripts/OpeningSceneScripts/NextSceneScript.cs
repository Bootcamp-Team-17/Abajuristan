using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextSceneScript : MonoBehaviour
{
    public void NextLevel()
    {
        SceneManager.LoadScene("FirstScene");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
