using UnityEngine;
using System.Collections;
using Xft;

public class SwordHit : MonoBehaviour {

    public bool isPunching = true;

	public XWeaponTrail Trail;


	bool isOn = false;
	// Use this for initialization
	void Start () {
	
		Trail.Deactivate ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (isPunching && !isOn) {
			Trail.Activate ();
			isOn = true;
		}
		else if (!isPunching && isOn) {
			Trail.Deactivate();
			isOn = false;
		}
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
