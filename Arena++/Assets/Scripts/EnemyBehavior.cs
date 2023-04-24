using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public Transform patrolRoute;
    public List<Transform> locations;
    
    private int locationIndex = 0;
    private NavMeshAgent agent;
    private float hitFlash = 0f;
    private Renderer rend;
    private int _lives = 3;
    public Material flashMat;
    public Material enemyMat;


    public int EnemyLives
    {
        get {return _lives;}

        private set 
        {
            _lives = value;

            if (_lives <= 0)
            {
                Destroy(this.gameObject, 5f);
                GetComponent<NavMeshAgent>().enabled = false;
                GetComponent<Rigidbody>().freezeRotation = false;
                GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePosition;
                GetComponent<Rigidbody>().AddForce(Vector3.forward * 200f);
                Debug.Log("Enemy down.");
            }
        }
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
        rend = this.GetComponent<Renderer>();
    }

    void Update()
    {
            if (agent.remainingDistance < 0.2f && !agent.pathPending)
            {
                MoveToNextPatrolLocation();
            }

            if (hitFlash >= 0)
            {
                rend.material = flashMat;
                hitFlash -= Time.deltaTime;
            }
            else if (hitFlash <= 0)
            {
                rend.material = enemyMat;
            }
    }

    void InitializePatrolRoute() 
    {
        foreach(Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
            return;

        agent.destination = locations[locationIndex].position;

        locationIndex = (locationIndex + 1) % locations.Count;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player detected - attack!");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            agent.destination = player.position;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player out of range, resume patrol");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            GetComponent<AudioSource>().Play();
            EnemyLives -= 1;
            Debug.Log("Critical hit!");
            hitFlash = 0.2f;

        }
    
    }
}