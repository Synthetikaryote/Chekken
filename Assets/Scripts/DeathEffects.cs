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
    public AudioClip m_Death;

    // Use this for initialization
    void Start () {
        m_ChickenSpawnerManager = GetComponent<ChickenSpawnerManager>();

        //m_Death = GetComponent<AudioClip>();
        Instantiate(m_Death, this.transform.position, this.transform.rotation);

        GameObject ExplosionEffectGO = (GameObject)Instantiate(ChickenSpawnerManager.Instance.ExplosionEffect, gameObject.transform.position, transform.rotation);
        ExplosionEffectGO.transform.parent = transform;
        ExplosionEffect = ExplosionEffectGO.GetComponent<ParticleSystem>();
        
        GameObject ExplosionAudioGO = (GameObject)Instantiate(ChickenSpawnerManager.Instance.ExplosionAudio, gameObject.transform.position, transform.rotation);
        ExplosionAudioGO.transform.parent = transform;
        ExplosionAudio = ExplosionAudioGO.GetComponent<AudioSource>();
        ExplosionAudio.PlayOneShot(m_Death);

        
        ExplosionEffect.Play();
        ExplosionAudio.Play();
        

    }

    // Update is called once per frame
    void Update () {
        Destroy(this.gameObject, 30.0f);
    }
}
