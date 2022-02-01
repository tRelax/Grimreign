using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class cameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    private AudioSource audioSource;
    bool isPaused = false;
    public bool playMusic = true;
    public Button resumeButton, mainMenuButton;

    private void Start()
    {
        playMusic = startLevel.playMusic;
        audioSource = GetComponent<AudioSource>();
        if (!playMusic)
        {
            audioSource.Stop();
        }
        resumeButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        resumeButton.onClick.AddListener(resume);
        mainMenuButton.onClick.AddListener(mainMenu);
    }
    void Update()
    {
        transform.position = new Vector3(player.position.x + offset.x,
        player.position.y + offset.y, offset.z);

        if ((Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.Escape)) && !isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            resumeButton.gameObject.SetActive(true);
            mainMenuButton.gameObject.SetActive(true);
            Screen.brightness = 0.5f;
        }

        else if ((Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.Escape)) && isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            resumeButton.gameObject.SetActive(false);
            mainMenuButton.gameObject.SetActive(false);
            Screen.brightness = 1f;
        }
    }
    private void resume()
    {
        Time.timeScale = 1;
        isPaused = false;
        resumeButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
    }

    private void mainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("mainMenuScene");
    }
}
