using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NewEnemy : MonoBehaviour
{
    public float health;
    public float pointsToGive;

    public GameObject player;

    public float waitTime;
    private float currentTime;
    private bool shot;

    public GameObject bullet;
    public Transform bulletSpawnPoint;

    NavMeshAgent pathFinder;
    Transform target;

    public event System.Action OnDeath;

    public void Start()
    {
        pathFinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        player = GameObject.FindWithTag("Player");

        bulletSpawnPoint = this.transform.GetChild(2);
        
    }

    public void Update()
    {
        pathFinder.SetDestination(target.position);

        if (!bulletSpawnPoint)
        {
            bulletSpawnPoint = this.transform.GetChild(2);
        }

        if (health <= 0)
        {
            Die();
        }

        this.transform.LookAt(player.transform);

        if (currentTime == 0)
        {
            Shoot();
        }

        if (shot && currentTime < waitTime)
        {
            currentTime += 1 * Time.deltaTime;
        }

        if (currentTime >= waitTime)
        {
            currentTime = 0;
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
        player.GetComponent<NewPlayer>().points += pointsToGive;      
    }

    public void Shoot()
    {
        shot = true;

        if (OnDeath != null)
        {
            OnDeath();
        }

        Instantiate(bullet.transform, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
    }
}
