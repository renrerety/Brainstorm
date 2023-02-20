using UnityEngine;
using Zenject;

public class BombPoolInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<BombPool>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}