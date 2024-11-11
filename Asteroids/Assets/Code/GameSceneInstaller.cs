using System;
using Code;
using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IInputSystem>().To<InputSystem>().FromNew().AsSingle().NonLazy();
    }
}