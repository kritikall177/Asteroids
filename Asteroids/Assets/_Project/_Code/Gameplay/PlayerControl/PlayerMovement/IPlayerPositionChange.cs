using System;
using _Project._Code.Parameters;

namespace _Project._Code.Gameplay.PlayerControl.PlayerMovement
{
    public interface IPlayerPositionChange
    {
        event Action<PlayerPositionParams> OnPositionChange;
    }
}