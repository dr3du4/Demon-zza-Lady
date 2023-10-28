using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVolume : MonoBehaviour
{
    private float _volume = 1f;
    public List<AudioSource> audioSources = new List<AudioSource>();
    private AudioListener _audio;
    
    private void Awake() {
        _volume = PlayerPrefs.GetFloat("Volume");
        foreach(AudioSource s in audioSources){
            s.volume = _volume;
        }
    }
}
