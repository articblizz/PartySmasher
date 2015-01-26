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
            text += ply.GetComponent<PlayerInput>().lives + " - " + ply.name + "\n";
        }

        scoreboard.text = text;
    }
}
