using System.Collections;
using TMPro;
using UnityEngine;

public class DribbleController : MonoBehaviour
{
    [Header("Player Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f;
    [SerializeField] SliderController sliderController;
    [Header("Dribble Settings")]
    public GameObject ball;
    public Transform ballFollowPoint;
    public float ballOffsetForward = 1f; // Offset to place ball in front of the player
    public float ballOffsetHeight = 0.5f; // Offset to raise ball slightly above ground
    public float dribbleAmplitude = 0.2f; // Amplitude of the ball movement
    public float dribbleSpeed = 5f; // Speed of the ball movement
    public float ballRotationSpeed = 300f; // Speed of ball rotation
    public float smoothDribbleFactor = 0.1f; // Smoothing factor for dribble movement

    private Vector3 movementInput;
    private float dribbleTimer;
    private bool hasBall = false;
    private Vector3 ballVelocity = Vector3.zero;
   [SerializeField] bool isShooting = false;
    bool allowAimShoot = false;
    float doubleSpaceBlocker = 0.15f;
    Animator animator;
    bool aiming = false;
    [SerializeField] float loseBallAimingTimer=7f;
    float aimingTimerReturn;
    ShotController shotController;

    bool sprinting = false;
    float sprintSpeed = 5f;
    float moveSpeedReturner;
    private void Awake()
    {
        shotController = GetComponent<ShotController>();
        moveSpeedReturner = moveSpeed;
    }
    void Start()
    {
        aimingTimerReturn = loseBallAimingTimer;
        moveSpeedWhileShooting = moveSpeed / 3;
         animator = GetComponent<Animator>();   
    }
    float moveSpeedWhileShooting;
    void Update()
    {
        if(!isShooting)
        {
            HandleMovementInput();
       
            moveSpeed = moveSpeedWhileShooting * 3;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                sprinting = true;
                moveSpeed = sprintSpeed;
            }
            else
            {
                moveSpeed = moveSpeedReturner;
                sprinting = false;
            }
        }
        else
        {
            moveSpeed = moveSpeedWhileShooting;
        }

    }
  
    void FixedUpdate()
    {
        if(isShooting)
        {
            doubleSpaceBlocker -=Time.deltaTime;
            if(doubleSpaceBlocker < 0 )
            {
                allowAimShoot=true;
            }
        }
        if(aiming)
        {
            loseBallAimingTimer -= Time.deltaTime;
            if(loseBallAimingTimer < 0 )
            {
                LoseBall();
            }
        }
        else
        {
            loseBallAimingTimer = aimingTimerReturn;
        }

        if(isShooting && hasBall )
        {
           
            HandleMovement();
            HandleDribble();
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isShooting && hasBall)
        {
            
            HandleShoot();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isShooting && hasBall&&allowAimShoot)
        {
            HandleAim();
            return;
        }
        if(isShooting)
            return;


        HandleMovement();
       
        if (hasBall)
        {
            HandleDribble();
        }
      

    }
   
    void HandleAim()
    {
        animator.SetBool("Shoot", true);
        CalculateShoot();
        sliderController.ResetSlider();
        aiming = false;
     }
    float distanceToCenter;
    void CalculateShoot()
    {
        float aimQuality;
        aimQuality = sliderController.GetValue();
        distanceToCenter = Mathf.Abs(aimQuality - 0.5f);
        shotController.ShowFeedBackText(distanceToCenter);
    }
    public void AnimationEnd()
    {
         Debug.Log("Ball" + ball);
        shotController.GetReadyForShoot(ball.transform);
        Debug.Log("shoot value " + distanceToCenter);
        EndShoot();
    }
    void LoseBall()
    {
        aiming = false;
        EndShoot();
        sliderController.ResetSlider();
        Vector3 a = ballVelocity;
        a = a * 3;
        Vector3 direct = ball.transform.position - transform.position;
        direct=direct * 25;
        direct.y = Mathf.Max(direct.y, 0.25f); // Prevent ball from going below ground level

        // ball.transform.position = Vector3.SmoothDamp(ball.transform.position, direct, ref(a), smoothDribbleFactor);
       ballrb=ball.GetComponent<Rigidbody>();
        ballrb.isKinematic = false;
        ballrb.AddForce(direct);
        Invoke("StopBall", 3f);
    }
    Rigidbody ballrb;

    void StopBall()
    {
        if(ball.GetComponent<DribblingBall>().hasOwner)
        {
            Debug.LogError("might be causing bug");
        }
        else
        { 
            ballrb.angularVelocity = Vector3.zero;
            ballrb.linearVelocity = Vector3.zero;
        }

    }
    void EndShoot()
    {
        animator.SetBool("Shoot", false);
        isShooting = false;
        hasBall = false;
        allowAimShoot= false;
        doubleSpaceBlocker = 0.3f;
    }
   
    
    void HandleShoot()
    {
         animator.SetBool("Running", false);
        sliderController.StartSlider();
        isShooting = true;
        aiming = true;
      //  Time.timeScale = Time.timeScale / 2;   
    }
    
    void HandleMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
       
        movementInput = new Vector3(horizontal, 0, vertical).normalized;
    }

