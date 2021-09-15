using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlyCows : MonoBehaviour
{
    public double count = 1f;
    public int countSpawn=10;
    int spawn;
    int dead = 0;
    double cnt;
    public GameObject cows;
    GameObject player;
    public GameObject win;
    bool end = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        cnt = 0;
        spawn = countSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (countSpawn > 0 && !end)
        {
            if (cnt <= 0)
            {
                Vector3 vec;
                vec.x = Random.Range(transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2);
                vec.y= Random.Range(transform.position.y - transform.localScale.y / 2, transform.position.y + transform.localScale.y / 2);
                vec.z = -1;
                Instantiate(cows, vec, Quaternion.identity);
                countSpawn--;
                cnt = count;

            }
            else
            {
                cnt -= Time.deltaTime;
            }
        }
        if (dead >= spawn)
        {
            if (!end)
            {
                player.GetComponent<BonusGameControl>().enabled = false;
                win.SetActive(true);
                end = true;
            }
            player.transform.position = Vector3.MoveTowards(player.transform.position, player.transform.position + Vector3.right * 15, 15 * Time.deltaTime);
        }
    }
    public void CowDead()
    {
        dead+=1;
        Debug.Log("dead:" + dead + " all:" + spawn);
    }
}
