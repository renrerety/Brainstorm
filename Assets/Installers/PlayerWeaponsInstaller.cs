using UnityEngine;
using Zenject;

public class PlayerWeaponsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerWeapons>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}