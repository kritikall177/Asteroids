namespace _Project._Code.Signals
{
    public struct UpdateLaserCountSignal
    {
        public int LaserCount;
        
        public UpdateLaserCountSignal(int laserCount)
        {
            LaserCount = laserCount;
        }
    }
}