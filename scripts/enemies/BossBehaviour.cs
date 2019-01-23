using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(enemyController2D))]
public class BossBehaviour : MonoBehaviour {

    public int health = 160;
    public Transform character;

    public float moveSpeed = 2;
    public float spottedSpeed = 4;
    public float timeToApex = 0.4f;
    public float jumpHeight = 3.1f;
    float accelerationTimeAirborne = 0.2f;
    float accelerationTimeGrounded = 0.1f;
    float strikeTimer = 0.5f;
    float currentStrikeTimer;

    bool hastarget;

    Vector3 velocity;
    float gravity;
    float jumpVelocity;
    float velocityXSmoothing;

    enemyController2D controller;
    //private Attack attackscript;
    private Transform self;
    public projectile ball;

    private float teleportcooldown = 0f;
    private bool right;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<enemyController2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToApex;
        Debug.Log(gravity + ", " + jumpVelocity);

        //attackscript = GameObject.Find("sword").GetComponent<Attack>();
        currentStrikeTimer = strikeTimer;
        self = GetComponentInParent<Transform>();
        hastarget = false;
        right = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(self.position, character.position) < 40.0f && character.position.x >= 69)
        {
            teleportcooldown += Time.deltaTime;
            if (teleportcooldown >= 4.0f)
            {
                teleportcooldown = 0;
                teleport(right);
                right = !right;
            }


            move();

            strike();
        }

    }

    private void move()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        if (controller.collisions.left || controller.collisions.right)
        {
            if (controller.collisions.below)
                moveSpeed *= -1;
            velocity.x = 0;
        }

        if (Vector2.Distance(self.position, character.position) < 40.0f && Mathf.Abs(self.position.x - character.position.x) > .5f)
        {
            hastarget = true;
            if (Mathf.Sign(moveSpeed) == Mathf.Sign(self.position.x - character.position.x))
                moveSpeed *= -1;
        }

        float targetVelocityX = 0;//(hastarget) ? moveSpeed * 2 : moveSpeed;


        Vector2 input = new Vector2(moveSpeed, velocity.y);

        if (input.x < 0)
        {
            self.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (input.x > 0)
        {
            self.transform.localScale = new Vector3(1, 1, 1);
        }
        //input.x = 0;
        //velocity = input;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void strike()
    {
        if (strikeTimer >= 1)
        {
            strikeTimer = 0;
            if (Vector2.Distance(self.position, character.position) < 40.0f)
            {
                Instantiate(ball, transform.position, transform.rotation);
            }
        }
        else
        {
            strikeTimer += Time.deltaTime;
        }
    }


    private void teleport(bool side)
    {
        if (side)
            transform.position = new Vector2(70, 8.3f);
        else
            transform.position = new Vector2(106, 8.3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("sword") || collision.CompareTag("projectile"))
        {
            health -= 20;
            Debug.Log(health);
            //moveSpeed *= 1.2f;
            //velocity = new Vector2(10 * (self.position.x - character.position.x), jumpHeight);
            //controller.Move(velocity * Time.deltaTime);
        }
        //if (collision.CompareTag("Player"))
        //{
        //    velocity = new Vector2(15 * Mathf.Sign(self.position.x - character.position.x), jumpHeight);
        //}

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
