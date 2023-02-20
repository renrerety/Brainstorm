using UnityEngine;
using Zenject;

public class FireballPoolInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<FireballPool>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}