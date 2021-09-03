using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bombs : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Plane" || other.gameObject.name == "Car" || other.gameObject.name == "Capsule")
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("Bum");
        }
    }
}
