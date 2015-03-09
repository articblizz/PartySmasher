using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using XInputDotNetPure;


public enum PlyControls
{
    WASD,
    ARROWS
}

public class BobScript : PlayerInputV2 {

    [Header("Bobscript Attributes")]
    public PlyControls Controls;

    public ParticleSystem particles;

	int slashes = 0;
	

    //float damage = 10;
    Animator animator;
    public KeyCode SlashKey;

    public SwordHit sword;

    // Use this for initialization
    void Start () {

        animator = GetComponentInChildren<Animator>();
        DaStart();
        particles = GetComponentInChildren<ParticleSystem>();
    }

    public float DashSpeed = 5;

    float dashTimer;
    float dashTimerTwo;
    public float DashTiming = 0.5f;
    public float DashCooldown = 3;
    public float DashForce = 100;
    bool readyToDash = false;
    bool dashIsOffCooldown = true;
    public float SlashTime = 0.4f;

	public float DashingTime = 0.3f;

    void FixedUpdate()
    {
        base.DaFixed();

        if (isDashing)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x + (dashDirection * DashSpeed), GetComponent<Rigidbody>().velocity.y);

        }

        if (isOnGround && !particles.enableEmission)
            particles.enableEmission = true;
        else if (!isOnGround && particles.enableEmission)
            particles.enableEmission = false;
    }
    
    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
			animator.SetFloat("TimeSince", timer);
		
        base.DaUpdate();

		var slashtimer = SlashTime;
		if (slashes == 2)
			slashtimer = SlashTime + 0.2f;

        if (timer >= slashtimer) {
			slashes = 0;
			sword.isPunching = false;
		}


        //if (dashIsOffCooldown)
        //{
        //    dashTimer += Time.deltaTime;
       //     if (dashTimer >= DashTiming)
       //     {
      //          readyToDash = false;
      //          dashTimer = 0;
     //       }
     //   }
         if(!dashIsOffCooldown)
        {
            dashTimerTwo += Time.deltaTime;
            //if (dashTimerTwo >= 0.2f)
            //    rigidbody.drag = 1;

            //print(rigidbody.drag);

            if (dashTimerTwo >= DashingTime)
                isDashing = false;

            if (dashTimerTwo >= DashCooldown)
            {
                dashIsOffCooldown = true;
                dashTimerTwo = 0;
            }
        }


        if (Controls == PlyControls.WASD && !isUsingController)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
            }

			if (Input.GetKey(SlashKey))
			{
				Slash();
			}
        }
        else
        {
			if(gamepadState.Buttons.RightShoulder == ButtonState.Pressed && prevState.Buttons.RightShoulder == ButtonState.Released)
				Slash();
			if(gamepadState.Buttons.LeftShoulder == ButtonState.Pressed && prevState.Buttons.LeftShoulder == ButtonState.Released)
			{
				Dash();
			}
        }
    }


	int dashDirection;
    bool isDashing = false;

    float timer;
    public float SlashCooldown;

    void Dash()
    {
        if (!dashIsOffCooldown || isStunned)
            return;

		if (transform.rotation.y > 10)
			dashDirection = -1;
		else
			dashDirection = 1;
        isDashing = true;

        print("Dashes!");
        dashIsOffCooldown = false;
    }

    void Slash()
    {
        if (timer >= SlashCooldown) {
			timer = 0;
			animator.SetTrigger ("Slash");
			sword.isPunching = true;
			slashes = 0;
		} else if (slashes == 0) {
			animator.SetTrigger ("Slash2");
			timer = 0;
			slashes = 1;
		} else if (slashes == 1) {
			animator.SetTrigger("Slash3");
			timer = 0;
			slashes = 2;
		}
		sword.currentSlash = slashes;
		
    }
}
