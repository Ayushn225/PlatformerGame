using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform playerTransform;
    Vector3 velocity = Vector3.zero;

    [Range(0, 1)]
    public float smoothtime;

    public Vector3 positionOffset;

    [Header("Axis Limits")]
    public Vector2 xLimit;
    public Vector2 yLimit;

    void Awake(){
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate(){
        Vector3 targetPosition = playerTransform.position + positionOffset;
        targetPosition.x = Mathf.Clamp(targetPosition.x, xLimit.x, xLimit.y);
        targetPosition.y = Mathf.Clamp(targetPosition.y, yLimit.y, yLimit.x);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothtime);
    }
}
