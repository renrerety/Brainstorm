using UnityEngine;
using Zenject;

public class XpPoolInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<XpPool>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}