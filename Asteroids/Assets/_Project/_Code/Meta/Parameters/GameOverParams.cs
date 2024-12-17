namespace _Project._Code.Meta.Parameters
{
    public struct GameOverParams
    {
        public int totalShots;
        public int laserUses;
        public int destroyedAsteroids;
        public int destroyedUFOs;

        public GameOverParams(int totalShots, int laserUses, int destroyedAsteroids, int destroyedUFOs)
        {
            this.totalShots = totalShots;
            this.laserUses = laserUses;
            this.destroyedAsteroids = destroyedAsteroids;
            this.destroyedUFOs = destroyedUFOs;
        }
    }
}