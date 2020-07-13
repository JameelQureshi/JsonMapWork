
using UnityEngine;
using UnityEngine.UI;

public class MapItem : MonoBehaviour {
    public string ObjectID;
    public Image thumbnail;

    // Use this for initialization
    void Start () {
		
	}

    public void Init(string objectID)
    {
        ObjectID = objectID;
        GetComponent<Button>().onClick.AddListener(delegate { TaskOnClick(objectID); });
    }

    private void TaskOnClick(string objectID)
    {
        Debug.Log(objectID);
    }
}
