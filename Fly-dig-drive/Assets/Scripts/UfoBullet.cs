using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoBullet : MonoBehaviour
{
    public float speed = 30f;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 13)
        {
            enemy.GetComponent<CowAnimations>().SetAnim();
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Enter2:" + enemy.name);
        if (name != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, speed * Time.fixedDeltaTime);
        }
    }
}
