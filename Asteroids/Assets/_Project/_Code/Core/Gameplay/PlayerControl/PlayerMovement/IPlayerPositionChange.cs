using System;
using _Project._Code.Meta.Parameters;

namespace _Project._Code.Core.Gameplay.PlayerControl.PlayerMovement
{
    public interface IPlayerPositionChange
    {
        event Action<PlayerPositionParams> OnPositionChange;
    }
}