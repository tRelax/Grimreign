using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicControl : MonoBehaviour
{
    public AudioSource audioSource;
    public static bool playMusic = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                playMusic = false;
            }
            else
            {
                audioSource.Play();
                playMusic = true;
            }
        }
    }

}
