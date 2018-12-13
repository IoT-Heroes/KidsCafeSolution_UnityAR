using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackingObject : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour trackableBehaviour;
    public bool is_detected_ = false;
    public string model_name = "";

    // Use this for initialization
    void Start () {
        trackableBehaviour = GetComponent<TrackableBehaviour>();
        if (trackableBehaviour)
        {
            // 상태가 변하는 것에 대해 알 수 있도록 등록
            trackableBehaviour.RegisterTrackableEventHandler(this);
        }

        // TODO : TrackableName이 imageTarget 이름을 가져옴 -> 이걸로 밴드 디바이스 가져와서 내일 네트워크 통신 구현할 것
        model_name = trackableBehaviour.TrackableName;
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED)
        {
            is_detected_ = true;
        }
        else
        {
            is_detected_ = false;
        }
        //throw new System.NotImplementedException();
    }
}
