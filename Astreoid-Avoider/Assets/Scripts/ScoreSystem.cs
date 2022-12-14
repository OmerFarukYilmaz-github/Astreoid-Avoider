using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem instance;
    [SerializeField] TMP_Text scoreText;
    int score=0;


    public void Start()
    {
        instance = this;
    }
    public void AddScore()
    {
        score++;
    }
    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }
}
