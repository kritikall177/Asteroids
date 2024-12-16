using Firebase.RemoteConfig;

namespace _Project._Code.DataConfig
{
    public interface IConfigsDataBase
    {
        public void Initialize(FirebaseRemoteConfig databaseConfig);
    }
}