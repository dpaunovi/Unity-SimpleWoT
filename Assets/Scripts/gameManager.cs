using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour {

    public AudioClip mainClip;
    public AudioClip panicClip;

    private AudioSource mainAudioSource;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
        mainAudioSource = GetComponent<AudioSource>();
        mainAudioSource.clip = mainClip;
        mainAudioSource.loop = true;
        mainAudioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
