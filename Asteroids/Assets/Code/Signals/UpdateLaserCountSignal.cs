namespace Code.Signals
{
    public class UpdateLaserCountSignal
    {
        public int LaserCount;
        
        public UpdateLaserCountSignal(int laserCount)
        {
            LaserCount = laserCount;
        }
    }
}