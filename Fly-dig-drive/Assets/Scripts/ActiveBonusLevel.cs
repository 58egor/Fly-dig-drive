using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBonusLevel : MonoBehaviour
{
    // Start is called before the first frame update
    bool dead = false;
    public bool active = false;
    public GameObject canvas;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            if (other.gameObject.GetComponent<EnemyInfo>().hp <= 0)
            {
                dead = true;
            }
        }
        if ( (other.gameObject.layer == 8 || other.gameObject.layer == 9))
        {
            if (dead || active)
            {
                other.GetComponent<Control>().setCar();
                other.GetComponent<Control>().enabled = false;
                other.GetComponent<BonusGameControl>().enabled = true;
                Camera.main.GetComponent<CameraControl>().SetFase3();
                canvas.SetActive(true);
            }

        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
