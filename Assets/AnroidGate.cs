using UnityEngine;
using System.Collections;

public class AnroidGate : MonoBehaviour {

    private AndroidJavaObject curActivity;
    private AndroidJavaClass firstPluginJc;

    public string kidId;

    // Use this for initialization
    void Start () {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        curActivity = jc.GetStatic<AndroidJavaObject>("currentActivity");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // 인식한 밴드 id를 안드로이드에 보낸다
    public void SetBandId(string id)
    {
        curActivity.Call("SetBandId", id);
    }

    // 앱으로 부터 밴드를 연결시키고자 하는 아이의 id를 받아온다
    public void SetKidId(string id)
    {
        kidId = id;
    }
}
