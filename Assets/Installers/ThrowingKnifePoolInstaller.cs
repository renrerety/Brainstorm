using UnityEngine;
using Zenject;

public class ThrowingKnifePoolInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ThrowingKnifePool>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}