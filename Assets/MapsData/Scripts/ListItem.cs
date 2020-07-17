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
    public string ObjectID;


    private void Start()
    {
        LocationProviderFactory.Instance.DeviceLocationProvider.OnLocationUpdated += OnUpdateLocationCalled;
    }
    private void OnDestroy()
    {
        LocationProviderFactory.Instance.DeviceLocationProvider.OnLocationUpdated -= OnUpdateLocationCalled;
    }

    public void Init(string thumbnailURL , string descriptionText , string objectID , double lat,double lon)
    {

        description.text = descriptionText;
        GetComponent<Button>().onClick.AddListener(delegate { TaskOnClick(objectID); });
        m_lat = lat;
        m_lon = lon;
        ObjectID = objectID;
        GetImageFromMapItem();
    }

    private void OnUpdateLocationCalled(Location location)
    {
        SetDistance(DistanceCalculator.DistanceBetweenPlaces(location.LatitudeLongitude.x, location.LatitudeLongitude.y,m_lat,m_lon));
    }

    public void GetImageFromMapItem()
    {
        foreach (GameObject mapItem in MapPointsPlacement._spawnedObjects)
        {
            MapItem mp = mapItem.GetComponent<MapItem>();
            if (mp.ObjectID == ObjectID)
            {
                if (mp.isThumbnailLoaded)
                {
                    thumbnail.sprite = mp.thumbnail.sprite;
                    return;
                }
            }
        }
    }

    public void SetDistance(double x)
    {
        double Miles = DistanceCalculator.ConvertToMiles(x);

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


    void TaskOnClick(string _ObjectID)
    {
        Debug.Log(_ObjectID);
    }
}
