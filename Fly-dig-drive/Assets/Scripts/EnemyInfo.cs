using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    public GameObject bomb;
    public GameObject coin;
    public GameObject traectory;
    public int hp = 100;
    public int spawnBomb = 5;
    int minus;
    public int bombCount = 2;
    int bCount;
    public float spawnCount = 0.5f;
    float spawn = 0;
    bool spawnActive = false;
    public GameObject effcet;
    // Start is called before the first frame update
    void Start()
    {
        minus = spawnBomb;
        bCount = bombCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnActive && spawn <= 0 && hp>0)
        {
            Instantiate(bomb, transform.position, Quaternion.identity);
            bCount--;
            if (bCount == 0)
            {
                bCount = bombCount;
                spawnActive = false;
            }
            spawn = spawnCount;
        }
        else
        {
            spawn -= Time.deltaTime;
        }
    }
    public void Damage(int HP)
    {
        minus--;
        if (minus <= 0)
        {
            spawnActive = true;
            minus = spawnBomb;
        }
        hp -= HP;
        for(int i = 0; i < HP; i++)
        {
            GameObject obj= Instantiate(coin, transform.position, Quaternion.identity);
            obj.GetComponent<Money>().isGet = true;
            obj.GetComponent<Money>().count = 0.2f;
        }
        //if (hp <= 0)
        //{
        //    Instantiate(effcet, transform.position, Quaternion.identity);
        //    Destroy(gameObject);
        //}
    }
}
