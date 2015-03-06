using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LuieScript : PlayerInputV2 {

    [Header("Luie Script Attributes")]
    public PlyControls Controls;
    public KeyCode ThrowKey;
    public GameObject BombPref;

    public float ThrowForceX = 10;
    public float ThrowForceY = 5;
    public int MaxBombs = 2;

    public float DashTiming = 0.5f;
    public float DashCooldown = 3;
    public float DashForce = 100;
    public float hitDistance = 3;
    public float ThrowCooldown;

    Animator animator;

    int dir = 1;
    int lastDirection = 0;

    //float damage = 10;
    float dashTimer;
    float dashTimerTwo;
    float throwTimer;

    bool readyToDash = false;
    bool isOffCooldown = true;

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

        Debug.DrawRay(transform.position, transform.right * hitDistance, Color.red);

        throwTimer += Time.deltaTime;

        if (Input.GetKey(ThrowKey))
        {
            Throw();
        }

        if (readyToDash && isOffCooldown)
        {
            dashTimer += Time.deltaTime;
            if (dashTimer >= DashTiming)
            {
                readyToDash = false;
                dashTimer = 0;
            }
        }
        else if(!isOffCooldown)
        {
            dashTimerTwo += Time.deltaTime;
            if (dashTimerTwo >= 0.2f)
                GetComponent<Rigidbody>().drag = 1;

            print(GetComponent<Rigidbody>().drag);
            if (dashTimerTwo >= DashCooldown)
            {
                isOffCooldown = true;
                dashTimerTwo = 0;
            }
        }

        if (Controls == PlyControls.WASD)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                dir = 1;
                HandleDash(dir);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                dir = -1;
                HandleDash(dir);
            }
        }
    }

    void HandleDash(int d)
    {
        if (!readyToDash)
        {
            lastDirection = d;
            //print("Ready!");
            readyToDash = true;
        }
        else if (isOffCooldown && d == lastDirection)
        {
            Dash(d);
        }
    }

    void Dash(int direction)
    {
        if (!isOffCooldown)
            return;
        GetComponent<Rigidbody>().drag = 0f;
        GetComponent<Rigidbody>().AddForce(new Vector3(DashForce * direction, 0));

        print("Dashes!");
        isOffCooldown = false;
        readyToDash = false;
    }

    void Throw()
    {
        if (throwTimer >= ThrowCooldown && GameObject.FindGameObjectsWithTag("Bomb").Length < MaxBombs)
        {
            throwTimer = 0;
            animator.SetTrigger("Throw");
            GameObject bomb = (GameObject)Instantiate(BombPref, new Vector3(transform.GetComponent<Rigidbody>().position.x + .8f * transform.right.x, transform.GetComponent<Rigidbody>().position.y, transform.GetComponent<Rigidbody>().position.z), Quaternion.identity);
            float[] array = new float[3];
            array[0] = gameObject.transform.right.x;
            array[1] = ThrowForceX;
            array[2] = ThrowForceY;
            bomb.SendMessage("Throw", array);
        }
    }
}
