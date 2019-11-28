using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlaySound : MonoBehaviour 
{
    private int num;
    private AudioSource source;
	void Start () 
    {
        if(transform.parent.GetComponent<DrumInfo>())
            num = transform.parent.GetComponent<DrumInfo>().num;
        source = GetComponent<AudioSource>();
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DrumStickHead")
        {
            source.volume = other.gameObject.GetComponent<TrackSpeed>().speed;
            ActivateSound();
            GameManager.Instance.HitDrum(num);
            if (!GameObject.Find("AudioPeer").GetComponent<AudioSource>().isPlaying)
            {
                GameObject.Find("AudioPeer").GetComponent<AudioPeer>()._audioSource.Play();
            }
        }
        
    }

    private void ActivateSound()
    {
        source.Play();
    }
}
