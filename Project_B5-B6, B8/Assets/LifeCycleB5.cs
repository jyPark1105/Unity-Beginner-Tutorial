using UnityEngine;

public class LifeCycleB5 : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("player data is ready");
    }

    void OnEnable()
    {
        Debug.Log("Player logged in");
    }

    void Start()
    {
        Debug.Log("Packed hunting gear");
    }

    void FixedUpdate()
    {
        Debug.Log("Moves");
    }

    void Update()
    {
        Debug.Log("Monster hunting");
    }

    void LateUpdate()
    {
        Debug.Log("Gain exp");
    }

    void OnDisable()
    {
        Debug.Log("Player logged out");
    }

    void OnDestroy()
    {
        Debug.Log("Player data has been destroyed");
    }
}