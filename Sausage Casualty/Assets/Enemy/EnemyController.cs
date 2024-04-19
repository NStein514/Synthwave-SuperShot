using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float life = 10; 
    private Transform target;
    private float moveSpeed = 6f;

    void Update()
    {
        if (target != null)
        {
            // Calculate direction vector from enemy to player
            Vector3 direction = (target.position - transform.position).normalized;

            // Move the enemy towards the player 
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            // Adjust the Y-axis position of the enemy to follow the player
            transform.position = new Vector3(transform.position.x, transform.position.z);

            // Rotate the enemy in the direction of the player 
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log(life);
            life--;
            if (life < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
