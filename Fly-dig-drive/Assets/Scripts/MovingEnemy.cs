using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{   
    public BezierCurves bezier;
    public GameObject end;
    public GameObject dop;
    public GameObject effect;
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
                sp -= dist*2;
            }
            else
            {
                sp += dist*2;
            }
        }
        Debug.Log("speed:"+point);
        if (point < bezier.bezierPath.Length-1)
        {
            //transform.LookAt(bezier.bezierPath[point]);
            if (Vector3.Distance(transform.position, bezier.bezierPath[point]) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, bezier.bezierPath[point], sp * Time.deltaTime);
            }
            else
            {

                point++;
                
            }
        }
        else
        
        {
            Debug.Log("speed");
            if (Vector3.Distance(transform.position, end.transform.position) > 1f){
                transform.position = Vector3.MoveTowards(transform.position, end.transform.position, speed/2 * Time.deltaTime);
            }
            else
            {
                dop.SetActive(true);
                Instantiate(effect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

}
