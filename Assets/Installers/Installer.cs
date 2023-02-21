using UnityEngine;
using Zenject;

public class Installer : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<EasyEnemyFactory>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<MediumEnemyFactory>().FromComponentInHierarchy().AsSingle().NonLazy();
        
        Container.Bind<PlayerHealth>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<PlayerHealthProxy>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<PlayerWeapons>().FromComponentInHierarchy().AsSingle().NonLazy();
        
        Container.Bind<ThrowingKnifePool>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<XpPool>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<BombPool>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<FireballPool>().FromComponentInHierarchy().AsSingle().NonLazy();
        
        Container.Bind<DamagePopupPool>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}