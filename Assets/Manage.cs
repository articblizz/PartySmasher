using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manage : MonoBehaviour {


    public Slider slider;
    public Image[] images;
    public Color unselectCol = new Color(5, 5, 5);
    int latestValue = 1;
	// Use this for initialization
	void Start () {

        images[1].color = unselectCol;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GotoLevel()
    {
        if (latestValue == 0)
            Application.LoadLevel(2);
        else
            Application.LoadLevel(1);
    }

    public void ChangeLevel(float value)
    {
        latestValue = (int)value;
        print(latestValue);
        slider.value = value;
        if (value == 1)
        {
            images[1].color = unselectCol;
            images[0].color = Color.white;
        }
        else
        {
            images[1].color = Color.white;
            images[0].color = unselectCol;
        }
    }
}
