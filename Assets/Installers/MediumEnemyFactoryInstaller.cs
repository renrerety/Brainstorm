using UnityEngine;
using Zenject;

public class MediumEnemyFactoryInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MediumEnemyFactory>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}