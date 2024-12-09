using Firebase.RemoteConfig;

namespace _Project._Code._DataConfig
{
    public interface IConfigsDataBase
    {
        public void Initialize(FirebaseRemoteConfig databaseConfig);
    }
}