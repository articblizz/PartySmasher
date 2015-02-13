using UnityEngine;
using System.Collections;
using UnityEngine.UI;




public class PlayerInput : MonoBehaviour {

    public PlyInput inputType;

    public float Speed = 5;
    public float KnockbackForce = 100;
    public float Health = 100;

    public int lives = 3;

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
        hpText.text = string.Format("{0:0}", Health);

        isOnGround = Physics.CheckCapsule(collider.bounds.center,new Vector3(collider.bounds.center.x,collider.bounds.min.y-0.1f,collider.bounds.center.z),0.18f, whatIsGround);

        if (direction < 0 && isFacingRight)
            Flip();
        else if (direction > 0 && !isFacingRight)
            Flip();

        rigidbody.AddForce(new Vector3(Speed * direction, 0));

        if(rigidbody.velocity.x >= MaxVelocity)
            rigidbody.velocity = new Vector3(MaxVelocity, rigidbody.velocity.y);
        else if(rigidbody.velocity.x <= -MaxVelocity)
            rigidbody.velocity = new Vector3(-MaxVelocity, rigidbody.velocity.y);
    }

    public float DoubleTapThingy = 0.5f;

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
        {
            lives--;
            if (lives < 0)
                Destroy(gameObject);
            else
            {
                Health = 100;
                rigidbody.MovePosition(new Vector3(0, 12, 0));
            }
        }
    }

    public void BombHit(float dmg)
    {
        Health -= dmg;
        if (Health <= 0)
        {
            lives--;
            if (lives < 0)
                Destroy(gameObject);
            else
            {
                Health = 100;
                rigidbody.MovePosition(new Vector3(0, 12, 0));
            }
        }
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

                break;
        }



    
    }
}
