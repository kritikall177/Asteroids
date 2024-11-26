using System;

namespace _Project._Code.System.Score
{
    public interface IChangeScoreAction
    {
        public event Action OnScoreChanged;
    }
}