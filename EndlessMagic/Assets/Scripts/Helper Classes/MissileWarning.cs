using UnityEngine;
using System.Collections;

public class MissileWarning : MonoBehaviour {
    GameObject EnemyInPath = null;
    float distance;
    Ray probe;
    RaycastHit hit;
    // Update is called once per frame
    void FixedUpdate () {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
       
        probe.origin = transform.position;
        probe.direction = transform.forward;
        if (Physics.Raycast(probe, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                EnemyInPath = hit.collider.gameObject;
                distance = hit.distance;
                WarnEnemy();
            }
        }
	}

    void WarnEnemy()
    {
       EnemyMovement eMove = EnemyInPath.GetComponent<EnemyMovement>();
        eMove.AvoidProjectile(transform.position, distance);
    }
}
