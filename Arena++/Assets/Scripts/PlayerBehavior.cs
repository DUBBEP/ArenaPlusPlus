using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public GameBehavior gameManager;

    public float moveSpeed = 10f;
    public float rotateSpeed = 10f;
    public float jumpVelocity = 5f;
    public float airJumpVelocity = 6f;
    public float distanceToGround = 0.1f;
    public float standardFallForce = -10f;
    public float floatyFallForce = -5f;
    public float decendingForce = 1.5f;
    public float shortHopMultiplier = 2f;
    public float bufferTime = 0.6f;
    public float coyoteTime = 0.6f;
    public float knockBack = 2f;
    public LayerMask groundLayer;


    public Transform mainCamera;
    public GameObject bullet;
    public GameObject platform;
    public float bulletSpeed = 100f;


    private Vector3 bulletOffSet;
    private Transform position;
    public bool playerIsMobile = true;
    private bool isGroundJumping = false;
    private bool isAirJumping = false;
    private bool isShooting = false;
    private bool firePlatform = false;
    private bool flippingCharacter = false;
    private float turnWindow;
    private float vInput;
    private float hInput;
    private float lastJumpTime;
    private float lastGroundTime;
    public float fallSpeed;
    private Rigidbody rb;
    private CapsuleCollider col;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        position = GameObject.Find("Player").transform;   
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        lastJumpTime -= Time.deltaTime;
        lastGroundTime -= Time.deltaTime;
        turnWindow -= Time.deltaTime;

        if (playerIsMobile)
        {
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        }
        
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        if (Input.GetKeyUp(KeyCode.S) && turnWindow < 0)
        {
            turnWindow = 0.095f;
        }

        if (turnWindow >= 0)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                flippingCharacter = true;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            lastJumpTime = bufferTime;
        
            if (!IsGrounded() && gameManager.airJumpCount > 0 && lastGroundTime <= 0)
            {
                isAirJumping = true;
            }
        
        }
        
        if (IsGrounded())
        {
            lastGroundTime = coyoteTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            isShooting = true;
        }

        if (gameManager.platformTrigger && Input.GetMouseButtonDown(1) && !IsGrounded() )
        {
            firePlatform = true;
        }

    }

    void FixedUpdate() 
    {

        if (lastGroundTime >= 0 && lastJumpTime >= 0 && !isGroundJumping)
        {
            lastJumpTime = 0f;
            lastGroundTime = 0f;
            isGroundJumping = true;
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);            
            rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }

        
        if (isAirJumping)
        {
            lastJumpTime = 0f;
            isAirJumping = false;
            gameManager.airJumpCount -= 1;
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * airJumpVelocity, ForceMode.Impulse);
        }

        if (rb.velocity.y <= 0)
        {
            isGroundJumping = false;
        }

        #region JumpBehavior
        if (Input.GetButton("Jump") && rb.velocity.y < 0 && !IsGrounded() && lastJumpTime > -0.5f)
        {
            fallSpeed = floatyFallForce;
        } else if (rb.velocity.y <= 0 && !IsGrounded())
        {
            fallSpeed = decendingForce;

        } else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
                fallSpeed = decendingForce * shortHopMultiplier;
        } else
        {
            fallSpeed = standardFallForce;
        }
        #endregion


        if (isShooting)
        {
            bulletOffSet = position.TransformPoint(1f, 0f, 0f);
            Vector3 source = mainCamera.transform.eulerAngles;
            Vector3 target = bullet.transform.eulerAngles;
            target.y = source.y;
            bullet.transform.eulerAngles = target;
            GameObject newBullet = Instantiate(bullet, bulletOffSet, bullet.transform.rotation) as GameObject;
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
            bulletRB.velocity = this.transform.forward* bulletSpeed;
            isShooting = false;
        }


        if (firePlatform)
        {
            GameObject newPlatform = Instantiate(platform, this.transform.position + new Vector3(0, -2.8f, 0), this.transform.rotation) as GameObject;
            gameManager.platformsRemaining -= 1;
            if (gameManager.platformsRemaining <= 0)
            {
                gameManager.platformTrigger = false;
            }
            firePlatform = false;
        }

            #region movement
            rb.AddForce(Vector3.up * fallSpeed);
            Vector3 rotation = Vector3.up * hInput;
            Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
            rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * angleRot);

            if (flippingCharacter)
            {
                rb.MoveRotation(rb.rotation * Quaternion.Euler(0, 180, 0));
                flippingCharacter = false;
            }
            #endregion
    }

    public bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);
        return grounded;
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;

        if(collidedObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("PlayerHurt");
            gameManager.HP -= 1;
            rb.velocity = new Vector3(0f, 0f, 0f);
            float enemyAngle = collision.gameObject.transform.eulerAngles.y;
            Quaternion forceDirection = Quaternion.AngleAxis(enemyAngle, Vector3.up);
            Vector3 forceVector = forceDirection * Vector3.forward + forceDirection * Vector3.up;
            rb.AddForce(forceVector * knockBack, ForceMode.Impulse);
            //pauseControls();
            //For later. Function should render the player imobile for a brief moment when they are knocked backwards.
        }
    }

    private void pauseControls(float inactivePeriod)
    {
        //should disable controls for the "inactivePeriod" before returning control to the user.
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Void")
        {
            gameManager.HP = 0;
        }
    }
}
