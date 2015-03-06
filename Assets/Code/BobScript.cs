using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public enum PlyControls
{
    WASD,
    ARROWS
}

public class BobScript : PlayerInputV2 {

    [Header("Bobscript Attributes")]
    public PlyControls Controls;

    //float damage = 10;
    Animator animator;
    public KeyCode SlashKey;

    public SwordHit sword;

    // Use this for initialization
    void Start () {

        animator = GetComponentInChildren<Animator>();
        DaStart();
    }

    public float DashSpeed = 5;

    float dashTimer;
    float dashTimerTwo;
    public float DashTiming = 0.5f;
    public float DashCooldown = 3;
    public float DashForce = 100;
    bool readyToDash = false;
    bool isOffCooldown = true;
    public float SlashTime = 0.4f;

    void FixedUpdate()
    {
        base.DaFixed();

        if (isDashing)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x + (lastDirection * DashSpeed), GetComponent<Rigidbody>().velocity.y);

        }
    }
    
    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;

        base.DaUpdate();

        if (timer >= SlashTime)
            sword.isPunching = false;

        if (Input.GetKey(SlashKey))
        {
            Slash();
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
            //if (dashTimerTwo >= 0.2f)
            //    rigidbody.drag = 1;

            //print(rigidbody.drag);

            if (dashTimerTwo >= 0.4f)
                isDashing = false;

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
                HandleDash(1);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                HandleDash(-1);
            }
        }
        else
        {

        }
    }

    bool isDashing = false;

    int lastDirection = 0;
    void HandleDash(int d)
    {
        if (!readyToDash)
        {
            lastDirection = d;
            //print("Ready!");
            readyToDash = true;
        }
        else if (isOffCooldown && d == lastDirection && !isStunned)
        {
            Dash(d);
        }
    }

    float timer;
    public float SlashCooldown;

    void Dash(int direction)
    {
        if (!isOffCooldown)
            return;
        isDashing = true;
        //rigidbody.drag = 0f;
        //rigidbody.AddForce(new Vector3(DashForce * direction, 0));

        print("Dashes!");
        isOffCooldown = false;
        readyToDash = false;
    }

    void Slash()
    {
        if (timer >= SlashCooldown)
        {
            timer = 0;
            animator.SetTrigger("Slash");
            sword.isPunching = true;
            
            //RaycastHit hit;
            //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, hitDistance))
            //{
            //    if (hit.collider.tag == "Player")
            //    {
            //    }
            //}
        }
    }
}
