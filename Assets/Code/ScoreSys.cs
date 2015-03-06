using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreSys : MonoBehaviour
{
    bool GameWon = false;
    public GameObject winningPlayer, winPanel;

    GameObject[] players;
    public Text scoreboard;
    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        players = GameObject.FindGameObjectsWithTag("Player");

        if (!GameWon)
        {
            string text = "";
            foreach (GameObject ply in players)
            {
                text += ply.name + " - " + ply.GetComponent<PlayerInputV2>().lives + "\n";
                winningPlayer = ply;
            }

            scoreboard.text = "Player Lives" + "\n" + text;
        }

        if (players.Length == 1)
            GameWon = true;

        if (GameWon)
        {
            GameObject winCam = GameObject.FindGameObjectWithTag("WinCam");
            GameObject.FindGameObjectWithTag("MainCamera").camera.enabled = false;

            winCam.camera.enabled = true;

            winCam.camera.fieldOfView = Mathf.Min(100 , winCam.camera.fieldOfView + 5 * Time.deltaTime);

            if (winCam.camera.fieldOfView >= 98)
            {
                winPanel.SetActive(true);
                winPanel.GetComponentInChildren<Text>().text = winningPlayer.name + " is the winner!" + "\n" + "With " + (int)winningPlayer.GetComponent<PlayerInputV2>().Health + " health and " + winningPlayer.GetComponent<PlayerInputV2>().lives + " lives left!";
            }
        }
    }
}