    void HandleMovement()
    {
        if (movementInput.magnitude >= 0.1f)
        {
            // Calculate target rotation
            float targetAngle = Mathf.Atan2(movementInput.x, movementInput.z) * Mathf.Rad2Deg;
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, Time.deltaTime * rotationSpeed);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            // Save the current position before moving
            Vector3 originalPosition = transform.position;

            // Move the player
            transform.Translate(movementInput * moveSpeed * Time.deltaTime, Space.World);

            // Clamp the position within the x and z borders
            float clampedX = Mathf.Clamp(transform.position.x, -17f, 18f);
            float clampedZ = Mathf.Clamp(transform.position.z, -17,9);

            // Apply the clamped position if out of bounds
            transform.position = new Vector3(clampedX, transform.position.y, clampedZ);

            animator.SetBool("Running", true);
        }
        else
            animator.SetBool("Running", false);

    }

    void HandleDribble()
    {
        if (ball == null || ballFollowPoint == null)
            return;

        Vector3 targetPosition;
        if (movementInput.magnitude >= 0.1f)
        {
            // Increment the dribble timer
            dribbleTimer += Time.deltaTime * dribbleSpeed;

            // Calculate an offset to simulate ball moving slightly away and coming back
            float offset = Mathf.Sin(dribbleTimer) * dribbleAmplitude;

            // Set the ball's target position with the offset
            targetPosition = ballFollowPoint.position + transform.forward * (ballOffsetForward + offset) + Vector3.up * ballOffsetHeight;
        }
        else
        {
            // Keep the ball at the fixed follow point when not moving
            targetPosition = ballFollowPoint.position + transform.forward * ballOffsetForward + Vector3.up * ballOffsetHeight;
        }

        // Smoothly move the ball to the target position
        targetPosition.y = Mathf.Max(targetPosition.y, 0.25f); // Prevent ball from going below ground level
        ball.transform.position = Vector3.SmoothDamp(ball.transform.position, targetPosition, ref ballVelocity, smoothDribbleFactor);

        // Rotate the ball for realism
        ball.transform.Rotate(Vector3.right * ballRotationSpeed * Time.deltaTime);
    }
    void TakeBall()
    {
        
        Debug.Log("Ball Taken");
        hasBall = true;
        ball.GetComponent<DribblingBall>().BallReceived();
        // Optional: Disable physics on the ball when picked up
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        if (ballRigidbody != null)
        {
            ballRigidbody.isKinematic = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isShooting)
            return;

        if (other.gameObject.CompareTag("Ball"))
        {
            ball = other.gameObject;

            if(ball.GetComponent<DribblingBall>().hasOwner==false)
            TakeBall();
        }
    }
}
