using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class canonScript : MonoBehaviour {

    public Transform hitPoint;

    public ParticleSystem rifleParticle;
    public ParticleSystem missileParticle;

    public AudioSource tankAudioSource;

    public AudioClip rifleClip;
    public AudioClip missileClip;

    public float fireRange = 100;
    public int missileAmmo = 10;

    private ParticleSystem hitParticle;
    private RaycastHit rayHit;
    private float time = 0.0f;
    private float randTime = 0.0f;

    void Start() {
        tankAudioSource = GetComponent<AudioSource>();
        hitParticle = hitPoint.GetComponent<ParticleSystem>();
    }

    void Update() {
        if (time > randTime) {
            randTime = Random.Range(0, 3);
            time = 0;
        }
        time += Time.deltaTime;
        aim();
        if (tag == "Bot") {
            if (rayHit.collider)
            {
                if (rayHit.collider.gameObject.tag == "Bot" || rayHit.collider.gameObject.tag == "Player")
                {
                    if (time > randTime) {
                        int rand = Random.Range(1, 5);
                        if (rand > 0 && rand < 3 || missileAmmo <= 0)
                        {
                            rifleShot();
                        } else {
                            missileShot();
                        }
                    }
                }
            }
        }
    }

    public void rifleShot() {
        tankAudioSource.clip = rifleClip;
        rifleParticle.Play();
        shot(10);
    }

    public void missileShot()
    {
        if (missileAmmo > 0)
        {
            tankAudioSource.clip = missileClip;
            missileParticle.Play();
            shot(20);
            missileAmmo -= 1;
        }
    }

    private void aim() {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out rayHit, fireRange)) {
            Debug.DrawLine(transform.position, rayHit.point, Color.red);
        }
    }

    private void shot(int damage) {
        tankAudioSource.Play();
        if (rayHit.collider)
        {
            int rand = Random.Range(1, 4);
            if (rand == 1 || tag != "Bot") {
                hitPoint.position = rayHit.point;
                hitParticle.Play();
                if (rayHit.collider.gameObject.tag == "Bot" || rayHit.collider.gameObject.tag == "Player") {
                    rayHit.collider.gameObject.GetComponent<tankScript>().takeHit(damage);
                }
            }
        }
    }

}
