using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;

namespace Managers
{
    public class AdManager : MonoBehaviour
    {
        public static AdManager Instance;

    private bool rvStarted, intersestialStarted;
    private Action OnRVShowedEvent;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        GameDistribution.OnResumeGame = OnResumeGame;
        GameDistribution.OnPauseGame = OnPauseGame;
        GameDistribution.OnPreloadRewardedVideo = OnPreloadRewardedVideo;
        GameDistribution.OnRewardGame = OnRewardedVideoSuccess;

        GameDistribution.Instance.PreloadRewardedAd();
    }

    private void OnPauseGame()
    {
        if (GameManager.Instance != null) GameManager.Instance.StopGame();
        AudioListener.pause = true;
    }

    private void OnResumeGame()
    {
        if (intersestialStarted) TapToResumePanel.instance.Show();
        else if (GameManager.Instance != null) GameManager.Instance.ResumeGame();

        if (LoadingPanel.instance.isShown) LoadingPanel.instance.Hide();

        rvStarted = false;
        intersestialStarted = false;
        AudioListener.pause = false;


#if !UNITY_EDITOR
        Application.ExternalEval("window.focus();");
#endif
    }

    private void OnPreloadRewardedVideo(int loaded)
    {
        if (loaded == 1) return;

        GameDistribution.Instance.PreloadRewardedAd();
    }

    private void OnRewardedVideoSuccess()
    {
        GameDistribution.Instance.PreloadRewardedAd();
        RewardPlayer();
    }

    private void RewardPlayer() => OnRVShowedEvent?.Invoke();

    private IEnumerator CheckReachability(Action<bool> action)
    {
        var webRequest = UnityWebRequest.Get("https://opensheet.elk.sh/1U7QsW86isvPeQqzwQHNkGFKDGohtGftCuXx4EFLIjlo/Test");
        yield return webRequest.SendWebRequest();
        action?.Invoke(string.IsNullOrEmpty(webRequest.error));
    }

    public void PrepareOnRVShownEvent(Action onRewardPlayer) => OnRVShowedEvent = onRewardPlayer;

    public void ShowRewardedAd()
    {
        if (rvStarted) return;

        rvStarted = true;

        OnPauseGame();
        LoadingPanel.instance.Show();

        StartCoroutine(CheckReachability((isReachable) =>
        {
            if (!isReachable)
            {
                OnResumeGame();

                MessageBox.instance.Show("Internet Connection Error", "Could not load the ad, Please check your connection and try again.", MessageBox.TextSize.Small);

                rvStarted = false;

                return;
            }

#if UNITY_EDITOR
            OnPauseGame();
            OnResumeGame();
            RewardPlayer();
#else
            GameDistribution.Instance.ShowRewardedAd();
#endif
        }));
    }

    public void ShowAd()
    {
        if (intersestialStarted) return;

        intersestialStarted = true;

        StartCoroutine(CheckReachability((isReachable) =>
        {
            if (!isReachable)
            {
                intersestialStarted = false;
                return;
            }

#if UNITY_EDITOR
            OnPauseGame();
            OnResumeGame();            
#else
            GameDistribution.Instance.ShowAd();
#endif
        }));
    }
    }
}