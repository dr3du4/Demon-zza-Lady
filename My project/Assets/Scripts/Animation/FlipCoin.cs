using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCoin : MonoBehaviour
{
    private Transform _coin;
    public AudioClip yourSoundClip; // Assign your sound clip in the Inspector
    private AudioSource audioSource;
    private void Start() {
        _coin = transform.Find("Coin");
        audioSource = GetComponent<AudioSource>();
        //audioSource.PlayOneShot(yourSoundClip, 1.0f);
        Debug.Log(_coin.name);
    }

    public void Throw() {
        StartCoroutine(fly());
    }

    private IEnumerator fly(){
        audioSource.PlayOneShot(yourSoundClip, 1.0f);
        float progress = 0f;
        Vector3 start = _coin.position;
        Vector3 target = start + new Vector3(0,0.3f,0);
        _coin.gameObject.SetActive(true);
        while(progress < 1f){
            progress += Time.deltaTime / 0.2f;
            _coin.position = Vector3.Lerp(start, target, progress);
            _coin.Rotate(90f,0f,0f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        progress = 0f;
        while(progress < 1f){
            progress += Time.deltaTime / 0.2f;
            _coin.position = Vector3.Lerp(target, start, progress);
            _coin.Rotate(90f,0f,0f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        _coin.position = start;
        _coin.gameObject.SetActive(false);
    }
}
