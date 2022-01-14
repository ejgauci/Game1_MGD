using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
   
    
    public void Play()
    {
        SceneManager.LoadScene("GameLobby");
    }

    public void HTP()
    {
        SceneManager.LoadScene("HowTo");
    }

    public void Back()
    {
        SceneManager.LoadScene("Start");
    }

}
