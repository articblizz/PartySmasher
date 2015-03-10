using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using XInputDotNetPure;
using Xft;


public enum PlyControls
{
    WASD,
    ARROWS
}

public class BobScript : PlayerInputV2 {

    [Header("Bobscript Attributes")]
    public PlyControls Controls;

    public XWeaponTrail moveTrail;

    public ParticleSystem hoverParticles;

	int slashes = 0;

	public ParticleSystem jumpParticles;

    //float damage = 10;
    Animator animator;
    public KeyCode SlashKey;

    public SwordHit sword;

    // Use this for initialization
    void Start () {

        animator = GetComponentInChildren<Animator>();
        DaStart();
        moveTrail.Deactivate();
    }

    public float DashSpeed = 5;

    float dashTimer;
    float dashTimerTwo;
    public float DashTiming = 0.5f;
    public float DashCooldown = 3;
    public float DashForce = 100;
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

        if (isOnGround && !hoverParticles.enableEmission)
            hoverParticles.enableEmission = true;
        else if (!isOnGround && hoverParticles.enableEmission)
            hoverParticles.enableEmission = false;
    }

	public override void OnJump ()
	{

		jumpParticles.Play ();
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

         if(!dashIsOffCooldown)
         {
            dashTimerTwo += Time.deltaTime;

            if (dashTimerTwo >= DashingTime)
            {
                moveTrail.Deactivate();
                isDashing = false;
            }

            if (dashTimerTwo >= DashCooldown)
            {
                dashIsOffCooldown = true;
                dashTimerTwo = 0;
            }
        }


        if (Controls == PlyControls.WASD && !isUsingController)
        {
			if (Input.GetKeyDown(SlashKey))
			{
				Slash();
			}
			if(Input.GetKeyDown(KeyCode.LeftControl))
				Dash ();
        }
        else
        {
            if (gamepadState.Buttons.RightShoulder == ButtonState.Pressed && prevState.Buttons.RightShoulder == ButtonState.Released)
            {
                float d = gamepadState.ThumbSticks.Left.Y;
                if (d > 0.5f)
                    UpwardSlash();
                else
                   Slash();
            }

			if(gamepadState.Buttons.LeftShoulder == ButtonState.Pressed && prevState.Buttons.LeftShoulder == ButtonState.Released)
				Dash();
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

		if (transform.rotation.eulerAngles.y > 10f)
			dashDirection = -1;
		else
			dashDirection = 1;


        isDashing = true;
        Hit(0, Vector3.zero, 0, DashingTime);
        moveTrail.Activate();

        dashIsOffCooldown = false;
    }

    void Slash()
    {
        if (isStunned)
            return;

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

    void UpwardSlash()
    {
        if (isStunned)
            return;
        slashes = 0;
        if (timer >= SlashCooldown)
        {
            timer = 0;
            animator.SetTrigger("UpSlash");
            sword.isPunching = true;
        }
        sword.currentSlash = slashes;
    }
}
