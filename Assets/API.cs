using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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
        public Data data;
    }

    [Serializable]
    public class Data
    {
        string id;
        public string name;
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

    [Serializable]
    public class Login
    {
        public string id;
        public string password;
    }

    private UnityWebRequest makePOST(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json; charset=utf-8");

        return request;
    }

    private IEnumerator request(UnityWebRequest www)
    {
        yield return www.SendWebRequest();
    }

    public void Request()
    {
        // use json (안됨)
        Login login = new Login();
        login.id = "KSH";
        login.password = "111";

        string json = JsonUtility.ToJson(login);

        Debug.Log(json);

        UnityWebRequest www = makePOST("http://192.168.0.4:7080/heroes/user/management/login", json);

        //UnityWebRequest www = UnityWebRequest.Post("http://192.168.0.4:7080/heroes/user/management/login", json);
        //www.SetRequestHeader("Content-Type", "application/json;charset=UTF-8");

        StartCoroutine(request(www));

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // show result as text
            Debug.Log("success!");
            //Response response = JsonUtility.FromJson<Response>(www.downloadHandler.text);

            //responseText.text = response.data.name;
            responseText.text = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
            Debug.Log(www.downloadHandler.data.ToString());
            Debug.Log(www.downloadHandler.text);
        }
    }
}
