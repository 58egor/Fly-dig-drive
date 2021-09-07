using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bombs : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject effect;
    GameObject loose;
    private void Start()
    {
        if (loose == null)
        {
            loose = GameObject.Find("Loose");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Plane" || other.gameObject.name == "Car" || other.gameObject.name == "Capsule")
        {
            Instantiate(effect, other.transform.position, Quaternion.identity);
            other.transform.parent.gameObject.GetComponent<Control>().set();
            Destroy(other.transform.parent.gameObject);
            Debug.Log("Bum");
        }
    }
}
