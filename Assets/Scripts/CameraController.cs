using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform targetTrans;
    [SerializeField]
    float distanceToTarget = 8f;
    [SerializeField]
    float heightToTarget = 5f;
    [SerializeField]
    Vector3 lookAtOffset = Vector3.up;
    [SerializeField]
    float rotateSpeed = 100f;

    void Start()
    {
        transform.position = targetTrans.position - (targetTrans.forward * distanceToTarget);
        transform.position += Vector3.up * heightToTarget;
        transform.LookAt(targetTrans.position + lookAtOffset);
    }

    void LateUpdate()
    {
        var lookTargetPos = targetTrans.position + lookAtOffset;

        Move(lookTargetPos);
        Rotate(lookTargetPos);

        transform.LookAt(lookTargetPos);
    }

    void Move(Vector3 lookTargetPos)
    {
        var cameraPos = lookTargetPos - (transform.forward * distanceToTarget);
        transform.position = cameraPos;
    }

    void Rotate(Vector3 lookTargetPos)
    {
        var h = Input.GetAxisRaw("CameraHorizontal");
        var v = Input.GetAxisRaw("CameraVertical");
        var dir = new Vector2(h, v);

        float rotX = dir.x * Time.deltaTime * rotateSpeed;
        float rotY = dir.y * Time.deltaTime * rotateSpeed;

        transform.RotateAround(lookTargetPos, Vector3.up, rotX);

        if (transform.forward.y > 0.9f && rotY < 0)
        {
            rotY = 0;
        }
        if (transform.forward.y < -0.9f && rotY > 0)
        {
            rotY = 0;
        }

        transform.RotateAround(lookTargetPos, transform.right, rotY);
    }
}
