using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class tryAgainScript : MonoBehaviour
{
    AudioSource audioSource;
    public Button yesButton, noButton;
    public bool playMusic = true;

    private void Start()
    {
        playMusic = startLevel.playMusic;
        yesButton.onClick.AddListener(buttonYes);
        noButton.onClick.AddListener(buttonNo);

        if (!playMusic)
        {
            audioSource.Stop();
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Y))
        {
            SceneManager.LoadScene("levelScene");
        }
        else if (Input.GetKey(KeyCode.N)){
            SceneManager.LoadScene("mainMenuScene");
        }
    }

    private void buttonYes()
    {
        SceneManager.LoadScene("levelScene");
    }

    private void buttonNo()
    {
        SceneManager.LoadScene("mainMenuScene");
    }
}
