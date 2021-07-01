using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class GroundSpawnScript : MonoBehaviour
{
    [SerializeField] private Config config;
    [SerializeField] private UnityEvent groundSpawnedEvent;

    private void Awake()
    {
        transform.localScale = new Vector3(
            config.GroundSize.x, 
            transform.localScale.y, 
            config.GroundSize.y
            );
        groundSpawnedEvent.Invoke();
    }
}
