using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{  // Config 
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climpSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    //State
    bool isAlive = true;

    //Cached component references 

    Rigidbody2D myRidgidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;
    float gravityScaleAtStart;
    // Message then methods

    void Start()
    {
        myRidgidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRidgidBody.gravityScale;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }

        Run();
        ClimpLadder();
        Jump();
        FlipSprite();
        Die();
      
    }
    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // value is between -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRidgidBody.velocity.y);
        myRidgidBody.velocity = playerVelocity;

        bool playHasHorizontalSpeed = Mathf.Abs(myRidgidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playHasHorizontalSpeed);

    }
    private void ClimpLadder()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Climping")))
        {
            myAnimator.SetBool("Climping", false);
            myRidgidBody.gravityScale = gravityScaleAtStart;
            return;

        }
        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climpVelocity = new Vector2(myRidgidBody.velocity.x, controlThrow * climpSpeed);
        myRidgidBody.velocity = climpVelocity;
        myRidgidBody.gravityScale = 0f;
        bool playerHasVerticalSpeed = Mathf.Abs(myRidgidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climping", playerHasVerticalSpeed);
    }
        private void Jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRidgidBody.velocity += jumpVelocityToAdd;
        }

    }
    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard ")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Die");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
    private void FlipSprite()
    {
        bool playHasHorizontalSpeed = Mathf.Abs(myRidgidBody.velocity.x) > Mathf.Epsilon;
        if (playHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRidgidBody.velocity.x), 1f);
        }
    }
    }
