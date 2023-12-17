//using Cinemachine.Editor;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class Swat : AIController
//{
//    //Animations
//    private Animator animator;
//    private int velocityXHash, velocityZHash, takeDamageHash, aimTargetHash, fireHash;

//    [Header("Gun settings")]
//    public GameObject bulletPrefab;
//    public Transform bulletSpawnPoint;
//    public float bulletSpeed;
//    public float shotCooldown;
//    private float _shotTime;

//    new void Start()
//    {
//        base.Start();
//        animator = GetComponent<Animator>();
//        velocityXHash = Animator.StringToHash("Velocity X");
//        velocityZHash = Animator.StringToHash("Velocity Z");
//        takeDamageHash = Animator.StringToHash("TakeDamage");
//        aimTargetHash = Animator.StringToHash("Aim target");
//        fireHash = Animator.StringToHash("Fire");
//    }

//    new void Update()
//    {
//        base.Update();
//        if (incapacitated)
//            return;

//        animator.SetFloat(velocityXHash, agent.velocity.normalized.x);
//        animator.SetFloat(velocityZHash, agent.velocity.normalized.z);

//        Shot();
//    }



//    #region Gun shooting

//    private void Shot()
//    {
//        animator.SetBool(aimTargetHash, canAttackThePlayer);

//        if (!canAttackThePlayer || _shotTime < shotCooldown)
//        {
//            _shotTime += Time.deltaTime;
//            return;
//        }

//        Vector3 toPlayer = playerTransform.position - transform.position;
//        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
//        bullet.GetComponent<Rigidbody>().AddForce(toPlayer.normalized * bulletSpeed, ForceMode.Impulse);
//        animator.SetTrigger(fireHash);

//        Destroy(bullet, 2f);
//        _shotTime = 0f;
//    }

//    #endregion

//    public void TakeDamageAnimation()
//    {
//        animator.SetTrigger(takeDamageHash);
//    }

//}
