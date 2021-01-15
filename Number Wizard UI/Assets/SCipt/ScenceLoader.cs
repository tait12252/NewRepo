using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenceLoader : MonoBehaviour
{
    public void LoadNextScence()
    {

        int currentScenceIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScenceIndex + 1);

    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }
        public void QuitGame()
        {
        Application.Quit();
        
        }
    
    }

