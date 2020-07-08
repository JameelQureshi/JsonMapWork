using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListDataCreator : MonoBehaviour {

    public GameObject prefab;
    public GameObject canvas;

    public static ListDataCreator instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void Populate(LocationData locationData)
    {
        GameObject newObj; // Create GameObject instance
      
        for (int i = 0; i < locationData.ObjectLocations.Count ; i++)
        {
            newObj = Instantiate(prefab, transform);
            string[] location = locationData.ObjectLocations[i].Location.Split(',');
            newObj.GetComponent<ListItem>().Init( locationData.ObjectLocations[i].ThumbnailURL,
                                                  locationData.ObjectLocations[i].Description,
                                                  locationData.ObjectLocations[i].ObjectID,
                                                  double.Parse(location[0]),
                                                  double.Parse(location[1]));
        }

        float width = canvas.GetComponent<RectTransform>().rect.width;


        Vector2 newSize = new Vector2(width,300);
        GetComponent<GridLayoutGroup>().cellSize = newSize;

    }



}
