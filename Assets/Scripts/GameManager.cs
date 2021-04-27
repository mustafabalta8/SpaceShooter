using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int Score;
   // [SerializeField] int ScoreValue;
    ScoreUI scoreUI;
    // Start is called before the first frame update
    void Awake()
    {
        SetUpSingelton();

        scoreUI = FindObjectOfType<ScoreUI>();
    }

    private void SetUpSingelton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            //Destroy(gameObject);
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return Score;
    }

    public void AddToScore(int scoreValue)
    {
        Score += scoreValue;
        scoreUI.DisplayScore();

    }
    public void Reset()
    {
        Destroy(gameObject);
    }
}
