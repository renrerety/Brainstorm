using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Zenject;

public class GameInit : MonoBehaviour
{
    public static GameInit instance;
    
    [Inject] private PlayerWeapons _playerWeapons;

    [Inject] private EnemySpawner _enemySpawner;

    [Inject] private EasyEnemyFactory _easyEnemyFactory;
    [Inject] private PlayerMovement _playerMovement;
    [Inject] private PlayerHealth _playerHealth;
    [Inject] private PlayerLevel _playerLevel;

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
        Init();
    }
    public void Init()
    {
        //systems.SetActive(true);
        Debug.Log("Game init");

        _playerLevel.Init();
        _playerMovement.Init();
        _playerHealth.Init();
        _playerWeapons.Init();
        PlayerSkinLoader.instance.Init();
        _enemySpawner.Init();
        _playerWeapons.AddWeaponToList("Laser Gun");

        _enemySpawner.factory = _easyEnemyFactory;
        StartCoroutine(_enemySpawner.SwapFactory());
        _enemySpawner.StartWaveLoop();
    }
}
