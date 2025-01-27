using System;

namespace _Project._Code.Core.Gameplay.PlayerControl.PlayerShooting
{
    public interface IOnBulletInvoke
    {
        public event Action OnBulletInvoke;
    }
}