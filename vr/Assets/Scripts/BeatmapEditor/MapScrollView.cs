using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapScrollView : MonoBehaviour
{
    public Scrollbar scrollbar;
    public AudioSource audioSource;
    public Slider slider;
    public float BPM = 140f;
    void Start()
    {
        BPM = UniBpmAnalyzer.AnalyzeBpm(audioSource.clip);
        var field = transform.Find("Viewport").Find("Content").GetComponent<RectTransform>();
        field.sizeDelta = new Vector2(800, audioSource.clip.length * 110000 / BPM);
        Debug.Log(field.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        scrollbar.value = (slider.value / slider.maxValue);
    }
}
