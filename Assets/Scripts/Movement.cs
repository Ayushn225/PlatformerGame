using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    private float speedMultiplier = 0;

    [Range(0, 10)]
    [SerializeField] private float acceleration;

    private bool buttonPressed = false;

    private bool isWallTouch = false;
    public LayerMask wallLayer;
    public Transform WallCheckPoint;

    Vector2 relativeTransform;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        updateInverseVector();
    }

    void FixedUpdate()
    {
        updateSpeedMultiplier();
        float targetSpeed = speed * speedMultiplier*relativeTransform.x;
        rb.linearVelocity = new Vector2(targetSpeed, rb.linearVelocity.y);

        isWallTouch = Physics2D.OverlapBox(WallCheckPoint.position, 
            new Vector2(0.05f, 0.6f),
            0, 
            wallLayer
        );


        if(isWallTouch)
        {
            flip();
        }
    }

    private void flip(){
        transform.Rotate(0, 180, 0);
        updateInverseVector();
        rb.linearVelocity = new Vector2(-rb.linearVelocity.x, rb.linearVelocity.y);
    }

    private void updateInverseVector(){
        relativeTransform = transform.InverseTransformVector(Vector2.one);
    }
    public void move(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            buttonPressed = true;
        }
        else if (value.canceled)
        {
            buttonPressed = false;
        }
    }

    private void updateSpeedMultiplier(){
        if(buttonPressed && speedMultiplier < 1){
            speedMultiplier += Time.deltaTime* acceleration;
        }else if(!buttonPressed && speedMultiplier > 0){
            speedMultiplier -= Time.deltaTime * acceleration;
        }
        speedMultiplier = Mathf.Clamp(speedMultiplier, 0, 1);
    }
}
