using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    //Movement data
    public float MaxSpeed = 2.75f;
    public float MinSpeed = 0.125f;
    public float Acceleration = 5.75f;
    public float Deceleration = 7.5f;

    //Mouse control data
    //Min distance to start counting as movement
    public float MinMouseDist = 0.1f;
    //When to consider the mouse to be so far, we need to reach max speed
    public float MaxMouseDist = 1.5f;
    public float DistanceAccel = 0;

    //Values for rotation
    public float FacingRightAngle = 91;
    public float FacingLeftAngle = 269;
    public float RotationTime = 0.25f;

    //Assignable variables
    private Camera mainCamera;
    [SerializeField]
    private Animator charAnimator;

    //Variables
    private float speed = 0f;
    private float targetSpeed = 0f;
    private Rigidbody2D rb;

    //Booleans probably used mainly for animation
    private bool falling = false;
    private bool walking = false;
    private bool turning = false;
    private bool braking = false;
    private bool facingRight = true;

    //For rotating the character
    private float characterRotation = 1f;

    //Moving platforms
    private const float groundCircleRadius = 0.15f;
    private Vector3 groundOffset = new Vector3(0f, 0f, 0f);
    public LayerMask movingPlatformMask;

    //Movement with a button
    private KeyCode key = KeyCode.Space;
    private float direction = 1f; //-1 or 1

    void Start()
    {
        mainCamera = Camera.main;
        DistanceAccel = MaxSpeed / (MaxMouseDist - MinMouseDist);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Figure out a correct target speed
        if (Input.GetKeyDown(key) || Input.GetKey(key))
        {
            targetSpeed = direction * MaxSpeed;
        }
        if (Input.GetKeyUp(key))
        {
            //Character turns around
            direction = -direction;
            targetSpeed = 0f;
        }

        this.charAnimator.SetFloat("Speed", Mathf.Abs(this.speed) / MaxSpeed);
        this.charAnimator.SetBool("Falling", this.falling);
    }

    void FixedUpdate()
    {
        this.speed = rb.velocity.x;
        this.falling = rb.velocity.y < -2.5f;

        if (this.targetSpeed != this.speed)
        {
            float accel = 0f;

            int signReal = (int)Mathf.Sign(this.speed);
            if (this.speed == 0) signReal *= 0;
            int signTarget = (int)Mathf.Sign(this.targetSpeed);
            if (this.targetSpeed == 0) signTarget *= 0;

            this.braking = !(signReal == 0 || signReal == signTarget);
            if (braking)
            {
                accel = Deceleration;
                if (this.falling) accel /= 4f;
            }
            else
            {
                accel = Acceleration;
            }
            float deltaV = accel * Time.fixedDeltaTime;
            if (Mathf.Abs(this.targetSpeed - speed) < deltaV)
            {
                speed = this.targetSpeed;
            }
            else
            {
                int accelDirection = (int)Mathf.Sign(this.targetSpeed - speed);
                speed += accelDirection * deltaV;
            }

            if (this.speed < 0) this.facingRight = false;
            if (this.speed > 0) this.facingRight = true;
        }
        else
        {
            this.braking = false;
        }

        this.walking = Mathf.Abs(this.speed) > 0.05f;
        this.turning = this.walking && Mathf.Sign(this.speed) != Mathf.Sign(this.targetSpeed) && Mathf.Sign(this.targetSpeed) != 0;

        rb.velocity = new Vector2(speed, rb.velocity.y);

        //Rotate character
        if (!((this.characterRotation == 1 && this.facingRight) || (this.characterRotation == 0 && !this.facingRight)))
        {
            this.charAnimator.transform.rotation = Quaternion.Euler(0, Mathf.LerpAngle(FacingLeftAngle, FacingRightAngle, characterRotation), 0);
            this.characterRotation += Time.fixedDeltaTime / RotationTime * (this.facingRight ? 1 : -1);
            this.characterRotation = Mathf.Clamp01(this.characterRotation);
        }

        CheckIsOnMovingPlatform();
    }

    //These two methods are for easy portability and input bug fixing
    //The x value difference between player and mouse
    float GetMousePosition()
    {
        return this.mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -this.mainCamera.transform.position.z)).x - this.transform.position.x;
    }

    bool GetMouseDown()
    {
        return Input.GetMouseButton(0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + groundOffset, groundCircleRadius);
    }

    private void CheckIsOnMovingPlatform()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position + groundOffset, groundCircleRadius, movingPlatformMask);
        if (col != null && transform.parent != col.transform)
        {
            transform.SetParent(col.transform);
        }
        if (col == null && transform.parent != null)
        {
            transform.parent = null;
        }
    }
}
