using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LocationDataManager : MonoBehaviour
{
    public const string JsonURL = "http://euphoriaxr.com/Files/data.json";
    // Start is called before the first frame update

    public Image image;

    void Start()
    {
        StartCoroutine(DownloadLocationData());
    }


    IEnumerator DownloadLocationData()
    {
        UnityWebRequest www = UnityWebRequest.Get(JsonURL);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
           
            LocationData locationData = JsonUtility.FromJson<LocationData>(www.downloadHandler.text);
            Debug.Log(locationData.ObjectLocations.Count);
            ListDataCreator.instance.Populate(locationData);

        }
    }

  

}


[Serializable]
public class LocationData
{
    public List<ObjectLocation> ObjectLocations;
}

[Serializable]
public class ObjectLocation
{
    public string ObjectID;
    public string ThumbnailURL;
    public string Description;
    public string Location;

}
