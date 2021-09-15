using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    int value = 1;
    float speed=80f;
    public bool isGet = false;
    bool once = false;
    AudioManager audio;
   // GameObject moneys;
    public float count = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 9)
        {
            //SaveLoadOperations.SaveCollectedMoneys(value);
     
            isGet = true;
        }
    }
    private void Start()
    {
        audio = gameObject.GetComponent<AudioManager>();
        //moneys = GameObject.Find("Moneys2");
        
    }
    private void Update()
    {
        if (isGet)
        {
           
            Vector3 p = Camera.main.WorldToScreenPoint(transform.position);
            p = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, p.z));
            Debug.Log("Moneys:" + p);
            if (count <= 0)
            {
                if (!once)
                {
                    audio.Play("Coin");
                    once = true;
                }
                if (Vector2.Distance(transform.position, p) > 0.1f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, p, speed * Time.deltaTime);
                }
                else
                {
                    SaveLoadOperations.SaveCollectedMoneys(value);
                    Destroy(gameObject);
                }
            }
            else
            {
                count -= Time.deltaTime;
            }
        }
    }
    private void OnBecameInvisible()
    {
        SaveLoadOperations.SaveCollectedMoneys(value);
        Destroy(gameObject);
    }
    void OnDrawGizmosSelected()
    {
        Vector3 p = Camera.main.WorldToScreenPoint(transform.position);
       p = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, p.z));
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(p, 0.1F);
    }

}
