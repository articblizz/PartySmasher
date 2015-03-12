using UnityEngine;
using System.Collections;

public enum forceMode
{
    Acceleration,
    Force,
    Impulse,
    VelocityChange
}

public class BombScript : MonoBehaviour
{
    public float BlowUpTimer = 5;
    
    public float ExplosionForce = 10;
    public float ExplosionRadius = 10;
    public float UpwardForce = 1;
    public forceMode Force_Mode = forceMode.Impulse;

    public GameObject explosionParticles;

    MeshRenderer renderer;

    bool haveExploded;

    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        renderer.material.color = Color.gray;
    }

	void Throw (float[] array)
    {
        gameObject.GetComponent<Rigidbody>().AddForce(array[1] * array[0], array[2], 0, ForceMode.Impulse);
	}

	void Update ()
    {
        BlowUpTimer -= Time.deltaTime;

        if (BlowUpTimer <= 0 && !haveExploded)
            Explode();
	}

    void Explode()
    {
        haveExploded = true;
        renderer.enabled = false;


        Collider[] colliders = Physics.OverlapSphere(gameObject.GetComponent<Rigidbody>().position, ExplosionRadius);
        foreach (Collider col in colliders)
        {
            if (col.GetComponent<Rigidbody>() == null) continue;

            if (col.tag == "Player")
            {
                col.gameObject.GetComponent<PlayerInputV2>().Hit(25, Vector3.zero,0, 2);
            }

            col.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, gameObject.GetComponent<Rigidbody>().position, ExplosionRadius, UpwardForce, (ForceMode)Force_Mode);

            explosionParticles.SetActive(true);
            GetComponent<TrailRenderer>().enabled = false;
            Destroy(gameObject, 1);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.GetComponent<Rigidbody>().position, ExplosionRadius);
    }
}
