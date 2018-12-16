using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BandConnectManager : MonoBehaviour {
    public TrackingObject obj_band1;
    public TrackingObject obj_band2;
    public Text kid_id_text;

    public AndroidGate gate;

    private void Awake()
    {
    }

    // Use this for initialization
    void Start () {
        gate.CallKidId();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        kid_id_text.text = gate.kidId;
        // 인식되면 밴드 정보를 안드로이드로 넘겨준다
        if (obj_band1.is_detected_)
        {
            gate.SetBandId(obj_band1.model_name);
            GUI.Button(new Rect(300, 300, 240, 120), obj_band1.model_name);
        }

        if (obj_band2.is_detected_)
        {
            gate.SetBandId(obj_band2.model_name);
            GUI.Button(new Rect(600, 300, 240, 120), obj_band2.model_name);
        }
    }


}
