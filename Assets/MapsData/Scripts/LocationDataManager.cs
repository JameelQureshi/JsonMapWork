using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LocationDataManager : MonoBehaviour
{
    public string JsonURL = "";
    public static LocationData locationData;
    // Start is called before the first frame update
    public static int Radius
    {
        set
        {
            PlayerPrefs.SetInt("Radius", value);
        }
        get
        {
            return PlayerPrefs.GetInt("Radius", 5);
        }
    }


    void Start()
    {
        StartCoroutine(DownloadLocationData());
        LocationProviderFactory.Instance.DeviceLocationProvider.OnLocationUpdated += OnUpdateLocationCalled;
    }

    private void OnUpdateLocationCalled(Location location)
    {
        if (locationData!=null)
        {
            Debug.Log(locationData.ObjectLocations.Count);
            ListDataCreator.instance.Populate(locationData);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        LocationProviderFactory.Instance.DeviceLocationProvider.OnLocationUpdated -= OnUpdateLocationCalled;
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

            locationData = JsonUtility.FromJson<LocationData>(www.downloadHandler.text);
            //Debug.Log(locationData.ObjectLocations.Count);
            //ListDataCreator.instance.Populate(locationData);

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
