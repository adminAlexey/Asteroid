using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int save, saveCount;
    public AudioSource explosionSound;

    public Color color;
    public GameObject halfPlayer, halfPlayer2, player;

    public Controller controller;

    public GameObject panelPause;

    public Text scoreText;
    public Text livesText;
    public Text controlText;

    public bool ctrl = true;
    public float respawnTime = 3f;
    public float lives = 3f;
    public static int score = 0;

    private bool paused = true;

    public void Start()
    {
        saveCount = PlayerPrefs.GetInt("save");
        if (saveCount == 0)
        {
            controlText.text = "Control: m+key";
            ctrl = false;
        }
        else
        {
            controlText.text = "Control: key";
            ctrl = true;
        }


        Time.timeScale = 0f;
        panelPause.SetActive(true);
        player.GetComponent<Controller>().enabled = false;
        player.GetComponent<Controller2>().enabled = false;   
    }

    public void Update()
    {
        livesText.text = lives.ToString();
        scoreText.text = score.ToString();

        if (Input.GetKeyUp("escape") && !paused)
        {
            player.GetComponent<Controller>().enabled = false;
            player.GetComponent<Controller2>().enabled = false;
            
            Time.timeScale = 0f;

            paused = true;

            panelPause.SetActive(true);
        }
        else if (Input.GetKeyUp("escape") && paused)
        {
            if (ctrl) player.GetComponent<Controller>().enabled = true;
            else player.GetComponent<Controller2>().enabled = true;

            Time.timeScale = 1f;

            paused = false;

            panelPause.SetActive(false);
        }
    }

    public void ChangeControl()
    {
        if (!ctrl)
        {
            PlayerPrefs.SetInt("save", 1);
            controlText.text = "Control: key";
            ctrl = true;
        }
        else
        {
            PlayerPrefs.SetInt("save", 0);
            controlText.text = "Control: m+key";
            ctrl = false;
        }
    }

    public void PlayerDied()
    {
        explosionSound.Play();
        lives-=1f;

        if (lives <= 0)
        {
            GameOver();
        }else Invoke(nameof(Respawn),respawnTime);
    }

    public void Resume()
    {
        if (ctrl) player.GetComponent<Controller>().enabled = true;
        else player.GetComponent<Controller2>().enabled = true;

        Time.timeScale = 1f;

        paused = false;

        panelPause.SetActive(false);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Game");
        Resume();
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
        Debug.Log("выход из игры"); 
    }

    public void Respawn()
    {
        controller.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        controller.transform.position = Vector3.zero;
        controller.gameObject.SetActive(true);
        StartCoroutine(Flicker());
        Invoke(nameof(TurnOnCollision), 3f);
    }

    private void TurnOnCollision()
    {
        controller.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void GameOver()
    {
        //TODO
    }

    IEnumerator Flicker()
    {
        for (int i = 0; i < 6; i++) 
        { 
            halfPlayer2.gameObject.GetComponentInChildren<SpriteRenderer>().color = color;
            halfPlayer.gameObject.GetComponentInChildren<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.25f);
            halfPlayer2.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
            halfPlayer.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.25f); 
        }
    }
}
