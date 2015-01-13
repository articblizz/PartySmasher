using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour {

    public GameObject[] players;

    float posZ;

	// Use this for initialization
	void Start () {

        posZ = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {

        var pos1 = players[0].transform.position;
        var pos2 = players[1].transform.position;

        float mX = (pos1.x + pos2.x) / 2;
        float mY = (pos1.y + pos2.y) / 2;

        var diff = Vector3.Distance(pos1, pos2);
        var camPos = transform.position;

        camPos = new Vector3(mX, mY + 2, posZ - (diff/3));


        transform.position = Vector3.MoveTowards(transform.position, camPos, Vector3.Distance(transform.position, camPos));
	}
}
