using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbo : MonoBehaviour
{
    public float addSpeed=2f;
    public float time=1f;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        Debug.Log("Попався");
        if (obj.gameObject.layer == 8 || obj.gameObject.layer == 9)
        {
            Debug.Log("Попався2");
            obj.transform.GetComponentInParent<Control>().SetTurbo(addSpeed, time);
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
