using _Project._Code.Meta.DataConfig.Configs.ClassConfigs;

namespace _Project._Code.Meta.DataConfig
{
    public interface IGameConfigProvider
    {
        public void LoadConfig(GameConfig config);
    }
}