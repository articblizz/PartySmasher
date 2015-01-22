using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LuieScript : MonoBehaviour {

    public PlyControls Controls;

    float damage = 10;
    Animator animator;
    public KeyCode ThrowKey;

    // Use this for initialization
    void Start ()
    {

        animator = GetComponentInChildren<Animator>();

    }



    void FixedUpdate()
    {

    }

    float dashTimer;
    float dashTimerTwo;
    public float DashTiming = 0.5f;
    public float DashCooldown = 3;
    public float DashForce = 100;
    bool readyToDash = false;
    bool isOffCooldown = true;
    
    // Update is called once per frame
    void Update ()
    {
        Debug.DrawRay(transform.position, transform.right * hitDistance, Color.red);

        timer += Time.deltaTime;

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
                rigidbody.drag = 1;

            print(rigidbody.drag);
            if (dashTimerTwo >= DashCooldown)
            {
                isOffCooldown = true;
                dashTimerTwo = 0;
            }
        }


        if (Controls == PlyControls.ARROWS)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                HandleDash(1);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                HandleDash(-1);
            }
        }



    }

    int lastDirection = 0;
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

    public float hitDistance = 3;
    float timer;
    public float ThrowCooldown;


    void Dash(int direction)
    {
        if (!isOffCooldown)
            return;
        rigidbody.drag = 0f;
        rigidbody.AddForce(new Vector3(DashForce * direction, 0));

        print("Dashes!");
        isOffCooldown = false;
        readyToDash = false;
    }

    void Throw()
    {
        if (timer >= ThrowCooldown)
        {
            timer = 0;
            animator.SetTrigger("Throw");
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, hitDistance))
            {
                if (hit.collider.tag == "Player")
                {
                    hit.collider.GetComponent<PlayerInput>().Hit(damage, Vector3.Normalize(hit.transform.position - transform.position));
                }
            }
        }
    }
}
