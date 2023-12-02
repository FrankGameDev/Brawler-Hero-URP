using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerGrabController : MonoBehaviour
{
    [Header("References")]
    public Transform grabPosition;
    public float grabDetectionRadius;

    [Header("Player grab parameters")]
    public PlayerCombatStats playerStats;
    public LayerMask enemyLayer;

    private Collider _enemyGrabbed;
    private IGrabbable _enemyGrabbable => _enemyGrabbed != null ? _enemyGrabbed.GetComponent<IGrabbable>() : null;
    private Vector3 _inputDir => InputManager.Instance.GetPlayerMovement();


    private void Update()
    {
        if (_enemyGrabbed != null)
            MoveGrabbedEnemy();
    }

    #region Grab and Throw

    //Gestito dall'evento GrabAndDropEvent
    public void GrabAndDropLogic()
    {
        if (_enemyGrabbed == null)
            Grab();
        else
            DropGrabbedEnemy();
    }

    private void Grab()
    {
        _enemyGrabbed = CheckGrabbableEnemyInRange();
        Debug.Log("Grabbo");

        if (_enemyGrabbed == null)
            return;

        _enemyGrabbable.IsGrabbed();
        Debug.Log(_enemyGrabbed.name + " grabbato");

        //Imposto il transform del nemico grabbato, al fine di portarlo in giro
        _enemyGrabbed.transform.parent = grabPosition;
        _enemyGrabbed.transform.position = grabPosition.localPosition;
        _enemyGrabbed.transform.Rotate(0, 0, 90);
    }

    private void DropGrabbedEnemy()
    {
        Debug.Log("Drop " + _enemyGrabbed.name);
        _enemyGrabbed.transform.parent = null;
        _enemyGrabbable.IsDropped();
        _enemyGrabbed = null;
    }

    //Gestito dal ThrowEvent
    public void Throw()
    {
        Vector3 throwDirection = _inputDir == Vector3.zero ? Camera.main.transform.forward : _inputDir;
        _enemyGrabbed.GetComponent<Rigidbody>().AddForce(throwDirection * playerStats.throwForce, ForceMode.Impulse);
        DropGrabbedEnemy();
    }


    // ============= UTILITY ===============

    private void MoveGrabbedEnemy()
    {
        if (_enemyGrabbed == null)
            return;

        _enemyGrabbed.transform.position = grabPosition.position;
    }

    Collider CheckGrabbableEnemyInRange()
    {
        Collider[] coll = Physics.OverlapSphere(grabPosition.position, grabDetectionRadius, enemyLayer);

        if (coll.Length == 0)
            return null;

        return coll
            .Where(enemyCollider => enemyCollider.GetComponent<IGrabbable>() != null)
            .Select(enemyCollider => enemyCollider.GetComponent<Collider>())
            .Where(_enemyGrabbed => _enemyGrabbed.TryGetComponent(out IGrabbable grabbableEnemy) && grabbableEnemy.IsGrabbable(playerStats.grabForce))
            .OrderBy(_enemyGrabbed => (_enemyGrabbed.transform.position - transform.position).sqrMagnitude)
            .FirstOrDefault();
    }

    public bool IsEnemyGrabbed() => _enemyGrabbed != null;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(grabPosition.position, grabDetectionRadius);
    }
    #endregion
}
