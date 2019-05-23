using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class BandInfoManager : MonoBehaviour
{
    public TrackingObject[] obj_band;
    public TextMesh[] start_time_text;
    public TextMesh[] end_time_text;
    public TextMesh[] cost_text;

    private void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {

    }

    private void OnGUI()
    {
        for (int i = 0; i < obj_band.Length; i++)
        {
            TrackingObject band = obj_band[i];
            if (band.is_detected_)
            {
                StartCoroutine(getBandData(band.model_name, start_time_text[i], end_time_text[i], cost_text[i]));
            }
        }
    }

    [Serializable]
    public class Response
    {
        string result;
        string message;
        public Data[] data;
        public int state;
    }

    [Serializable]
    public class Data
    {
        string childId;
        public string childName;
        public string startDate;
        public string endDate;
        string bandDeviceId;
        public int amountPrice;
        int amountRest;
        int usingTime;
    }

    public IEnumerator getBandData(string bandId, TextMesh start_time_text, TextMesh end_time_text, TextMesh cost_text)
    {
        // TODO : band 여러개 될 경우 아래 bandId로 바꾸기
        string uri = "http://221.148.109.180:7080/heroes/visitingrecord/management?bandDeviceId=BAND3";
        UnityWebRequest www = UnityWebRequest.Get(uri);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Response response = JsonUtility.FromJson<Response>(www.downloadHandler.text);

            Debug.Log("test: " + www.downloadHandler.text);
            Debug.Log(response.state);
            if (response.state == 2002)
            {
                foreach (Data bandData in response.data)
                {
                    string[] startDate = bandData.startDate.Split(' ');
                    string[] endDate = bandData.endDate.Split(' ');
                    start_time_text.text = startDate[1];
                    end_time_text.text = endDate[1];
                    cost_text.text = bandData.amountPrice.ToString();
                }
            }
        }
    }
}
