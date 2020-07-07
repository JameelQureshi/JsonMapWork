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
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            Root locationData = JsonUtility.FromJson<Root>(www.downloadHandler.text);

            Debug.Log(locationData.ObjectLocations.Count);


            StartCoroutine(GetThumbnail(locationData.ObjectLocations[0].ThumbnailURL));


            //foreach (ObjectLocation objectLocation in locationData.ObjectLocations)
            //{
            //    Debug.Log(objectLocation.ObjectID);
            //    Debug.Log(objectLocation.ThumbnailURL);
            //    Debug.Log(objectLocation.Description);
            //    Debug.Log(objectLocation.Location);
            //    StartCoroutine(GetThumbnail(objectLocation.ThumbnailURL));

            //}
        }
    }

    IEnumerator GetThumbnail(string uri)
    {

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(uri);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.responseCode);
        }
        else
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(www);

            Sprite thumbnail = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            image.sprite = thumbnail;

        }
    }


}


[Serializable]
public class Root
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
