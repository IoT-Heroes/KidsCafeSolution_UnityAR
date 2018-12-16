using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGate : MonoBehaviour {
    // 1 : connectBand, 2 : infoAR
    public int scenePage = 2;

    private AndroidJavaObject curActivity;
    private AndroidJavaClass firstPluginJc;

    // Use this for initialization
    void Start () {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        curActivity = jc.GetStatic<AndroidJavaObject>("currentActivity");
        //CallScenePage();
        SetScenePage("ar_info");
    }

    // 모드에 따른 scene을 활성화 시킨다
    public void SetScenePage(string page)
    {
        if (page.Equals("ar_connect")) 
            scenePage = 1;
        else if (page.Equals("ar_info"))
            scenePage = 2;
        else return;

        SceneManager.LoadScene(scenePage);
    }

    public void CallScenePage()
    {
        curActivity.Call("setScenePage");
    }
}
