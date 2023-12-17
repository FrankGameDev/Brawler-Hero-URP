using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class SwatBehaviour : MonoBehaviour, IDamageable, IGrabbable
{
    //State references
    [HideInInspector] public AIBaseState<SwatBehaviour> currentState;

    [Header("References")]
    public GroundEnemyParameters enemyParameters;
    [HideInInspector] public NavMeshAgent agent;
    private int _currentHealth;

    //Animator
    private int velocityHash, takeDamageHash, aimTargetHash, fireHash;
    private Animator _animator;

    [Header("States")]
    [HideInInspector] public SwatPatrol patrolState;
    [HideInInspector] public SwatAttack attackState;
    [HideInInspector] public SwatChase chaseState;

    [Header("Player references"), HideInInspector]
    public Transform playerTransform;

    
    [Header("Navigation fields")]
    public float playerDetectionRange;
    public LayerMask playerMask;
    public float stoppingDistanceToPlayer;
    public Transform[] patrolPoint;
    [HideInInspector] public Transform _currentPatrolPoint;
    [HideInInspector] public int _currentPatrolPointIndex;


    [Header("Attack parameters"),Space]
    [Header("Gun settings")]
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed;
    public float shotCooldown;
    [HideInInspector] public float shotTime;
    [Header("Grab fields")]
    public float enemyGrabResistence;
    [HideInInspector] public float grabResistence { get => enemyGrabResistence; set => value = enemyGrabResistence; }

    [Space, Header("Events")]
    public GameEvent takeDamageEvent;

    //Utility
    private bool incapacitated;
    private bool canAttackThePlayer;

    void Start()
    {
        //Init references
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        patrolState = new SwatPatrol(this);
        attackState = new SwatAttack(this);
        chaseState = new SwatChase(this);
        _animator = GetComponent<Animator>();
        _currentHealth = enemyParameters.hp;
        agent.speed = enemyParameters.speed;

        //Hashing animations
        velocityHash = Animator.StringToHash("Velocity");
        takeDamageHash = Animator.StringToHash("TakeDamage");
        aimTargetHash = Animator.StringToHash("Aim target");
        fireHash = Animator.StringToHash("Fire");


        currentState = patrolState;
    }

    void Update()
    {
        if (incapacitated)
            return;

        WalkingAnimation();
        currentState.UpdateStates();
        Debug.LogWarning(currentState);
    }


    public bool IsPlayerNear()
    {
        return Physics.CheckSphere(transform.position, playerDetectionRange, playerMask);
    }

    public bool IsPlayerOnTarget()
    {
        return Vector3.Distance(playerTransform.position, transform.position) <= stoppingDistanceToPlayer;
    }

    public void TakeDamageAnimation()
    {
        _animator?.SetTrigger(takeDamageHash);
    }

    #region Interfaces

    public void IsGrabbed()
    {
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
        Vector3 knockbackDir = playerTransform.position - transform.position;
        knockbackDir.y = 0;
        transform.DOMove(playerTransform.position - knockbackDir.normalized * 3.5f, .5f).SetDelay(.1f);
        _currentHealth -= damage;
        if (_currentHealth < 0)
            Destroy(gameObject);
    }

    IEnumerator IncapacitatedReset()
    {
        incapacitated = true;
        yield return new WaitForSeconds(1.25f);
        incapacitated = false;
    }

    #endregion


    public void Shot()
    {
        transform.DOLookAt(playerTransform.position, .25f);
        if (shotTime < shotCooldown)
        {
            shotTime += Time.deltaTime;
            return;
        }

        Vector3 toPlayer = playerTransform.position - transform.position;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(toPlayer.normalized * bulletSpeed, ForceMode.Impulse);
        _animator.SetTrigger(fireHash);

        Destroy(bullet, 2f);
        shotTime = 0f;
    }

    public void AimingAnimation(bool b)
    {
        _animator.SetBool(aimTargetHash, b);
    }

    public void WalkingAnimation()
    {
        _animator.SetFloat(velocityHash, agent.velocity.normalized.magnitude);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);
    }


}
