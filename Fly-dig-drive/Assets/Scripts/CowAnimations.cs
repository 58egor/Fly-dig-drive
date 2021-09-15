using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowAnimations : MonoBehaviour
{
    GameObject enemy;
    bool isCapture = false;
    bool isFind = false;
    float rotSpeed = 1f;
    float speed = 10f;
    float count = 0;
    public GameObject effect;
    Rigidbody body;
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
        enemy = GameObject.Find("enemy");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name=="enemy")
            Destroy(gameObject);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isCapture)
        {
            //effect.SetActive(true);
                if (Vector3.Distance(transform.position, enemy.transform.position) > 0.1f)
                {
                //body.velocity = new Vector3(0, 0, 0);
               transform.position = new Vector3(effect.transform.position.x, transform.position.y, transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, speed * Time.fixedDeltaTime);
                    transform.Rotate(new Vector3(rotSpeed, rotSpeed, rotSpeed));
                if(count <= 0)
                {
                    if (transform.localScale.x > 0.1f)
                    {
                        transform.localScale = new Vector3(transform.localScale.x - 0.1f, transform.localScale.y - 0.1f, transform.localScale.z - 0.1f);
                        count = 0.1f;
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
                else
                {
                    count -= Time.deltaTime;
                }
            }
                else
                {
                Destroy(gameObject);
            }
        }

    }
    public void SetAnim(GameObject obj=null)
    {
        effect = obj;
        isCapture = true;
    }
}
