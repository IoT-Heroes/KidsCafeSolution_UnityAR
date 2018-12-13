using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCardWar : MonoBehaviour {
    public TrackingObject obj_java_book;
    public TrackingObject obj_arvr_book;

    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject g = new GameObject("abc" + i.ToString("D3"));
        }
    }

    // Use this for initialization
    void Start () {
        //GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("BookTag");

        //foreach (GameObject go in gameObjects)
        //{
        //    Debug.Log(go + " is an active object " + go.GetInstanceID());
        //}


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        if (obj_arvr_book.is_detected_)
        {
            GUI.Button(new Rect(300, 300, 240, 120), obj_arvr_book.model_name + "mmmm");
        }

        if (obj_java_book.is_detected_)
        {
            GUI.Button(new Rect(600, 300, 240, 120), obj_java_book.model_name);
        }
    }


}
