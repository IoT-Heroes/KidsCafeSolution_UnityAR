using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;


public class ZoneInfoManager : MonoBehaviour
{
    public TrackingObject[] obj_zone;
    public TextMesh[] humid_text;
    public TextMesh[] temp_text;

    private void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {

    }

    private void OnGUI()
    {
        for (int i = 0; i < obj_zone.Length; i++)
        {
            TrackingObject zone = obj_zone[i];
            if (zone.is_detected_)
            {
                StartCoroutine(getZoneData(zone.model_name, humid_text[i], temp_text[i]));
            }
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

    public IEnumerator getZoneData(string zoneId, TextMesh humid_text, TextMesh temp_text)
    {
        // TODO : zone 여러개 될 경우 아래 zoneId로 바꾸기

        string uri = "https://iotmakers.kt.com:443/api/v1/streams/" + "zone1" + "/log?period=10000&count=2";
        UnityWebRequest www = UnityWebRequest.Get(uri);
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
