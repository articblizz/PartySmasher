using UnityEngine;
using System.Collections;
using Xft;

public class SwordHit : MonoBehaviour {

    public bool isPunching = true;

	public XWeaponTrail Trail;

	float hitMultiplier;

	public Color[] SlashColors;

	public int currentSlash = 0;


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

		if (!isPunching)
			hitMultiplier = 1;

		Trail.MyColor = SlashColors [currentSlash];
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
                    col.GetComponent<Collider>().GetComponent<PlayerInputV2>().Hit(10 * hitMultiplier, Vector3.Normalize(col.transform.position - transform.position) * 2, 0.5f);
					hitMultiplier += 0.5f;
                }
            }
        }
    }
}
