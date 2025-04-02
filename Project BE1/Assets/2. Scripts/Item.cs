using UnityEngine;

public class Item : MonoBehaviour
{
    public float rotateSpeed;

    void Update()
    {
        // Global Rotation
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }
}