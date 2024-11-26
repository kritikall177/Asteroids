using System;

namespace _Project._Code.System.PlayerShooting
{
    public interface ILaserChargeChange
    {
        public event Action<int> OnLaserChargeChanged;
    }
}