using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class API : MonoBehaviour {

    private const string URL = "http://192.168.0.4:7080/heroes";
    private const string IoTMakers_ENDPOINT = "https://iotmakers.kt.com:443/api/v1/device";
    public Text responseText;
    private const string API_KEY = "";

    [Serializable]
    public class Response
    {
        string result;
        string message;
        Data data;
    }

    [Serializable]
    public class Data
    {
        string id;
        string name;
        string password;
        string phoneNumber;
        Boolean isAuthor;
        string token;
        Child[] child;
    }

    [Serializable]
    public class Child
    {
        string id;
        string parentId;
        string userId;
        string name;
        Boolean isBandWearing;
        string sex;
        string birth;
        int targetActivityFigure;
        int age;
        int height;
        int weight;
        EatableFood[] eatableFoodList;
        Boolean currentCafeVisitingRecord;
        Boolean cafeVisitingRecord;
    }

    [Serializable]
    public class EatableFood
    {
        string id;
        string calorie;
        string name;
    }
    
    public void Request()
    {
        //WWWForm form = new WWWForm();
        //form.AddField("id", "KSH");
        //form.AddField("password", "111");

        //string loginPath = "/user/management/login";

        //UnityWebRequest www = UnityWebRequest.Post(IoTMakers_ENDPOINT + loginPath, form);
        //yield return www.Send ();

        //if (www.error != null)
        //{
        //    Debug.Log("Error : " + www.error);
        //}
        //else
        //{
        //    Debug.Log("Response : " + www.downloadHandler.text);

        //    Response response = JsonUtility.FromJson<Response>(www.downloadHandler.text);

        //    responseText.text = response.ToString();
        //}


        WWWForm form = new WWWForm();
        // header 정의
        Dictionary<string, string> headers = form.headers;
        headers["test"] = "eeee";

        // path 정의
        string path = "/zone1/deviceEvents";
        string loginPath = "/user/management/login";

        // post body?
        form.AddField("id", "KSH");
        form.AddField("password", "111");
        byte[] rawFormData = form.data;

        WWW request = new WWW(URL + loginPath, form);

        Debug.Log(request.text);
        StartCoroutine(OnResponse(request));
        //StartCoroutine(WaitWWW(form, URL + loginPath));
    }

    private IEnumerator OnResponse(WWW req)
    {
        yield return req;
        if (req.isDone && req.error == null)
        {
            responseText.text = req.text;
        }
    }

    private IEnumerator WaitWWW(WWWForm form, string url)
    {
        WWW www = new WWW(url, form);
        yield return www;
        if (www.isDone && www.error == null)
        {
            responseText.text = www.text;
        }
    }
}
