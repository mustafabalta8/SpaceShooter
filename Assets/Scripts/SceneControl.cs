using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
   public void LoadGame()
    {
        SceneManager.LoadScene(1);
        FindObjectOfType<GameManager>().Reset();
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadGameOver()
    {
        StartCoroutine(GameOverDelay());
        
    }
    IEnumerator GameOverDelay()
    {
        
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
    }
}
