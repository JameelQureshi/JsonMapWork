using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ListItem : MonoBehaviour {

    public Image thumbnail;
    public Text description;

    public void Init(string thumbnailURL , string descriptionText , string objectID)
    {
        StartCoroutine(GetThumbnail(thumbnailURL));
        description.text = descriptionText;
        GetComponent<Button>().onClick.AddListener(delegate { TaskOnClick(objectID); });
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
