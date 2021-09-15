using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour
{
    public GameObject bullet;
    public GameObject rot;
    bool start = false;
    bool capture = false;
    GameObject capteruCow;
    GameObject parent;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if((obj.gameObject.layer==8 || obj.gameObject.layer == 9) && !start)
        {
            Debug.Log("start");
            obj.GetComponent<Control>().set2();
            gameObject.GetComponentInParent<MovingEnemy>().enabled = true;
            start = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer==13)
        if (!capture)
        {
            if (other.gameObject.transform.position.x > transform.position.x && other.gameObject.transform.position.y <transform.position.y)
            {
                other.gameObject.GetComponent<CowAnimations>().SetAnim(bullet);
                capteruCow = other.gameObject;
                //bullet.SetActive(true);
                //rot.transform.LookAt(capteruCow.transform);
                //rot.transform.rotation = new Quaternion(-rot.transform.rotation.x, rot.transform.rotation.y, rot.transform.rotation.z, rot.transform.rotation.w);
                bullet.transform.localScale = new Vector3(bullet.transform.localScale.x, (transform.position.y - other.gameObject.transform.position.y) / 2, bullet.transform.localScale.z);
                bullet.transform.localPosition = new Vector3(0, -((transform.position.y - other.gameObject.transform.position.y) / 2), 0);
                capture = true;

            }
        }
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("Enter");
    //    if (other.gameObject.layer == 13)
    //    {
    //        Debug.Log("Enter:"+other.name);
    //        GameObject obj= Instantiate(bullet, transform.position, Quaternion.identity);
    //        obj.GetComponent<UfoBullet>().enemy = other.gameObject;
    //    }
    //}
    void Start()
    {
        parent = transform.parent.gameObject;
        Debug.Log("Enter1");
    }

    // Update is called once per frame
    void Update()
    {
        if (capteruCow != null && capture)
        {
            //rot.transform.LookAt(capteruCow.transform);
            //rot.transform.rotation = new Quaternion(-rot.transform.rotation.x, rot.transform.rotation.y, -rot.transform.rotation.z,rot.transform.rotation.w);
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x, (transform.position.y - capteruCow.transform.position.y) / 2, bullet.transform.localScale.z);
            bullet.transform.localPosition = new Vector3(0, -((transform.position.y - capteruCow.gameObject.transform.position.y) / 2), 0);
        }
        else
        {
            capture = false;
        }
    }
}
