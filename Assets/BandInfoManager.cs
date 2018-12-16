using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;


public class BandInfoManager : MonoBehaviour
{
    public TrackingObject obj_band1;
    public TrackingObject obj_band2;
    public Text humid_text;
    public Text temp_text;

    private void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {

    }

    private void OnGUI()
    {
        // 인식되면 밴드 정보를 안드로이드로 넘겨준다
        if (obj_band1.is_detected_)
        {
            StartCoroutine(getZoneData(humid_text, temp_text));
        }

        // zone이라고 가정
        if (obj_band2.is_detected_)
        {
            GUI.Button(new Rect(300, 300, 240, 120), obj_band2.model_name);
        }
    }

    [Serializable]
    public class IoTMakersResponse
    {
        public string responseCode;
        public IoTMakersData[] data;
    }

    [Serializable]
    public class IoTMakersData
    {
        string svcCode;
        string svcTgtSeq;
        string groupTagCd;
        int spotDevSeq;
        string occDt;
        public IoTMakersAttribute attributes;
    }

    [Serializable]
    public class IoTMakersAttribute
    {
        public int humid;
        public int temp;
    }

    // TODO : zone 조회시
    public IEnumerator getZoneData(Text humid_text, Text temp_text)
    {
        UnityWebRequest www = UnityWebRequest.Get("https://iotmakers.kt.com:443/api/v1/streams/zone1/log?period=10000&count=2");
        string token = "Bearer eyJhbGciOiJSUzI1NiJ9.eyJzdmNfdGd0X3NlcSI6IjEwMDAwMDY0NTEiLCJ1c2VyX25hbWUiOiJsb2NrYW5kbG9jazEiLCJwdWJfdGltZSI6MTU0NDg1MDE4NDM3NSwibWJyX2lkIjoibG9ja2FuZGxvY2sxIiwibWJyX3NlcSI6IjEwMDAwMDYzMzQiLCJtYnJfY2xhcyI6IjAwMDMiLCJhdXRob3JpdGllcyI6WyJST0xFX09QRU5BUEkiLCJST0xFX1VTRVIiXSwicGxhdGZvcm0iOiIzTVAiLCJ0aGVtZV9jZCI6IlBUTCIsImNsaWVudF9pZCI6IllPa1ZVOHJCRVhsaWlsVloiLCJhdWQiOlsiSU9ULUFQSSJdLCJ1bml0X3N2Y19jZCI6IjAwMSIsInNjb3BlIjpbInRydXN0Il0sImRzdHJfY2QiOiIwMDEiLCJjb21wYW55IjoiS3QiLCJtYnJfbm0iOiLquYDtmY3qt5wiLCJleHAiOjE1NDU0NTAxODQsImp0aSI6ImFkYTQ2N2UzLTJiYTMtNGQ3ZC1iNTc2LTNlZTYzYTU2NmVlZSJ9.UEISQ_ru_llgcTsK0hxkJuDa7kSYPBHMSwczsznHmB3OURaKWwqx3m_2UJlRr5rUjgS8q9yvTGGZNSomD_W23oErpu32xUE9Wk4kGz-D53kz7VnliMwNPzWC-MHG5fV5kx-rE2bQcp51DGppFDLQbuIciSQPFfnzEAjCE2EE_04in2Ehds2DqZLNLrSywodOhDLWFT4UFIqqhAxsh7E2gzu9HSGu5zLV3GJCO6rDghFJMcHcWpafHogPi1xPRrCWB_OxBVuomRciSFPYXpe1XxLy7RuPR83dgchIpAjul4-3eiuEO9VqRSGSJjbOZoo2FiGpBTgtawNN-UvuEz-udA";
        www.SetRequestHeader("Authorization", token);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            IoTMakersResponse response = JsonUtility.FromJson<IoTMakersResponse>(www.downloadHandler.text);

            Debug.Log(www.downloadHandler.text);
            Debug.Log(response.responseCode);
            if (response.responseCode.Equals("OK"))
            {
                //IoTMakersData[] datas = response.data;
                foreach (IoTMakersData data in response.data)
                {
                    IoTMakersAttribute attribute = data.attributes;
                    if (attribute.humid > 0)
                    {
                        humid_text.text = "습도 : " + attribute.humid;
                    }
                    else if (attribute.temp > 0)
                    {
                        temp_text.text = "온도 : " + attribute.temp;
                    }
                }
            }
        }
    }
}
