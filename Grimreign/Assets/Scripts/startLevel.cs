using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startLevel : MonoBehaviour
{
    public static bool playMusic = true;

    private void Start()
    {
        playMusic = musicControl.playMusic;
    }
    private void Update()
    {
        playMusic = musicControl.playMusic;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playMusic = musicControl.playMusic;
            SceneManager.LoadScene("levelScene");
        }
    }
}