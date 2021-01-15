using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Exit : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 1f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(LoadNextLevel());
    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
