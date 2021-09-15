using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWantKill : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public GameObject end;
    GameObject player;
    bool dead = false;
    public GameObject dop;
    public GameObject effect;
    public GameObject laser;
    AudioManager auido;
    void Start()
    {
        auido = gameObject.GetComponent<AudioManager>();
        auido.Play("Enemy");
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!dead)
        {
            if(player!=null)
            transform.position = new Vector3(transform.position.x, player.transform.position.y + 6f, transform.position.z);
            Vector3 vec = new Vector3(end.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, vec, speed * Time.fixedDeltaTime);
        }
        else
        {
            laser.SetActive(false);
            if (Vector3.Distance(transform.position, end.transform.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, end.transform.position, speed* Time.fixedDeltaTime);
            }
            else
            {
                dop.SetActive(true);
                Instantiate(effect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
    public void ImDead()
    {
        Debug.Log("Умер");
        dead = true;
    }
}
