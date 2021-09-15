using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowDown : MonoBehaviour
{
    public float speed=10f;
   public  GameObject coin;
    public GameObject effect;
    float speedX=0;
    float speedY = 0;
    float speedZ = 0;
    float speedRot=1f;
    int value = 3;
    bool dead = true;
    SpawnFlyCows spawnCows;
    private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("trig:"+ other.gameObject.layer);
        if (other.gameObject.layer == 8)
        {
            for (int i = 0; i < value; i++)
            {
                GameObject obj = Instantiate(coin, transform.position, Quaternion.identity);
                obj.GetComponent<Money>().isGet = true;
                obj.GetComponent<Money>().count = 0.2f+i/10f;
               
            }
            if (dead)
            {
                dead = false;
                spawnCows.CowDead();
                Destroy(gameObject); ;
            }
        }else
        if (other.gameObject.layer == 10)
        {
            dead = true;
            Instantiate(effect, transform.position, Quaternion.identity);
            if (dead)
            {
                dead = false;
                spawnCows.CowDead();
                Destroy(gameObject); ;
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        spawnCows = GameObject.Find("Spawner").GetComponent<SpawnFlyCows>();
        speedX = Random.Range(-speedRot, speedRot);
        speedY = Random.Range(-speedRot, speedRot);
        speedZ = Random.Range(-speedRot, speedRot);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = transform.position + Vector3.down * speed * Time.fixedDeltaTime;
        transform.Rotate(new Vector3(speedX, speedY, speedZ));
    }
}
