using UnityEngine;

public class MoveB7 : MonoBehaviour
{
    // Current Position, Target Position, Reference Speed, Speed
    void Update()
    {
        Vector3 target = new Vector3(0.014f, 0.4f, 1.38f);
        
        // 1. MoveTowards
        transform.position = Vector3.MoveTowards(transform.position, target, 0.3f);

        // 2. SmoothDamp
        Vector3 velo = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velo, 0.25f);

        // 3. Lerp
        transform.position = Vector3.Lerp(transform.position, target, 0.05f);

        // 4. Slerp
        transform.position = Vector3.Slerp(transform.position, target, .5f);

    }
}