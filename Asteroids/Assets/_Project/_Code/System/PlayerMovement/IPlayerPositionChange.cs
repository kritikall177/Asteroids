using System;
using _Project._Code.Parameters;

namespace _Project._Code.System.PlayerMovement
{
    public interface IPlayerPositionChange
    {
        event Action<PlayerPositionParams> OnPositionChange;
    }
}