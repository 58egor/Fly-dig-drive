using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Rigidbody bullet;
    public float gunCount = 0.5f;
    float count=0;
    public float speed=40;
    int layerMask;
    void Start()
    {
        transform.position = player.transform.position;
        layerMask = 1 << 11;
        Debug.Log("Name:" + layerMask);
        //layerMask = ~layerMask;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position = player.transform.position;
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.right);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && count <= 0)
            {
                Debug.Log("Name:" + hit.collider.name);
                Debug.Log("Pos:" + hit.point);
                Instantiate(bullet, transform.position, Quaternion.identity);
                count = gunCount;
            }
            else
            {
                count -= Time.deltaTime;
            }
        }
    }
}

