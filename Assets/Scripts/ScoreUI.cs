using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
     TextMeshProUGUI Score;
    
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Score = GetComponent<TextMeshProUGUI>();
        // DisplayScore();

        Score.text = gameManager.GetScore().ToString();
    }
    private void Update()
    {
        
      /*  if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }*/

       // Score.text = gameManager.GetScore().ToString();
    }
    public void DisplayScore()
    {
         //Debug.Log("as:" + gameManager.GetScore().ToString());
         Score.text = gameManager.GetScore().ToString();
    }
}
