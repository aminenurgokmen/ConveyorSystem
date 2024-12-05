using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductScript : MonoBehaviour
{
    private Quaternion targetRotation;
    private Transform objectToRotate;
    public float rotationSpeed = 1f;
    private bool isRotating = false;
    private float rotationThreshold = 0.01f;
    float timer = 0;
    private void Update()
    {
        if (isRotating && objectToRotate != null)
        {
            objectToRotate.rotation = Quaternion.Lerp(objectToRotate.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            if (Quaternion.Angle(objectToRotate.rotation, targetRotation) < rotationThreshold)
            {
                Vector3 finalEulerAngles = objectToRotate.rotation.eulerAngles;
                finalEulerAngles.y = Mathf.Round(finalEulerAngles.y / 90f) * 90f;
                objectToRotate.rotation = Quaternion.Euler(finalEulerAngles);
                isRotating = false;
            }
        }
        if (timer < 1)
        {
            timer += Time.deltaTime;

            if (timer >= 1)
            {
                GetComponent<CapsuleCollider>().enabled = true;
                timer = 0f;
            }
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
        }

        float currentYRotation = transform.eulerAngles.y;
        if (other.gameObject.CompareTag("Turn"))
        {
            objectToRotate = transform;
            targetRotation = Quaternion.Euler(0, currentYRotation - 90, 0);
            isRotating = true;

        }

    }
    
}
