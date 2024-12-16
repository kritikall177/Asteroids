namespace _Project._Code.Services.Ads
{
    public interface IAds
    {
        public string AndroidBannerAdsId { get; }
        public string AndroidRewardedAdsId { get; }

        public void Initialize();
        public void ShowBannerAds();
        public void ShowRewardedAds();
    }
}