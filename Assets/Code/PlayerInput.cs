using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum PlyInput
{
    Player1 = 0,
    Player2 = 1
}


public class PlayerInput : MonoBehaviour {

    public PlyInput inputType;

    public float Speed = 5;
    public float KnockbackForce = 100;
    public float Health = 100;

    public float JumpForce = 200;
    public LayerMask whatIsGround;

    bool isFacingRight = true;
    Text hpText;

    bool isOnGround;

    float direction = 0;

    bool immortal = false;
    float immortalTimer;
    public float ImmortalTime;


    Canvas uiCanvas;

    public float MaxVelocity = 10;

	void Start () {
        uiCanvas = GetComponentInChildren<Canvas>();
        hpText = GetComponentInChildren<Text>();

	}

    void FixedUpdate()
    {
        hpText.text = Health.ToString();

        isOnGround = Physics.CheckCapsule(collider.bounds.center,new Vector3(collider.bounds.center.x,collider.bounds.min.y-0.1f,collider.bounds.center.z),0.18f, whatIsGround);

        if (direction < 0 && isFacingRight)
            Flip();
        else if (direction > 0 && !isFacingRight)
            Flip();

        //rigidbody.velocity = new Vector3(Speed * direction, rigidbody.velocity.y);
        rigidbody.AddForce(new Vector3(Speed * direction, 0));

        if(rigidbody.velocity.x >= MaxVelocity)
            rigidbody.velocity = new Vector3(MaxVelocity, rigidbody.velocity.y);
        else if(rigidbody.velocity.x <= -MaxVelocity)
            rigidbody.velocity = new Vector3(-MaxVelocity, rigidbody.velocity.y);


    }

    public float DoubleTapThingy = 0.5f;
    //int buttonCount = 0;

    void Flip()
    {
        isFacingRight = !isFacingRight;

        transform.RotateAround(transform.position, transform.up, 180f);
        uiCanvas.transform.RotateAround(transform.position, transform.up, 180f);
    }

    public void Hit(float dmg, Vector3 dir)
    {
        rigidbody.AddForce(dir * KnockbackForce);

        Health -= dmg;
        if (Health <= 0)
            Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {

        if (immortal)
        {
            immortalTimer += Time.deltaTime;
            if (immortalTimer >= ImmortalTime)
            {
                immortal = false;
                immortalTimer = 0;
            }
        }

        //var pos = transform.position;
        switch (inputType)
        {

            case PlyInput.Player1:

                if (Input.GetKey(KeyCode.D))
                {
                    direction = 1;

                }
                else if (Input.GetKey(KeyCode.A))
                {
                    direction = -1;
                }
                else
                    direction = 0;



                if (isOnGround && Input.GetKeyDown(KeyCode.W))
                {
                    rigidbody.AddForce(new Vector3(0, JumpForce));
                }



                break;

            case PlyInput.Player2:

                if (Input.GetKey(KeyCode.RightArrow))
                    direction = 1;
                else if (Input.GetKey(KeyCode.LeftArrow))
                    direction = -1;
                else
                    direction = 0;

                if (isOnGround && Input.GetKeyDown(KeyCode.UpArrow))
                {
                    rigidbody.AddForce(new Vector3(0, JumpForce));
                }

                //if (Input.GetKeyDown(KeyCode.RightControl))
                //    Slash();
                break;
        }



	
	}
}
