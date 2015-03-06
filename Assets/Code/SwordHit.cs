using UnityEngine;
using System.Collections;

public class SwordHit : MonoBehaviour {

    public bool isPunching = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (isPunching)
        {
            if (col.tag == "Player" && isPunching)
            {
                var script = col.GetComponent<Collider>().GetComponent<PlayerInputV2>();
                if (script != null)
                {
                    print(col.name);
                    col.GetComponent<Collider>().GetComponent<PlayerInputV2>().Hit(10, Vector3.Normalize(col.transform.position - transform.position) * 2, 0.5f);
                }
            }
        }
    }
}
