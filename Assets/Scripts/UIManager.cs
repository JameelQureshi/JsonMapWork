using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Canvas listView;
    public GameObject mapOnButton;
    public GameObject mapOffButton;
    public Slider radiusSlider;
    public Text radiusText;
    private bool isFirstTime = true;
    // Use this for initialization
    void Start () {
        mapOffButton.SetActive(false);
        radiusSlider.value = LocationDataManager.Radius;
        radiusText.text = LocationDataManager.Radius + " Miles";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TurnMapOn()
    {
        listView.enabled = false;
        mapOnButton.SetActive(false);
        mapOffButton.SetActive(true);
    } 
    public void TurnMapOff()
    {
        listView.enabled = true;
        mapOnButton.SetActive(true);
        mapOffButton.SetActive(false);
    }
    public void OnRadiusValueChanged()
    {
        if (isFirstTime)
        {
            isFirstTime = false;
            return;
        }
        StopAllCoroutines();
        LocationDataManager.Radius = (int)radiusSlider.value;
        radiusText.text = LocationDataManager.Radius + " Miles";
        StartCoroutine(Repopulate());
    }

    IEnumerator Repopulate()
    {
        yield return new WaitForSeconds(1);
        ListDataCreator.instance.RePopulate();
    }
}
