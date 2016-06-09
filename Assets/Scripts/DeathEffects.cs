using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Text;

public class DeathEffects : MonoBehaviour
{
    public ParticleSystem ExplosionEffect;
    public AudioSource ExplosionAudio;
    private ChickenSpawnerManager m_ChickenSpawnerManager;
    private AudioClip m_Death;

    // Use this for initialization
    void Start () {
        m_ChickenSpawnerManager = GetComponent<ChickenSpawnerManager>();

        //these should be done in a for loop in a vector. 

        GameObject ExplosionEffectGO = (GameObject)Instantiate(ChickenSpawnerManager.Instance.ExplosionEffect, gameObject.transform.position, transform.rotation);
        ExplosionEffectGO.transform.parent = transform;
        ExplosionEffect = ExplosionEffectGO.GetComponent<ParticleSystem>();
        
        GameObject ExplosionAudioGO = (GameObject)Instantiate(ChickenSpawnerManager.Instance.ExplosionAudio, gameObject.transform.position, transform.rotation);
        ExplosionAudioGO.transform.parent = transform;
        ExplosionAudio = ExplosionAudioGO.GetComponent<AudioSource>();
        ExplosionAudio.PlayOneShot(m_Death);

        ExplosionEffect.Play();
        Destroy(this, m_Death.length);

    }

    // Update is called once per frame
    void Update () {
	
	}
}
