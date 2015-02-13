using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerInputV2 : MonoBehaviour {

    [Header("Player Input Attributes")]
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

        if (direction < 0 && isFacingRight)
            Flip();
        else if (direction > 0 && !isFacingRight)
            Flip();

        if(direction != 0)
            rigidbody.velocity = new Vector3(Speed * direction, rigidbody.velocity.y);

        if (rigidbody.velocity.x >= MaxVelocity)
            rigidbody.velocity = new Vector3(MaxVelocity, rigidbody.velocity.y);
        else if (rigidbody.velocity.x <= -MaxVelocity)
            rigidbody.velocity = new Vector3(-MaxVelocity, rigidbody.velocity.y);
    }


    protected void DaUpdate()
    {
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

        if (isOnGround && Input.GetKeyDown(KeyCode.W))
        {
            rigidbody.AddForce(new Vector3(0, JumpForce));
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
}
