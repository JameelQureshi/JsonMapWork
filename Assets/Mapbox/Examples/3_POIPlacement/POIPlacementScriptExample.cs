using UnityEngine;
using System.Collections.Generic;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;

//namespace Mapbox.Examples
//{


//	public class POIPlacementScriptExample : MonoBehaviour
//	{
//		public AbstractMap map;

//		//prefab to spawn
//		public GameObject prefab;
//		//cache of spawned gameobjects
//		private List<GameObject> _prefabInstances;

//		// Use this for initialization
//		void Start()
//		{
//			//add layers before initializing the map
//			map.VectorData.SpawnPrefabByCategory(prefab, LocationPrefabCategories.ArtsAndEntertainment, 10, HandlePrefabSpawned, true, "SpawnFromScriptLayer");
//			map.Initialize(new Vector2d(37.784179, -122.401583), 16);
//		}

//		//handle callbacks
//		void HandlePrefabSpawned(List<GameObject> instances)
//		{
//			if (instances.Count > 0)
//			{
//				Debug.Log(instances[0].name);
//			}
//		}

//	}
//}




public class POIPlacementsWSP : MonoBehaviour
{

    [SerializeField]
    [Geocode]
    string[] _TrackPartsLatitudeLongitude;
    public string[] title;
    public string[] body;
    public Sprite[] PannelBG;
    public Vector2d[] _locations;
    [SerializeField]
    AbstractMap _map;
    public int _spawnScale = 1;
    public List<GameObject> _spawnedObjects;
    Vector2d[] _coordinates;
    bool instatiatedmap = false;
    public GameObject CheckpointIndicator;


    private void Awake()
    {
        _map.OnInitialized += PLACEPOINTS;
    }

    private void PLACEPOINTS()
    {
        _locations = new Vector2d[_TrackPartsLatitudeLongitude.Length];
        _spawnedObjects = new List<GameObject>();

        int count = 0;
        foreach (string x in _TrackPartsLatitudeLongitude)
        {
            _locations[count] = Conversions.StringToLatLon(x);
            var instance = Instantiate(CheckpointIndicator);
            instance.transform.position = _map.GeoToWorldPosition(_locations[count], false);

            Debug.LogWarning(_map.GeoToWorldPosition(_locations[count], true));

            _spawnedObjects.Add(instance);
            count++;
        }
        instatiatedmap = true;

    }


    private void LateUpdate()
    {
        if (instatiatedmap)
        {
            int count = _spawnedObjects.Count;
            for (int i = 0; i < count; i++)
            {
                var spawnedObject = _spawnedObjects[i];
                var location = _locations[i];
                spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
                spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            }
        }
    }
}
