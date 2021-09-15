using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damage = 1;
    public float speed = 50f;
    GameObject enemy;
    public GameObject effect;
    public bool kill = false;
	// Start is called before the first frame update
	void OnTriggerEnter(Collider coll)
	{
        if (coll.gameObject.layer == 11)
        {
            if (kill)
            {
                coll.gameObject.GetComponent<EnemyWantKill>().ImDead();
            }
            else
            {
                coll.gameObject.GetComponent<EnemyInfo>().Damage(damage);
            }
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
	}
    private void Start()
    {
        enemy = GameObject.Find("enemy");
    }
    private void FixedUpdate()
    {
        if (enemy != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, speed * Time.fixedDeltaTime);
            transform.LookAt(enemy.transform.position);
        }
        else
        {
            Destroy(gameObject);
        }
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 90f);
        
    }
}
