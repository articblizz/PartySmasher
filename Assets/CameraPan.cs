using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour {

	GameObject[] players;

	float posZ;

	// Use this for initialization
	void Start () {

		posZ = transform.position.z;

		players = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

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

        float distance = Vector3.Distance(positions[idToLeft], positions[idToRight]);
        print(distance);
		transform.position = new Vector3(center.x, center.y + 2, (-distance/3) - 5);
		//var pos1 = new Vector3();
		//var pos2 = new Vector3();

		//if (GameObject.FindGameObjectsWithTag("Player").Length < 2)
		//{
		//    transform.position = GameObject.FindGameObjectsWithTag("Player")[0].transform.position + new Vector3(0, 0, posZ);
		//}
		//else
		//{
		//    pos1 = players[0].transform.position;
		//    pos2 = players[1].transform.position;

		//    float mX = (pos1.x + pos2.x) / 2;
		//    float mY = (pos1.y + pos2.y) / 2;

		//    var diff = Vector3.Distance(pos1, pos2);
		//    var camPos = transform.position;

		//    camPos = new Vector3(mX, mY + 2, posZ - (diff / 3));


		//    transform.position = Vector3.MoveTowards(transform.position, camPos, Vector3.Distance(transform.position, camPos));
		//}


	}
}
