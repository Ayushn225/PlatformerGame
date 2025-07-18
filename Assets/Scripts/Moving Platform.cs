using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public float waitDuration;
    private Transform target;

    private Movement playerMovement;
    private Rigidbody2D platformRB;
    private Rigidbody2D playerRB;
    private Vector3 direction;

    public GameObject ways;
    public Transform[] waypoints;
    private int wayIndex;
    private int count;
    private int IndexDirection;

    void Awake(){
        platformRB = GetComponent<Rigidbody2D>();
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();

        waypoints = new Transform[ways.transform.childCount];
        for(int i = 0; i<ways.transform.childCount; i++){
            waypoints[i] = ways.transform.GetChild(i).transform;
        }
    }

    void Start()
    {
        target = waypoints[1];
        wayIndex = 1;
        count = waypoints.Length;
        IndexDirection = 1;
        getDirection();
    }

    void getDirection()
    {
        if (target == null) return;
        if (waypoints.Length == 0) return;

        direction = (target.position - transform.position).normalized;
    }

    void FixedUpdate()
    {

        platformRB.linearVelocity = direction*speed;
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            direction = Vector2.zero;
            if(wayIndex == count - 1)
            {
                IndexDirection = -1;
            }
            else if(wayIndex == 0)
            {
                IndexDirection = 1;
            }

            wayIndex += IndexDirection;

            target = waypoints[wayIndex];
            StartCoroutine(WaitThenMove());
        }
    }IEnumerator WaitThenMove(){
        yield return new WaitForSeconds(waitDuration);
        getDirection();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerMovement.onPlatform = true;
            playerMovement.platformRB = platformRB;
            playerRB.gravityScale *= 50;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerMovement.onPlatform = false;
            playerRB.gravityScale /= 50;
        }
    }
}
