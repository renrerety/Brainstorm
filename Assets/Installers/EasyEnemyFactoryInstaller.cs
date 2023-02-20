using UnityEngine;
using Zenject;

public class EasyEnemyFactoryInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<EasyEnemyFactory>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}