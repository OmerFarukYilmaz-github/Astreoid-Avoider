using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealtManager : MonoBehaviour
{
    public static ShipHealtManager instance;
    GameOver gameOver;
    public void Awake()
    {
        gameOver = FindObjectOfType<GameOver>();
        instance = this;
    }
    public void Crash()
    {
        Debug.Log("Crash!!");
        gameObject.SetActive(false);
        gameOver.EndGame();
    }

}
