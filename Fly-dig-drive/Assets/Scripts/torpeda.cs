using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torpeda : MonoBehaviour
{

    public GameObject bul;
    public bool kill = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==8 || other.gameObject.layer == 9)
        {
            GameObject obj=Instantiate(bul, transform.position, Quaternion.identity);
            if (kill)
            {
                obj.GetComponent<BulletScript>().kill = kill;
            }
            Instantiate(bul, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
       
    }
    // Start is called before the first frame update
    
}
