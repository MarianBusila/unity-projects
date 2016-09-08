using UnityEngine;
using System.Collections;

public abstract class TurretTargettingSystem : MonoBehaviour {

    public ArrayList enemyGameObjects;
    public GameObject currentTarget;
    public int turretSpeed = 2;

    public enum TurretState { Disabled, Idle, LockingOn, Engaged };
    protected TurretState currentTurretState;

    public float delayBetweenFire = 1f;
    protected float fireCooldown;

    protected virtual void Start()
    {
        enemyGameObjects = new ArrayList();
        SetCurrentTurretState(TurretState.Idle);
        fireCooldown = Time.time;
    }

    void Update()
    {
        if(currentTurretState == TurretState.Idle)
        {
            CheckForEnemiesInRange();
        }
        else if (currentTurretState == TurretState.LockingOn)
        {
            LockOn();
        }
        else if (currentTurretState == TurretState.Engaged)
        {
            LookAtTarget();
            MaybeFire();
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        //Debug.Log("Enemy " + collider.gameObject + " in range");
        if (collider.gameObject.tag == "Enemy")
            enemyGameObjects.Add(collider.gameObject);

    }

    void OnTriggerExit(Collider collider)
    {
        //Debug.Log("Enemy " + collider.gameObject + " dropped out of range");
        if (collider.gameObject.tag == "Enemy")
        {
            enemyGameObjects.Remove(collider.gameObject);

            if(currentTarget == collider.gameObject)
            {
                currentTarget = null;
                SetCurrentTurretState(TurretState.Idle);
            }
        }
    }

    void CheckForEnemiesInRange()
    {
        if(enemyGameObjects.Count > 0)
        {
            int neareastEnemyIndex = GetNearestEnemyIndex();
            //Debug.Log("Neareast enemy: " + neareastEnemyIndex);
            currentTarget = (GameObject)enemyGameObjects[0];
            SetCurrentTurretState(TurretState.LockingOn);
        }
    }

    int GetNearestEnemyIndex()
    {
        float neareastDistance = 9999f;
        int neareastEnemyIndex = 0;
        for(int i = 0; i < enemyGameObjects.Count; i++)
        {
            float distanceToObject = Vector3.Distance(transform.position, ((GameObject)enemyGameObjects[i]).transform.position);
            if (distanceToObject < neareastDistance)
            {
                neareastEnemyIndex = i;
                neareastDistance = distanceToObject;
            }
        }
        return neareastEnemyIndex;
    }

    void LockOn()
    {
        Vector3 currentTargetPosition = currentTarget.transform.position;
        currentTargetPosition.y = transform.position.y; // this is to ensure the turret does not angle up or down
        Vector3 targetDirection = currentTargetPosition - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDirection, turretSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        float remainingRotation = Mathf.Abs(Quaternion.LookRotation(transform.forward).eulerAngles.y - Quaternion.LookRotation(targetDirection).eulerAngles.y);
        if (remainingRotation < 2.5f)
            SetCurrentTurretState(TurretState.Engaged);
    }

    void LookAtTarget()
    {
        if (currentTarget != null)
        {
            Vector3 targetPosition = currentTarget.transform.position;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
        }

    }

    public void SetCurrentTurretState(TurretState state)
    {
        currentTurretState = state;
        if (currentTurretState == TurretState.Engaged)
            EngagedTarget();
        else
            DisengagedTarget();
    }
    abstract protected void MaybeFire();
    abstract protected void EngagedTarget();
    abstract protected void DisengagedTarget();
    

}
