using GoogleMobileAds.Api;

public partial class GoogleManager
{
    public bool isTestMode;

    private void Start()
    {
        LoadBannerAd();
    }

    private AdRequest GetAdRequest()
    {
        return new AdRequest.Builder().AddTestDevice("652D1D85D16F87B2").Build();
        //return new AdRequest.Builder().Build();
    }

    #region  Banner

    const string bannerTestID = "ca-app-pub-3940256099942544/6300978111";
    const string bannerID = "ca-app-pub-8176709912093780/4257197214";
    BannerView bannerAd;

    private void LoadBannerAd()
    {
        bannerAd = new BannerView(isTestMode ? bannerTestID : bannerID, AdSize.SmartBanner, AdPosition.Top);
        bannerAd.LoadAd(GetAdRequest());
    }

    #endregion
}
