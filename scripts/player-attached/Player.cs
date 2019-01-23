using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof (Controller2d))]
public class Player : MonoBehaviour {

    public float moveSpeed = 6;
    public float timeToApex = 0.4f;
    public float jumpHeight = 3.1f;
    public Transform sword;


    float accelerationTimeAirborne = 0.1f;
    float accelerationTimeGrounded = 0.05f;
    Vector3 velocity;
    float gravity;
    float jumpVelocity;
    float velocityXSmoothing;
    Vector2 input;


    float strikeTimer = 0.1f;
    float currentStrikeTimer;

    float timeSinceHit = 0.6f;


    Controller2d controller;
    private Attack attackscript;
    public playerStats stats;
    private Transform character;

    private bool roll;
    private float rollTime = 0.5f;
    private float currentRollTime;

    private bool lookRight;
    SwingDirection swingDirection; 
    private struct SwingDirection
    {
        public bool up, down, side;
        public void reset()
        {
            up = down = false;
            side = true;
        }
    }

    // Use this for initialization
    void Start () {
        controller = GetComponent<Controller2d>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToApex;
        //Debug.Log(gravity + ", " + jumpVelocity);

        sword = GameObject.Find("sword").GetComponent<Transform>();
        attackscript = GameObject.Find("sword").GetComponent<Attack>();


        currentStrikeTimer = strikeTimer;
        character = GetComponentInParent<Transform>();
        stats = GetComponent<playerStats>();

        currentRollTime = rollTime;
        roll = false;

        swingDirection.reset();
        lookRight = true;
	}
	
	// Update is called once per frame
	void Update () {


        move();

        strike();

        if(timeSinceHit != 0.6f)
            checkHit();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

	}

    private void move()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Debug.Log(Input.GetAxisRaw("Vertical"));
        if ((/*Input.GetAxisRaw("Vertical") > 0*/ Input.GetAxisRaw("Jump") > 0 || Input.GetKeyDown(KeyCode.Space)) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
        }


        float targetVelocityX = input.x * moveSpeed;

        if (!attackscript.getStrike())
        {
            if (input.x < 0)
            {
                character.transform.localScale = new Vector3(-1.7f, 1.7f, 1);
                lookRight = false;
            }
            else if (input.x > 0)
            {
                character.transform.localScale = new Vector3(1.7f, 1.7f, 1);
                lookRight = true;
            }
            else
            {

            }
        }


        //velocity.x = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void strike()
    {
        //Debug.Log(currentStrikeTimer + ", " + strikeTimer);


        if (input.y > 0.1)
        {
            swingDirection.reset();
            swingDirection.up = true;
        }
        else if(input.y < -0.1)
        {
            swingDirection.reset();
            swingDirection.down = true;
        }
        else
        {
            swingDirection.reset();
        }

        if(swingDirection.up)
        {
            sword.eulerAngles = lookRight?new Vector3(0,0,90.1f): new Vector3(0, 0, -90.1f);
            sword.localPosition = new Vector2(0, 1.5f);
        }
        else if(swingDirection.down)
        {
            sword.eulerAngles = lookRight ?new Vector3(0, 0, -90.1f): new Vector3(0, 0, 90.1f);
            sword.localPosition = new Vector2(0, -1.5f);
        }
        else
        {
            sword.eulerAngles = new Vector3(0, 0, 0);
            sword.localPosition = new Vector2(1.5f, 0);
        }


        if (Input.GetAxisRaw("Xbutton") > 0 && !attackscript.getStrike())
        {
            attackscript.setstrike(true);
        }
        else if(attackscript.getStrike())
        {
            currentStrikeTimer -= Time.deltaTime;
        }
        if (currentStrikeTimer <= 0.0f && Input.GetAxisRaw("Xbutton") == 0)
        {
            currentStrikeTimer = strikeTimer;
            attackscript.setstrike(false);
        }
    }

    private void checkHit()
    {
        timeSinceHit -= Time.deltaTime;
        if (timeSinceHit <= 0)
            timeSinceHit = 0.6f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.CompareTag("enemy") || collision.CompareTag("enemyprojectile")) && timeSinceHit == 0.6f)
        {
            stats.setCurrentHP(stats.getCurrentHP()+1);
            Debug.Log(stats.currentHP);
            velocity = new Vector2(20 * Mathf.Sign(character.position.x - collision.transform.position.x), jumpHeight*2);
            controller.Move(velocity * Time.deltaTime);
            timeSinceHit -= 0.01f;
        }

    }
}
