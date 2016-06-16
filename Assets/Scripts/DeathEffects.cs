using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Text;

public class DeathEffects : MonoBehaviour
{
    public ParticleSystem ExplosionEffect;
    public AudioSource DeathAudio;
    private ChickenSpawnerManager m_ChickenSpawnerManager;

    // Use this for initialization
    void Start () {
        m_ChickenSpawnerManager = GetComponent<ChickenSpawnerManager>();
        DeathAudio = this.GetComponent<AudioSource>();
        GameObject ExplosionEffectGO = (GameObject)Instantiate(ChickenSpawnerManager.Instance.ExplosionEffect, gameObject.transform.position, transform.rotation);
        ExplosionEffectGO.transform.parent = transform;
        ExplosionEffect = ExplosionEffectGO.GetComponent<ParticleSystem>();

        
        ExplosionEffect.Play();
        DeathAudio.Play();

    }

    // Update is called once per frame
    void Update () {
        Destroy(this.gameObject, 10.0f);
    }
}
