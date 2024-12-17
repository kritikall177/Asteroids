using System;

namespace _Project._Code.Core.Gameplay.Score
{
    public interface IChangeScoreAction
    {
        public event Action OnScoreChanged;
    }
}