using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AudioUI : MonoBehaviour
{
    public AudioSource AudioWave;
    public GameObject timeText;
    public Slider audioSlider;
    void Start()
    {
        audioSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        setTextProTest();
        setSliderPosition();
    }

    public void setSliderLength()
    {
        audioSlider.minValue = 0;
        audioSlider.maxValue = AudioWave.clip.length;
    }
    void setTextProTest()
    {
        float t = AudioWave.time;

        string min = ((int)t / 60).ToString("00");
        string sec = (t % 60 * 1000).ToString("00 : 000");
        timeText.GetComponent<TextMeshProUGUI>().text = min + " : " + sec;
    }
    void setSliderPosition()
    {
        audioSlider.value = AudioWave.time;
    }

    public void ClickSliderPosition()
    {
        AudioWave.time = audioSlider.value;
    }
}
