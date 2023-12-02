using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/**
 * This class is used to determine basic logic and the locomotion beahaviour of a grounded enemy. 
 * It determines its movement and its behaviour against the player
 */
[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(NavMeshAgent))]
public abstract class GroundEnemy : MonoBehaviour, IGrabbable, IDamageable
{

    [Header("Ground Enemy Settings")]
    public GroundEnemyParameters enemyParameters;
    private Rigidbody _rb;
    private int _currentHealth;

    [Header("Grab fields")]
    public float enemyGrabResistence;
    [HideInInspector]
    public float grabResistence { get => enemyGrabResistence; set => value = enemyGrabResistence; }


    [Header("Navigation fields")]
    public Transform[] patrolPoint;
    [Tooltip("Raggio della sfera di rilevamento del giocatore")]
    public float playerDetectionRange;
    public LayerMask playerMask;
    public float stoppingDistanceToPlayer;
    private Transform _currentPatrolPoint;
    private int _currentPatrolPointIndex;
    [HideInInspector] protected NavMeshAgent agent;
    protected bool isFollowingThePlayer, canAttackThePlayer;

    [Header("Player references"), HideInInspector]
    public Transform playerTransform;

    [Header("Events")]
    public GameEvent takeDamageEvent;

    // == Utility
    [HideInInspector] public bool incapacitated;

    protected void Start()
    {
        _rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        _currentHealth = enemyParameters.hp;
        agent.speed = enemyParameters.speed;
    }

    protected void Update()
    {
        if (incapacitated)
            return;

        NavigationHandler();
    }

    #region Navigation

    private void NavigationHandler()
    {
        isFollowingThePlayer = FollowPlayer();

        if (!isFollowingThePlayer && agent.remainingDistance < .5f)
            PatrolToTheNextPoint();
    }

    private void PatrolToTheNextPoint()
    {
        agent.stoppingDistance = 0f;
        agent.isStopped = false;
        _currentPatrolPointIndex = Random.Range(0, patrolPoint.Length);
        _currentPatrolPoint = patrolPoint[_currentPatrolPointIndex];
        agent.SetDestination(_currentPatrolPoint.position);
    }


    private bool FollowPlayer()
    {
        if (playerTransform == null)
            return false;

        if (Physics.CheckSphere(transform.position, playerDetectionRange, playerMask))
        {
            Debug.DrawLine(transform.position, playerTransform.position, Color.yellow);
            float distance = Vector3.Distance(playerTransform.position, transform.position);
            if (distance <= stoppingDistanceToPlayer)
            {
                agent.isStopped = true;
                transform.LookAt(playerTransform.position);
                canAttackThePlayer = true;
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(playerTransform.position);
                agent.stoppingDistance = stoppingDistanceToPlayer;
                canAttackThePlayer = false;
            }
            return true;
        }

        agent.stoppingDistance = 0;
        return false;
    }

    #endregion


    #region Interfaces

    public void IsGrabbed()
    {
        //TODO completare
        incapacitated = true;
    }

    public void IsDropped()
    {
        incapacitated = false;
    }

    public void TakeDamage(int damage)
    {
        takeDamageEvent.RaiseEvents();

        StartCoroutine(IncapacitatedReset());
        transform.DORotate(playerTransform.position, 0f);
        transform.DOMove(transform.position - (transform.forward), .3f).SetDelay(.1f);
        _currentHealth -= damage;
        if (_currentHealth < 0)
            Destroy(gameObject);
    }

    IEnumerator IncapacitatedReset()
    {
        incapacitated = true;
        yield return new WaitForSeconds(1);
        incapacitated = false;
    }


    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);
    }

}
