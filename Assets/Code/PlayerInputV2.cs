using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using XInputDotNetPure;

public enum PlyInput
{
    Player1 = 0,
    Player2 = 1
}

public class PlayerInputV2 : MonoBehaviour {

    [Header("Player Input Attributes")]
    public float Speed = 5;
    public float KnockbackForce = 100;
    public float Health = 100;

    public PlyInput inputType;

	protected GamePadState gamepadState;
	protected GamePadState prevState;
	public PlayerIndex MyIndex;

	public bool isUsingController = true;

    public int lives = 3;

    public float JumpForce = 200;
    public LayerMask whatIsGround;

	Rigidbody rigidBody;

    bool isFacingRight = true;
    Text hpText;

    protected bool isOnGround;

    float direction = 0;

    bool immortal = false;
    float immortalTimer;
    public float ImmortalTime;


    Canvas uiCanvas;

    protected bool isStunned = false;

    public float MaxVelocity = 10;

    protected void DaStart()
    {
        uiCanvas = GetComponentInChildren<Canvas>();
		rigidBody = GetComponent<Rigidbody> ();
        hpText = GetComponentInChildren<Text>();
    }

    protected void DaFixed()
    {
        hpText.text = string.Format("{0:0}", Health);

        isOnGround = Physics.CheckCapsule(GetComponent<Collider>().bounds.center, new Vector3(GetComponent<Collider>().bounds.center.x, GetComponent<Collider>().bounds.min.y - 0.1f, GetComponent<Collider>().bounds.center.z), 0.18f, whatIsGround);
        //print(isOnGround);

        if (!isStunned)
        {
            if (direction < 0 && isFacingRight)
                Flip();
            else if (direction > 0 && !isFacingRight)
                Flip();
        }

        if(!isStunned)
            GetComponent<Rigidbody>().velocity = new Vector3(Speed * direction, GetComponent<Rigidbody>().velocity.y);

        if (GetComponent<Rigidbody>().velocity.x >= MaxVelocity)
            GetComponent<Rigidbody>().velocity = new Vector3(MaxVelocity, GetComponent<Rigidbody>().velocity.y);
        else if (GetComponent<Rigidbody>().velocity.x <= -MaxVelocity)
            GetComponent<Rigidbody>().velocity = new Vector3(-MaxVelocity, GetComponent<Rigidbody>().velocity.y);
    }

    protected void DaUpdate()
    {
        if (stunnedDuration > 0)
            stunnedDuration -= Time.deltaTime;
        else
            isStunned = false;

        if (immortal)
        {
            immortalTimer += Time.deltaTime;
            if (immortalTimer >= ImmortalTime)
            {
                immortal = false;
                immortalTimer = 0;
            }
        }

	


        switch (inputType)
        {
            case PlyInput.Player1:

				if(isUsingController)
				{
					prevState = gamepadState;
					gamepadState = GamePad.GetState (MyIndex);
				
					direction = gamepadState.ThumbSticks.Left.X;

					if(gamepadState.Buttons.A == ButtonState.Pressed && prevState.Buttons.A == ButtonState.Released && isOnGround)
					{
						rigidBody.AddForce(new Vector2(0, JumpForce));
					}

				}
				else
				{
	                if (Input.GetKey(KeyCode.A))
	                {
	                    direction = -1;
	                }
	                else if (Input.GetKey(KeyCode.D))
	                {
	                    direction = 1;
	                }
	                else
	                    direction = 0;

	                if (Input.GetKeyDown(KeyCode.W) && isOnGround)
	                {
	                    GetComponent<Rigidbody>().AddForce(new Vector3(0, JumpForce));
	                }
				}
                break;
            case PlyInput.Player2:
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    direction = -1;
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    direction = 1;
                }
                else
                    direction = 0;

                if (Input.GetKeyDown(KeyCode.UpArrow) && isOnGround)
                {
                    GetComponent<Rigidbody>().AddForce(new Vector3(0, JumpForce));
                }
                
                break;
        }

        if (GetComponent<Rigidbody>().velocity.y > 1)
            gameObject.layer = LayerMask.NameToLayer("PlayerNoPcol");
        else
            gameObject.layer = LayerMask.NameToLayer("Player");
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        transform.RotateAround(transform.position, transform.up, 180f);
        uiCanvas.transform.RotateAround(transform.position, transform.up, 180f);
    }


    float stunnedDuration = 0;
    public void Hit(float dmg, Vector3 dir, float knockback,float stunTime)
    {
		print (dir * knockback);
		GetComponent<Rigidbody>().AddForce(dir * knockback);
		stunnedDuration = stunTime;

        isStunned = true;

        Health -= dmg;
        if (Health <= 0)
        {
            lives--;
            if (lives < 0)
                Destroy(gameObject);
            else
            {
                Health = 100;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().MovePosition(new Vector3(0, 12, 0));
            }
        }
    }
}
