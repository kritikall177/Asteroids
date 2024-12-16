using System;

namespace _Project._Code.Services.Analytics
{
    public interface IFirebaseConfigUpdated
    {
        public event Action OnFirebaseConfigUpdated;
    }
}