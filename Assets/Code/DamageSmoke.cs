using UnityEngine;
using System.Collections;

public class DamageSmoke : MonoBehaviour {

	GameObject[] characters;
	float[] keepTrackOfDmg;

    public float DmgPerTick = 0.4f;

	// Use this for initialization
	void Start () {

		characters = GameObject.FindGameObjectsWithTag("Player");
		keepTrackOfDmg = new float[characters.Length];

	}
	
	// Update is called once per frame
	void Update () {
        characters = GameObject.FindGameObjectsWithTag("Player");
	
	}

	void OnTriggerStay(Collider col)
	{
		for(int i = 0; i < characters.Length; i++)
		{
			if (col == characters[i].collider)
			{
				if (keepTrackOfDmg[i] == 0)
					keepTrackOfDmg[i] = DmgPerTick;
				col.gameObject.GetComponent<PlayerInputV2>().Hit(keepTrackOfDmg[i], Vector3.zero);
			}
		}
	}
}
