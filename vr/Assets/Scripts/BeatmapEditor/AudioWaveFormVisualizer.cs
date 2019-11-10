using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
//[RequireComponent(typeof(RawImage))]
public class AudioWaveFormVisualizer : MonoBehaviour
{
    public float playSpeed = 1;
    Color[] blank;
    private void Awake()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
        UnityEngine.XR.XRSettings.enabled = false;
        GetComponent<AudioSource>().playOnAwake = false;
    }
    private void Start()
    {
        Texture2D tex = PaintWaveformSpectrum(GetWaveform(GetComponent<AudioSource>().clip, 1500, 1f), 100, Color.green);

        GetComponent<RawImage>().texture = tex;
    }
    public void AudioPlay()
    {
        if (GetComponent<AudioSource>().time > 0)
            GetComponent<AudioSource>().UnPause();
        else
            GetComponent<AudioSource>().Play();
    }
    public void SetAudioSpeed()
    {
        var pitchBendGroup = Resources.Load<UnityEngine.Audio.AudioMixerGroup>("Pitch Bend Mixer");
        GetComponent<AudioSource>().outputAudioMixerGroup = pitchBendGroup;
        GetComponent<AudioSource>().pitch = playSpeed;
        pitchBendGroup.audioMixer.SetFloat("pitchBend", 1f / playSpeed);
    }
    public float[] GetWaveform(AudioClip audio, int size, float sat)
    {
        float[] samples = new float[audio.channels * audio.samples];
        float[] waveform = new float[size];
        audio.GetData(samples, 0);
        int packSize = audio.samples * audio.channels / size;
        float max = 0f;
        int c = 0;
        int s = 0;
        for (int i = 0; i < audio.channels * audio.samples; i++)
        {
            waveform[c] += Mathf.Abs(samples[i]);
            s++;
            if (s > packSize)
            {
                if (max < waveform[c])
                    max = waveform[c];
                c++;
                s = 0;
            }
        }
        for (int i = 0; i < size; i++)
        {
            waveform[i] /= (max * sat);
            if (waveform[i] > 1f)
                waveform[i] = 1f;
        }

        return waveform;
    }

    public Texture2D PaintWaveformSpectrum(float[] waveform, int height, Color c)
    {
        Texture2D tex = new Texture2D(waveform.Length, height, TextureFormat.RGBA32, false);

        blank = new Color[waveform.Length * height];
        for (var i = 0; i < blank.Length; i++)
        {
            blank[i] = Color.black;
        }

        tex.SetPixels(blank, 0);
        for (int x = 0; x < waveform.Length; x++)
        {
            for (int y = 0; y <= waveform[x] * (float)height / 2f; y++)
            {
                tex.SetPixel(x, (height / 2) + y, c);
                tex.SetPixel(x, (height / 2) - y, c);
            }
        }
        tex.Apply();

        return tex;
    }
}
