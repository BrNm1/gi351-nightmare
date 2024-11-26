using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScence : MonoBehaviour
{
    public void Menu() 
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void Play() 
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void Dead()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void Exit() 
    {
        Application.Quit();
    }
}
