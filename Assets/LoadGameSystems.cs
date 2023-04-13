using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Zenject;

public class LoadGameSystems : MonoBehaviour
{
    [Inject] private EasyEnemyFactory _easyEnemyFactory;
    [Inject] private EnemySpawner _enemySpawner;
    [Inject] private PlayerWeapons _playerWeapons;
    [Inject] private PlayerHealth _playerHealth;
    
    [SerializeField] private Installer installer;
    [SerializeField] private LevelData scene;

    [SerializeField] private WorldScrolling _worldScrolling;
    // Start is called before the first frame update
    void Start()
    {
        Addressables
                    .LoadAssetsAsync<UnityEngine.Object>(new List<string>() { "Systems" }, x => { }, Addressables.MergeMode.Union)
                    .Completed += LoadGameSystemsScene;
    }

    public void LoadGameSystemsScene(AsyncOperationHandle<IList<UnityEngine.Object> > op)
    {
        //Addressables.LoadSceneAsync(scene.name, LoadSceneMode.Additive);
    }

    private void SetupPlayerHealth()
    {
        _playerHealth._spriteRenderer = _playerHealth.GetComponent<SpriteRenderer>();
        _playerHealth._audioSource = _playerHealth.GetComponent<AudioSource>();
        _playerHealth.maxHp += (PlayerData.instance.persistentData.upgrades.hpUp * 5);
        _playerHealth.hp = _playerHealth.maxHp;
        _playerHealth.UpdateHpBar();
    }
}
