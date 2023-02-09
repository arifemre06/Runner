using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = transform.position;
        Vector3 newPosition = new Vector3(pos.x, pos.y, offset.z + target.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, 0.2f);
    }
}
