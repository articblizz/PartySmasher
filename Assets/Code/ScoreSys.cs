using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreSys : MonoBehaviour {

    GameObject[] players;
    public Text scoreboard;
    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {

        players = GameObject.FindGameObjectsWithTag("Player");

        string text = "";
        foreach (GameObject ply in players)
        {
            if(ply.GetComponent<PlayerInputV2>() != null)
            text += ply.GetComponent<PlayerInputV2>().lives + " - " + ply.name + "\n";
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if (Input.GetKeyDown(KeyCode.R))
            Application.LoadLevel("felix");

        scoreboard.text = text;
    }
}
