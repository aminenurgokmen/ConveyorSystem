using UnityEngine;

public class PushSphere : MonoBehaviour
{
    private float pushSpeed;
    private float offset;


    private void Update()
    {
        (pushSpeed, offset) = SpawnCube.Instance.GetCornerCubeProperties(gameObject);

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody>();

            if (otherRb != null)
            {

                Vector3 pushDirection = transform.forward + (offset > 0 ? transform.right : -transform.right) * Mathf.Abs(offset);
                otherRb.velocity = pushDirection.normalized * pushSpeed;

                otherRb.constraints = RigidbodyConstraints.FreezeRotationX
                                     | RigidbodyConstraints.FreezeRotationZ
                                     | RigidbodyConstraints.FreezeRotationY;
            }
        }
    }

}
