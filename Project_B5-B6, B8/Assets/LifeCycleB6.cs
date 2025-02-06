using UnityEngine;

public class LifeCycleB6 : MonoBehaviour
{
    void Start()
    {
        //Vector3 vec = new Vector3(0, 0, 0);
        
    }
    void Update()
    {
        Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"), 0);
        transform.Translate(vec);
    }
}