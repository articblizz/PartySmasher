using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    protected bool isStunned = false;

    public float MaxVelocity = 10;

    protected void DaStart()
    {
        uiCanvas = GetComponentInChildren<Canvas>();
        hpText = GetComponentInChildren<Text>();
    }

    protected void DaFixed()
    {
        hpText.text = string.Format("{0:0}", Health);

        isOnGround = Physics.CheckCapsule(collider.bounds.center, new Vector3(collider.bounds.center.x, collider.bounds.min.y - 0.1f, collider.bounds.center.z), 0.18f, whatIsGround);
        //print(isOnGround);

        if (!isStunned)
        {
            if (direction < 0 && isFacingRight)
                Flip();
            else if (direction > 0 && !isFacingRight)
                Flip();
        }

        if(!isStunned)
            rigidbody.velocity = new Vector3(Speed * direction, rigidbody.velocity.y);

        if (rigidbody.velocity.x >= MaxVelocity)
            rigidbody.velocity = new Vector3(MaxVelocity, rigidbody.velocity.y);
        else if (rigidbody.velocity.x <= -MaxVelocity)
            rigidbody.velocity = new Vector3(-MaxVelocity, rigidbody.velocity.y);
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

        direction = Input.GetAxis("Horizontal");

        switch (inputType)
        {
            case PlyInput.Player1:
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
                    rigidbody.AddForce(new Vector3(0, JumpForce));
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
                    rigidbody.AddForce(new Vector3(0, JumpForce));
                }
                
                break;
        }

        if (rigidbody.velocity.y > 1)
            gameObject.layer = LayerMask.NameToLayer("PlayerNoPcol");
        else
            gameObject.layer = LayerMask.NameToLayer("Player");
        //print(gameObject.layer.ToString());



    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        transform.RotateAround(transform.position, transform.up, 180f);
        uiCanvas.transform.RotateAround(transform.position, transform.up, 180f);
    }


    float stunnedDuration = 0;
    public void Hit(float dmg, Vector3 dir, float stunTime)
    {
        rigidbody.AddForce(dir * KnockbackForce);
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
                rigidbody.velocity = Vector3.zero;
                rigidbody.MovePosition(new Vector3(0, 12, 0));
            }
        }
    }
}
