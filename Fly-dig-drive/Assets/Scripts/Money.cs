using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    int value = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Plane" || other.gameObject.name == "Car" || other.gameObject.name == "Capsule")
        {
            SaveLoadOperations.SaveCollectedMoneys(value);
            Destroy(gameObject);
        }
    }
}
