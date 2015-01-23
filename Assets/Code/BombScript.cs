using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour
{
    public float BlowTimer = 5;
    public float ForceX = 100;
    public float ForceY = 50;

	void Start ()
    {
        gameObject.rigidbody.AddForce(ForceX, ForceY, 0, ForceMode.Impulse);
	}

	void Update ()
    {
        BlowTimer -= Time.deltaTime;

        if (BlowTimer <= 0)
            Explode();
	}

    void OnCollision(Collider col)
    {
        if(col.collider.tag == "Player")
        {
        }
    }

    void Explode()
    {
        DestroyObject(gameObject);
    }
}
