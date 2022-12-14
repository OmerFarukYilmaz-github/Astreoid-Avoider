using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Button continueButton; 
    [SerializeField] GameObject gameOverUI;
    AstreoidSpawnner astreoidSpawnner;

    public void Start()
    {
        astreoidSpawnner = FindObjectOfType<AstreoidSpawnner>();
    }

    public void EndGame()
    {
        astreoidSpawnner.isGameOver = true;
        gameOverUI.gameObject.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }
    public void ReturnMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Continue()
    {
        player.transform.position = Vector3.zero;
        player.SetActive(true);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        astreoidSpawnner.isGameOver = false;
        gameOverUI.SetActive(false);
        
    }
    public void ContinueButton()
    {
        AdManager.instance.ShowAd(this);

        continueButton.interactable= false;
    }
}
