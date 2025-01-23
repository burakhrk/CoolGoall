using System.Collections;
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
    float doubleSpaceBlocker = 0.3f;
    Animator animator;
    void Start()
    {
         animator = GetComponent<Animator>();   
    }

    void Update()
    {
        HandleMovementInput();
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
        float aimQuality;
        aimQuality = sliderController.GetValue();
        sliderController.ShootValueFeedBack();
        Debug.Log( "shoot value "+aimQuality);
        sliderController.ResetSlider(); 
     }
    public void AnimationEnd()
    {
        Debug.Log("it works animation end ");
        EndShoot();
    }
    void EndShoot()
    {

        animator.applyRootMotion = true;
        /*
        // Animasyonun son karesindeki pozisyonu hesapla
        animator.Play("Shoot", 0, 1f); // Animasyonu son kareye getir
        animator.Update(0f); // Animasyonu güncelle

        Vector3 endPosition = transform.position;

        animator.Play("Idle"); //animasyonu idle'a geri döndür
        animator.Update(0f); //animasyonu güncelle
        
        // Karakteri son karedeki pozisyona yerleştir
        transform.position = endPosition;
        */
        animator.SetBool("Shoot", false);
        isShooting = false;
        hasBall = false;
        allowAimShoot= false;
        doubleSpaceBlocker = 0.3f;
       
    }
    /*
    IEnumerator ShootEndNumerator()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("Shoot", false);
        isShooting = false;
        hasBall = false;
        GetComponent<Rigidbody>().isKinematic = false;

    }
    */
    void HandleShoot()
    {
         animator.SetBool("Running", false);
        sliderController.StartSlider();
        isShooting = true;
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

            // Move the player
            transform.Translate(movementInput * moveSpeed * Time.deltaTime, Space.World);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ball = other.gameObject;
            hasBall = true;

            // Optional: Disable physics on the ball when picked up
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                ballRigidbody.isKinematic = true;
            }
        }
    }
}
