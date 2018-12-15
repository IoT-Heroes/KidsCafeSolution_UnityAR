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

    public IEnumerator getIoTMakersAPI()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://iotmakers.kt.com:443/api/v1/device/zone1/deviceEvents");
        string token = "Bearer eyJhbGciOiJSUzI1NiJ9.eyJzdmNfdGd0X3NlcSI6IjEwMDAwMDY0NTEiLCJ1c2VyX25hbWUiOiJsb2NrYW5kbG9jazEiLCJwdWJfdGltZSI6MTU0NDg1MDE4NDM3NSwibWJyX2lkIjoibG9ja2FuZGxvY2sxIiwibWJyX3NlcSI6IjEwMDAwMDYzMzQiLCJtYnJfY2xhcyI6IjAwMDMiLCJhdXRob3JpdGllcyI6WyJST0xFX09QRU5BUEkiLCJST0xFX1VTRVIiXSwicGxhdGZvcm0iOiIzTVAiLCJ0aGVtZV9jZCI6IlBUTCIsImNsaWVudF9pZCI6IllPa1ZVOHJCRVhsaWlsVloiLCJhdWQiOlsiSU9ULUFQSSJdLCJ1bml0X3N2Y19jZCI6IjAwMSIsInNjb3BlIjpbInRydXN0Il0sImRzdHJfY2QiOiIwMDEiLCJjb21wYW55IjoiS3QiLCJtYnJfbm0iOiLquYDtmY3qt5wiLCJleHAiOjE1NDU0NTAxODQsImp0aSI6ImFkYTQ2N2UzLTJiYTMtNGQ3ZC1iNTc2LTNlZTYzYTU2NmVlZSJ9.UEISQ_ru_llgcTsK0hxkJuDa7kSYPBHMSwczsznHmB3OURaKWwqx3m_2UJlRr5rUjgS8q9yvTGGZNSomD_W23oErpu32xUE9Wk4kGz-D53kz7VnliMwNPzWC-MHG5fV5kx-rE2bQcp51DGppFDLQbuIciSQPFfnzEAjCE2EE_04in2Ehds2DqZLNLrSywodOhDLWFT4UFIqqhAxsh7E2gzu9HSGu5zLV3GJCO6rDghFJMcHcWpafHogPi1xPRrCWB_OxBVuomRciSFPYXpe1XxLy7RuPR83dgchIpAjul4-3eiuEO9VqRSGSJjbOZoo2FiGpBTgtawNN-UvuEz-udA";
        www.SetRequestHeader("Authorization", token);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;

            responseText.text = www.downloadHandler.text;
        }
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

    // 통신 성공!!
    private IEnumerator testPost()
    {
        Login login = new Login();
        login.id = "KSH";
        login.password = "111";

        string json = JsonUtility.ToJson(login);

        Debug.Log(json);

        UnityWebRequest www = makePOST("http://220.94.248.34:7080/heroes/user/management/login", json);

        yield return www.SendWebRequest();

        // 데이터는 yield 다음~
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

    public void Request()
    {
        StartCoroutine(testPost());
    }
}
