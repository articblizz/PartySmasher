using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour
{

    GameObject[] players;

    // Use this for initialization
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        Vector3[] positions = new Vector3[players.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = players[i].transform.position;
        }

        Vector3 center = Vector3.zero;
        int idToLeft = 0, idToRight = 0;
        float leftX = 0, rightX = 0;

        for (int i = 0; i < positions.Length; i++)
        {
            if (i == 0)
            {
                leftX = positions[i].x;
                rightX = positions[i].x;
            }
            else if (positions[i].x < leftX)
            {
                leftX = positions[i].x;
                idToLeft = i;
            }
            else if (positions[i].x > rightX)
            {
                rightX = positions[i].x;
                idToRight = i;
            }
            center += positions[i];
        }
        center /= positions.Length;

        float camDis = Vector3.Distance(transform.position, center);
        float distance = Vector3.Distance(new Vector3(positions[idToLeft].x, 0), new Vector3(positions[idToRight].x, 0));
        center.z = (-distance / 3) - 5;
        center.y += 2;

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(center.x, center.y, center.z), camDis/40);
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(center.x, center.y, center.z), distance/15);
    }
}