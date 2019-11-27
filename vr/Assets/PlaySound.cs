using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlaySound : MonoBehaviour 
{
    private int num;
    private AudioSource source;
	void Start () {
        num = transform.parent.GetComponent<DrumInfo>().num;
        source = GetComponent<AudioSource>();
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DrumStickHead")
        {
            source.volume = other.gameObject.GetComponent<TrackSpeed>().speed;
            ActivateSound();
            var temp = GameManager.Instance.nodeObjList.FindAll(x => (x.GetComponent<NodeAnimation>().num == num) && x.activeSelf);
            var node=temp.First(m => m.transform.localScale.x == (temp.Min(x => x.transform.localScale.x)));
            node.SetActive(false);
            if (!GameObject.Find("AudioPeer").GetComponent<AudioSource>().isPlaying)
            {
                GameManager.Instance.LoadMusicInPlayScene();
                GameObject.Find("AudioPeer").GetComponent<AudioPeer>()._audioSource.Play();
            }
        }
        
    }

    private void ActivateSound()
    {
        source.Play();
    }
}
