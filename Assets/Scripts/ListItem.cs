using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ListItem : MonoBehaviour {

    public Image thumbnail;
    public Text description;
    public Text distanceText;
    static Vector2 devicelatlong;
    double m_lat;
    double m_lon;


    private void Start()
    {
        LocationProviderFactory.Instance.DeviceLocationProvider.OnLocationUpdated += OnUpdateLocationCalled;
    }

    public void Init(string thumbnailURL , string descriptionText , string objectID , double lat,double lon)
    {
        StartCoroutine(GetThumbnail(thumbnailURL));
        description.text = descriptionText;
        GetComponent<Button>().onClick.AddListener(delegate { TaskOnClick(objectID); });
        m_lat = lat;
        m_lon = lon;

    }

    private void OnUpdateLocationCalled(Location location)
    {
        Debug.Log(location.LatitudeLongitude);
        SetDistance(DistanceCalculator.DistanceBetweenPlaces(location.LatitudeLongitude.x, location.LatitudeLongitude.y,m_lat,m_lon));
    }


    public void SetDistance(double x)
    {
        int  distance = (int)x;
        double Miles;

        Miles = x / 1609.344f;
        Miles = Miles * 10;
        Miles = System.Math.Truncate(Miles) / 10;
        if (Miles > 0.1f)
        {
            distanceText.text = "" + Miles + " Miles";
        }
        else
        {
            int yards = (int)(x * 1.094f);
            distanceText.text = "" + yards + " Yards";
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

            Sprite thumbnailSprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            thumbnail.sprite = thumbnailSprite;

        }
    }

    void TaskOnClick(string ObjectID)
    {
        Debug.Log(ObjectID);
    }
}
