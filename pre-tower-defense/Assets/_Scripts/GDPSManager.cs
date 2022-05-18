using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using TMPro;
public class GDPSManager : MonoBehaviour
{
    public TMPro.TMP_Text GGPSText;
    public EnemySpawner eSReferencia;
    public void Start()
    {
        PlayGamesPlatform.Activate();
        
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    public void OnEnable()
    {
        eSReferencia.EnOlaGanada += DoAchievementUnlock;
    }

    private void OnDisable()
    {
        eSReferencia.EnOlaGanada -= DoAchievementUnlock;
    }

    internal void DoAchievementUnlock()
    {
        string mStatus;
        Social.ReportProgress(
            GPGSIds.achievement_oleadaganada,
            100.0f,
            (bool success) =>
            { 
                 mStatus = success ? "Unlocked successfully." : "*** Failed to unlock ach.";
                GGPSText.text = mStatus;
            });
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            Debug.Log("good auth");
            GGPSText.text = $"good Auth \n {Social.localUser.userName}\n ( {Social.localUser.id} )";
        }
        else
        {
            Debug.Log("bad auth");
            GGPSText.text = "bad Auth";
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }
}
