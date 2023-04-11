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
        Container.Bind<EasyEnemyFactory>().FromComponentOn(easyFactory).AsSingle().NonLazy();
        Container.Bind<MediumEnemyFactory>().FromComponentOn(mediumFactory).AsSingle().NonLazy();
        Container.Bind<HardEnemyFactory>().FromComponentOn(hardFactory).AsSingle().NonLazy();
        Container.Bind<EnemySpawner>().FromComponentOn(enemySpawner).AsSingle().NonLazy();
        
        Container.Bind<PlayerHealth>().FromComponentOn(player).AsSingle().NonLazy();
        Container.Bind<PlayerHealthProxy>().FromComponentOn(player).AsSingle().NonLazy();
        Container.Bind<PlayerWeapons>().FromComponentOn(player).AsSingle().NonLazy();
        Container.Bind<PlayerMovement>().FromComponentOn(player).AsSingle().NonLazy();
        
        Container.Bind<ThrowingKnifePool>().FromComponentOn(throwingKnifePool).AsSingle().NonLazy();
        Container.Bind<XpPool>().FromComponentOn(xpPool).AsSingle();
        Container.Bind<BombPool>().FromComponentOn(bombPool).AsSingle().NonLazy();
        Container.Bind<FireballPool>().FromComponentOn(fireballPool).AsSingle().NonLazy();
        Container.Bind<LightningPool>().FromComponentOn(lightningPool).AsSingle().NonLazy();
        Container.Bind<DamagePopupPool>().FromComponentOn(damagePool).AsSingle().NonLazy();
        Container.Bind<LaserPool>().FromComponentOn(laserPool).AsSingle().NonLazy();
        
        Container.Bind<KillCounter>().FromNewComponentOn(killCounter).AsSingle().NonLazy();
        //Container.Bind<MapSetup>().FromComponentInHierarchy().AsSingle().NonLazy();
        
        Debug.Log("Install bindings");
    }
}