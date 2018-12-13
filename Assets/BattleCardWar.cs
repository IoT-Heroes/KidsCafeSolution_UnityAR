using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCardWar : MonoBehaviour {
    public TrackingObject obj_java_book;
    public TrackingObject obj_arvr_book;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        if (obj_arvr_book.is_detected_)
        {
            GUI.Button(new Rect(300, 300, 240, 120), "ARVR 책 인식됨");
        }

        if (obj_java_book.is_detected_)
        {
            GUI.Button(new Rect(600, 300, 240, 120), "JAVA 책 인식됨");
        }
    }
}
