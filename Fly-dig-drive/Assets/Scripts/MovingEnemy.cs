using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{   
    public BezierCurves bezier;
    public float speed = 20f;
    public float distance = 30;
    public GameObject player;
    public GameObject[] objects;
    public TrailRenderer[] dirts;
    int point = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        float sp=speed;
        if (player != null)
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist > distance)
            {
                sp -= dist;
            }
            else
            {
                sp += dist;
            }
        }
        Debug.Log("speed:"+sp);
        if (point < bezier.bezierPath.Length)
        {
            if (Vector3.Distance(transform.position, bezier.bezierPath[point]) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, bezier.bezierPath[point], sp * Time.deltaTime);
            }
            else
            {

                point++;
                transform.LookAt(bezier.bezierPath[point]);
            }
        }
       
        if (transform.position.y > 0.1f)
        {
            objects[0].SetActive(true);
            objects[1].SetActive(false);
            objects[2].SetActive(false);
        }
        if (transform.position.y < -0.1f)
        {
            objects[0].SetActive(false);
            objects[1].SetActive(false);
            objects[2].SetActive(true);
        }
        if (transform.position.y > -0.1f && transform.position.y<0.1f)
        {
            objects[0].SetActive(false);
            objects[1].SetActive(true);
            objects[2].SetActive(false);
        }
        if (transform.position.y < -1f)
        {
            if (!dirts[0].enabled)
            {
                for (int i = 0; i < dirts.Length; i++)
                {
                    dirts[i].enabled = true;
                }
            }
        }
        else
        {
            if (dirts[0].enabled)
            {
                for (int i = 0; i < dirts.Length; i++)
                {
                    dirts[i].enabled = false;
                }
            }
        }
    }

}
