using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class TTS : MonoBehaviour
{
    public string text;
    public AudioSource audioSource;

    public void ReadText()
    {
        StartCoroutine(LoadAudio());
    }
    IEnumerator LoadAudio()
    {
        string url = "https://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q="+text+"&tl=En-gb";
        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url,AudioType.MPEG);
        yield return www.SendWebRequest();

        if (audioSource != null)
        {
            audioSource.clip = NAudioPlayer.FromMp3Data(www.downloadHandler.data);
            audioSource.Play();
        }
        else
        {
            audioSource = Camera.main.gameObject.GetComponent<AudioSource>();
        }
    }
}
