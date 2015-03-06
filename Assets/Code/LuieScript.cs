using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LuieScript : PlayerInputV2 {

    [Header("Luie Script Attributes")]

    public KeyCode ThrowKey;
    public GameObject BombPref;

    public float ThrowForceX = 10;
    public float ThrowForceY = 5;
    public int MaxBombs = 2;

    public float ThrowCooldown;

    Animator animator;

    float throwTimer;

    void Start ()
    {
        DaStart();
        animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        DaFixed();
    }
    
    void Update ()
    {
        DaUpdate();

        throwTimer += Time.deltaTime;

        if (Input.GetKey(ThrowKey))
        {
            Throw();
        }
    }

    void Throw()
    {
        if (throwTimer >= ThrowCooldown && GameObject.FindGameObjectsWithTag("Bomb").Length < MaxBombs)
        {
            throwTimer = 0;
            animator.SetTrigger("Throw");
            GameObject bomb = (GameObject)Instantiate(BombPref, new Vector3(transform.rigidbody.position.x + .8f * transform.right.x, transform.rigidbody.position.y, transform.rigidbody.position.z), Quaternion.identity);
            float[] array = new float[3];
            array[0] = gameObject.transform.right.x;
            array[1] = ThrowForceX;
            array[2] = ThrowForceY;
            bomb.SendMessage("Throw", array);
        }
    }
}
