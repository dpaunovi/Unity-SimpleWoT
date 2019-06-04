using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class tankScript: MonoBehaviour {

    public AudioClip exploseClip;

    public int healthPoints = 100;

    private AudioSource tankAudioSource;
    private ParticleSystem exploseParticle;
    private canonScript canonScript;
    private Collider [] colliders;

    private float time = 0.0f;
    private bool alive = true;

    void Start()
    {
        tankAudioSource = GetComponent<AudioSource>();
        colliders = GetComponents<Collider>();
        exploseParticle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (healthPoints <= 0)
        {
            explose();
        }
    }

    public bool isAlive() {
        return alive;
    }

    public void takeHit(int damage) {
        if (healthPoints > 0)
        {
            healthPoints -= damage;
        }
    }

    private void explose()
    {
        time += Time.deltaTime;
        if (alive)
        {
            tankAudioSource.clip = exploseClip;
            tankAudioSource.Play();
            exploseParticle.Play();
            alive = false;
            Destroy(canonScript);
            foreach (Collider col in colliders) {
                if (col.tag != "Player") {
                    col.tag = "Untagged";
                }
            }
        }
        else
        {
            if (time >= 3)
            {
                if (tag == "Player")
                {
                    SceneManager.LoadScene("00");
                } else {
                    Debug.Log("destroy");
                    Destroy(gameObject);
                }
            }
        }
    }
}