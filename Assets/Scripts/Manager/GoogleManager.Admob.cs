/*
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

public partial class GoogleManager
{
    public bool isTestMode;

    private void Start()
    {
        LoadBannerAd();
        LoadFrontAd();
        LoadRewardAd();
    }

    private void Update()
    {
        frontAdBtn.interactable = frontAd.IsLoaded();
        rewardAdBtn.interactable = rewardAd.IsLoaded();
    }

    private AdRequest GetAdRequest()
    {
        return new AdRequest.Builder().AddTestDevice("652D1D85D16F87B2").Build();
    }

    #region  Banner

    const string bannerTestID = "ca-app-pub-3940256099942544/6300978111";
    const string bannerID = "";
    BannerView bannerAd;

    private void LoadBannerAd()
    {
        bannerAd = new BannerView(isTestMode ? bannerTestID : bannerID, AdSize.SmartBanner, AdPosition.Bottom);
        bannerAd.LoadAd(GetAdRequest());
        ToggleBannerAd(false);
    }

    public void ToggleBannerAd(bool b)
    {
        if (b) bannerAd.Show();
        else bannerAd.Hide();
    }

    #endregion

    #region  Front

    const string frontTestID = "ca-app-pub-3940256099942544/8691691433";
    const string frontID = "";
    InterstitialAd frontAd;

    private void LoadFrontAd()
    {
        frontAd = new InterstitialAd(isTestMode ? frontTestID : frontID);
        frontAd.LoadAd(GetAdRequest());
        frontAd.OnAdClosed += (sender, e) =>
        {
            logText.text = "전면광고 성공";
        };
    }

    public void ShowFrontAd()
    {
        frontAd.Show();
        LoadFrontAd();
    }

    #endregion

    #region  Reward

    const string rewardTestID = "ca-app-pub-3940256099942544/5224354917";
    const string rewardID = "";
    RewardedAd rewardAd;

    private void LoadRewardAd()
    {
        rewardAd = new RewardedAd(isTestMode ? rewardTestID : rewardID);
        rewardAd.LoadAd(GetAdRequest());
        rewardAd.OnUserEarnedReward += (sender, e) =>
        {
            logText.text = "리워드 광고 성공";
        };
    }

    public void ShowRewardAd()
    {
        rewardAd.Show();
        LoadRewardAd();
    }

    #endregion
}
*/
