using UnityEngine;
using Zenject;

public class Installer : MonoInstaller
{ 
    [SerializeField] private GameObject easyFactory;
    [SerializeField] private GameObject mediumFactory;
    [SerializeField] private GameObject hardFactory;
    [SerializeField] private GameObject enemySpawner;

    [SerializeField] private GameObject player;

    [SerializeField] private GameObject throwingKnifePool;
    [SerializeField] private GameObject xpPool;
    [SerializeField] private GameObject bombPool;
    [SerializeField] private GameObject fireballPool;
    [SerializeField] private GameObject lightningPool;
    [SerializeField] private GameObject damagePool;
    [SerializeField] private GameObject laserPool;
    
    [SerializeField] private GameObject killCounter;
    
    public override void InstallBindings()
    {
        
        Container.Bind<EasyEnemyFactory>().FromComponentInNewPrefab(easyFactory).AsSingle().NonLazy();
        Container.Bind<MediumEnemyFactory>().FromComponentInNewPrefab(mediumFactory).AsSingle().NonLazy();
        Container.Bind<HardEnemyFactory>().FromComponentInNewPrefab(hardFactory).AsSingle().NonLazy();
        Container.Bind<EnemySpawner>().FromComponentInNewPrefab(enemySpawner).AsSingle().NonLazy();

        Container.Bind<KillCounter>().FromComponentInNewPrefab(killCounter).AsSingle().NonLazy();
        //Container.Bind<MapSetup>().FromComponentInHierarchy().AsSingle().NonLazy();
        
        Debug.Log("Install bindings");
    }
}