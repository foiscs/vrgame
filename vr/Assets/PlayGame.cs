using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGame : MonoBehaviour
{
    AudioSource audioSource;
    private void Start()
    {
        audioSource = Camera.main.gameObject.GetComponent<AudioSource>();
        SetMusic();
    }
    void SetMusic()
    {
        if (GameManager.Instance.musicName != null)
        {
            string name = GameManager.Instance.musicName;
            audioSource.clip = Resources.Load<AudioClip>("Music/" + name + "/" + name);
        }
    }
    public void StartMusic()
    {
        if(!audioSource.isPlaying)
            audioSource.Play();
    }
    public void SetDrum()
    {

    }
}
