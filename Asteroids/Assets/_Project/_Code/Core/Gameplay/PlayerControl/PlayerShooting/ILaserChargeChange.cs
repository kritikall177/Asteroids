using System;

namespace _Project._Code.Core.Gameplay.PlayerControl.PlayerShooting
{
    public interface ILaserChargeChange
    {
        public event Action<int> OnLaserChargeChanged;
    }
}