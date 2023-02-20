using UnityEngine;
using Zenject;

public class PlayerHealthProxyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerHealthProxy>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}