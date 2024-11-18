using System;
using Code;
using UnityEngine;
using Zenject;

public class Button : MonoBehaviour
{
    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void Click()
    {
        _signalBus.Fire<GameStartSignal>();
    }
}
