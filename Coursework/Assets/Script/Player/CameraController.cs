using System.Numerics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 8f;
    public UnityEngine.Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        UnityEngine.Vector3 desiredPosition = new UnityEngine.Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            target.position.z + offset.z
        );
        UnityEngine.Vector3 smoothPosition = UnityEngine.Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.position = smoothPosition;
    }
}
