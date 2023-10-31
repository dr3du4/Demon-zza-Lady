using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolumeSlider : MonoBehaviour
{
    private float _volume = 1f;
    public List<AudioSource> audioSources = new List<AudioSource>();
    private AudioListener _audio;
    
    private void Awake() {
        _volume = PlayerPrefs.GetFloat("Volume", 0.5f);
        Slider slide = GetComponent<Slider>();
        slide.value = _volume;
        foreach(AudioSource s in audioSources){
            s.volume = _volume;
        }
    }

    public void SetVolume(float v) {
        PlayerPrefs.SetFloat("Volume", v);
        _volume = v;
        foreach(AudioSource s in audioSources){
            s.volume = _volume;
        }
    }
}
