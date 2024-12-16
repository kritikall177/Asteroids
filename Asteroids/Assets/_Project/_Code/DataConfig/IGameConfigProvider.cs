using _Project._Code.DataConfig.Configs.ClassConfigs;

namespace _Project._Code.DataConfig
{
    public interface IGameConfigProvider
    {
        public void LoadConfig(GameConfig config);
    }
}