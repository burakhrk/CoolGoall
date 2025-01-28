using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyDribble : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 360f;
    public Transform ball;
    public Transform goal;
    public float stealRange = 2f; // Distance within which enemy can steal the ball
    public float shotPower = 10f; // Shot strength
    public float shotRange = 10f; // Range within which enemy can shoot
    public float timeBeforeShoot = 2f; // Time enemy waits after stealing the ball before shooting

    private bool hasBall = false;
    private bool gameStarted = false;
    private Animator animator;
    private Rigidbody ballRb;
    private float shootCooldown;
    private Coroutine stealCoroutine;
    [SerializeField] DribbleGameController gameController;

    void Start()
    {
        animator = GetComponent<Animator>();
        goal = FindFirstObjectByType<Kale>().transform;
    }

    private void OnEnable()
    {
        gameController = FindFirstObjectByType<DribbleGameController>();
        gameController.OnGameStart += OnGameStart;
    }

    private void OnDisable()
    {
        gameController.OnGameStart -= OnGameStart;
    }

    public void OnGameStart()
    {
        gameStarted = true;
    }

    void Update()
    {
        if (!gameStarted) return;

        if (!hasBall)
        {
            MoveTowardsBall();
        }
        else
        {
            if (Time.time >= shootCooldown)
            {
                MoveTowardsGoal();
            }
        }
    }

    void MoveTowardsBall()
    {
        if (ball == null)
        {
            ball = FindFirstObjectByType<DribblingBall>().transform;
            return;
        }

        Vector3 directionToBall = (ball.position - transform.position).normalized;
        float distanceToBall = Vector3.Distance(transform.position, ball.position);

        if (distanceToBall <= stealRange)
        {
            StealBall();
            return;
        }

        Move(directionToBall);
    }

    void MoveTowardsGoal()
    {
        if (goal == null) return;

        Vector3 directionToGoal = (goal.position - transform.position).normalized;
        float distanceToGoal = Vector3.Distance(transform.position, goal.position);

        if (distanceToGoal <= shotRange && IsFacingGoal())
        {
            ShootAtGoal();
            return;
        }

        Move(directionToGoal);
    }

    void Move(Vector3 direction)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        transform.position += direction * moveSpeed * Time.deltaTime;

        if (animator != null)
        {
            animator.SetBool("Running", true);
        }
    }

    void StealBall()
    {
        if (ball == null) return;

        hasBall = true;
        ball.SetParent(transform);
        ball.localPosition = new Vector3(0, 1f, 1f); // Position ball in front of the enemy

        ballRb = ball.GetComponent<Rigidbody>();
        if (ballRb != null)
        {
            ballRb.isKinematic = true;
        }

        if (animator != null)
        {
            animator.SetBool("Steal", true);
        }

        shootCooldown = Time.time + timeBeforeShoot; // Set cooldown before shooting

        // Start coroutine to continue moving during steal animation
        if (stealCoroutine != null)
        {
            StopCoroutine(stealCoroutine);
        }
        stealCoroutine = StartCoroutine(ContinueMovementDuringSteal());
    }

    IEnumerator ContinueMovementDuringSteal()
    {
        float stealDuration = animator.GetCurrentAnimatorStateInfo(0).length; // Assumes Steal animation is in Layer 0
        float elapsedTime = 0f;

        while (elapsedTime < stealDuration)
        {
            elapsedTime += Time.deltaTime;

            Vector3 directionToGoal = (goal.position - transform.position).normalized;
            Move(directionToGoal);

            yield return null;
        }

        StealEnd();
    }

    public void StealEnd()
    {
        if (stealCoroutine != null)
        {
            StopCoroutine(stealCoroutine);
        }

        if (animator != null)
        {
            animator.SetBool("Steal", false);
        }
    }

    void ShootAtGoal()
    {
        if (ball == null || goal == null) return;

        if (animator != null)
        {
            animator.SetBool("Shoot", true);
        }
    }

    public void AnimationEnd()
    {
        if (ball == null || goal == null) return;

        Vector3 randomGoalPosition = new Vector3(
            goal.position.x + Random.Range(-1.3f, 1.3f),
            goal.position.y + Random.Range(0.3f, 1.2f),
            goal.position.z + Random.Range(1, 3f)
        );

        DribblingBall dribblingBall = ball.GetComponent<DribblingBall>();
        if (dribblingBall != null)
        {
            dribblingBall.ShootSent(randomGoalPosition, true);
        }

        hasBall = false;
        ball.SetParent(null);
    }

    bool IsFacingGoal()
    {
        Vector3 directionToGoal = (goal.position - transform.position).normalized;
        float dotProduct = Vector3.Dot(transform.forward, directionToGoal);
        return dotProduct > 0.8f; // Adjust threshold as needed
    }
}