using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal; 

public class gameManager : MonoBehaviour
{
    private Vector2 startingPosition;
    public float waitDuration = 0.5f;
    private TrailRenderer trail;
    private ShadowCaster2D shadow;

    private Rigidbody2D rb;
    private Movement playerMovement;
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<Movement>();
        trail = transform.Find("Trail").GetComponent<TrailRenderer>();
        shadow = transform.Find("Shadow").GetComponent<ShadowCaster2D>();;
    }

    void Start()
    {
        startingPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("obstacle"))
        {
            Die();
        }
    }

    private void Die(){
        StartCoroutine(Respawn());
    }IEnumerator Respawn(){
        rb.simulated = false;
        transform.localScale = Vector3.zero;
        
        rb.linearVelocity = Vector2.zero;
        trail.enabled = false;
        shadow.enabled = false;


        yield return new WaitForSeconds(waitDuration);
        transform.position = startingPosition;
        transform.localScale = Vector3.one;

        playerMovement.ResetMovement();
        shadow.enabled = true;
        rb.simulated = true;
        trail.enabled = true;
    }

}
