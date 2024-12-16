using System;

namespace _Project._Code.Gameplay.Score
{
    public interface IChangeScoreAction
    {
        public event Action OnScoreChanged;
    }
}