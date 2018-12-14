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
    
    //public void Request()
    //{
    //    //StartCoroutine(Upload());

    //    Login login = new Login();
    //    login.id = "KSH";
    //    login.password = "111";

    //    string json = JsonUtility.ToJson(login);

    //    StartCoroutine(Post("http://192.168.0.4:7080/heroes/user/management/login", json));

    //    if (www.isNetworkError || www.isHttpError)
    //    {
    //        Debug.Log(www.error);
    //    }
    //    else
    //    {
    //        // show result as text
    //        Debug.Log("success!");
    //        Response response = JsonUtility.FromJson<Response>(www.downloadHandler.text);

    //        responseText.text = response.data.name;

    //        //byte[] results = www.downloadHandler.data;
    //        //responseText.text = www.downloadHandler.text;
    //    }
    //    //WWWForm form = new WWWForm();
    //    //// header 정의
    //    //Dictionary<string, string> headers = form.headers;
    //    //headers["test"] = "eeee";

    //    //// path 정의
    //    //string path = "/zone1/deviceEvents";
    //    //string loginPath = "/user/management/login";

    //    //// post body?
    //    //form.AddField("id", "KSH");
    //    //form.AddField("password", "111");
    //    //byte[] rawFormData = form.data;

    //    //WWW request = new WWW(URL + loginPath, form);

    //    //Debug.Log(request.text);
    //    //StartCoroutine(OnResponse(request));
    //}

    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", "KSH");
        form.AddField("password", "111");

        UnityWebRequest www = UnityWebRequest.Post("http://192.168.0.4:7080/heroes/user/management/login", form);
        www.SetRequestHeader("content-type", "application/json;");
        //UnityWebRequest www = UnityWebRequest.Get("http://192.168.0.4:7080/heroes/data/food/select");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // show result as text
            Debug.Log("success!");
            Response response = JsonUtility.FromJson<Response>(www.downloadHandler.text);

            responseText.text = response.data.name;

            //byte[] results = www.downloadHandler.data;
            //responseText.text = www.downloadHandler.text;
        }
    }

    [Serializable]
    public class Login
    {
        public string id;
        public string password;
    }

    //IEnumerator Post(string url, string bodyJsonString)
    //{
    //    var request = new UnityWebRequest(url, "POST");
    //    byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
    //    request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
    //    request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
    //    request.SetRequestHeader("Content-Type", "application/json");

    //    yield return request.Send();

    //    Debug.Log("Status Code: " + request.responseCode);
    //}

    private UnityWebRequest makeWWW(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        //byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        return request;
    }

    private IEnumerator request(UnityWebRequest www)
    {
        yield return www.SendWebRequest();
    }

    public void Request()
    {
        //StartCoroutine(Upload());

        Login login = new Login();
        login.id = "KSH";
        login.password = "111";

        string json = JsonUtility.ToJson(login);

        Debug.Log(json);

        //UnityWebRequest www = makeWWW("http://192.168.0.4:7080/heroes/user/management/login", json);

        UnityWebRequest www = UnityWebRequest.Post("http://192.168.0.4:7080/heroes/user/management/login", json);
        www.SetRequestHeader("Content-Type", "application/json;charset=UTF-8");

        StartCoroutine(request(www));

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // show result as text
            Debug.Log("success!");
            Response response = JsonUtility.FromJson<Response>(www.downloadHandler.text);

            //responseText.text = response.data.name;
            responseText.text = www.downloadHandler.data.ToString();
            Debug.Log(www.downloadHandler.data.ToString());
            Debug.Log(www.downloadHandler.text);
        }
    }
}
