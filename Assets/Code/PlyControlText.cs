using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlyControlText : MonoBehaviour
{
    GameObject[] players;
    public Text ControlBoard;
    float timer = 0;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        string text = "";
        foreach (GameObject ply in players)
        {
            var input = ply.GetComponent<PlayerInputV2>().inputType;
            string controls = "";

            if (input == PlyInput.Player1)
                controls = "Move: WASD  Hit: SPACE";

            if (input == PlyInput.Player2)
                controls = "Move: ARROWS  Bomb: RSHIFT";

            text += ply.name + " - " + controls.ToString() + "\n";
        }

        ControlBoard.text = "Controls:" + "\n" + text;
    }

    void Update()
    {
        if (timer >= 7)
            FadeOut();
        else
            timer += Time.deltaTime;
    }

    void FadeOut()
    {
        gameObject.GetComponent<Image>().CrossFadeAlpha(0.0f, 2, false);
        ControlBoard.CrossFadeAlpha(0.0f, 1, false);

        Debug.Log(ControlBoard.canvasRenderer.GetAlpha());

        if (ControlBoard.canvasRenderer.GetAlpha() <= 0.004)
        {
            GameObject.DestroyObject(gameObject);
        }
    }
}
