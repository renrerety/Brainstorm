using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class GameInit : MonoBehaviour
{
    public static GameInit instance;
    
    private AudioSource music;
    [SerializeField] public AudioClip gameClip;
    [SerializeField] private AudioClip menuClip;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else if (instance == null)
        {
            instance = this;
        }
    }
    public void Init()
    {
        //systems.SetActive(true);
        Debug.Log("Game init");

        music = GameObject.Find("Music").GetComponent<AudioSource>();
        music.clip = gameClip;
        music.Play();
        
        PlayerWeapons.Instance.Init(FireballPool.instance,ThrowingKnifePool.instance,BombPool.instance,LightningPool.instance, LaserPool.instance);
        PlayerSkinLoader.instance.Init();
        EnemySpawner.Instance.Init();

        PlayerWeapons.Instance.AddWeaponToList("Laser Gun");

        EnemySpawner.Instance.factory = EasyEnemyFactory.instance;
        StartCoroutine(EnemySpawner.Instance.SwapFactory());
        EnemySpawner.Instance.StartWaveLoop();

        GameObject ennemies = GameObject.Find("Ennemies");
        EasyEnemyFactory.instance.enemies = ennemies;
        MediumEnemyFactory.instance.enemies = ennemies;
        HardEnemyFactory.instance.enemies = ennemies;
        
        WorldScrolling.instance.player = GameObject.Find("Player").transform;
    }

    public void ResetGame()
    {
        AudioSource music = GameObject.Find("Music").GetComponent<AudioSource>();
        music.clip = menuClip;
        music.Play();
    }
}
