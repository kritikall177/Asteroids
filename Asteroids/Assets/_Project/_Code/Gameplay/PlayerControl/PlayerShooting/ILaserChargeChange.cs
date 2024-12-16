using System;

namespace _Project._Code.Gameplay.PlayerControl.PlayerShooting
{
    public interface ILaserChargeChange
    {
        public event Action<int> OnLaserChargeChanged;
    }
}