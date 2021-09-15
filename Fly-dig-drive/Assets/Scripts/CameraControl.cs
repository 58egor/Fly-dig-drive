using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    AudioManager audio;
    Camera cam;
    public GameObject player;
    public float x;
    public float minY;
    public float maxY;
    bool pause=false;
    public GameObject start;
    public GameObject end;
    bool fase1 = false;
    bool fase2 = false;
    bool fase3 = false;
    int music;
    // Start is called before the first frame update
    void Start()
    {
        audio = gameObject.GetComponent<AudioManager>();
        cam = Camera.main;
        music = Random.Range(1, 5);
        audio.Play("menu" + music);
    }

    // Update is called once per frame
    void Update()
    {
        if (!audio.isPlaying("start"))
        {
            if(!audio.isPlaying("race"+music))
                audio.Play("race" + music);
        }
        if (player != null && !pause && fase2)
        {
            Debug.Log("cam");
            cam.transform.position = new Vector3(player.transform.position.x + x, player.transform.position.y + 3, cam.transform.position.z);
        }
        if (player == null)
        {
            audio.Stop("race" + music);
        }
    }
    private void FixedUpdate()
    {
        if (fase1)
        {
            Vector3 vec=new Vector3(player.transform.position.x + x, player.transform.position.y + 3, start.transform.position.z);
            if (Vector3.Distance(transform.position, vec) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, vec, 30 * Time.fixedDeltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, start.transform.rotation, Time.fixedDeltaTime * 10);
            }
            else
            {
                fase2 = true;
                fase1 = false;
            }
        }
        if (fase3)
        {
            if (Vector3.Distance(transform.position, end.transform.position) >1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, end.transform.position, 25 * Time.fixedDeltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, end.transform.rotation, Time.fixedDeltaTime * 10);
            }
            else
            {
                fase3 = false;
            }
        }
    }
    public void stop()
    {
        pause = true;
    }
    //void OnDrawGizmosSelected()
    //{
    //    Camera camera = GetComponent<Camera>();
    //    Vector3 p = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(p, 0.1F);
    //}
    public void SetFase1()
    {
        audio.Stop("menu" + music);
        audio.Play("start");
        fase1 = true;
    }
    public void SetFase3()
    {
        fase1 = false;
        fase2 = false;
        fase3 = true;
    }
}
