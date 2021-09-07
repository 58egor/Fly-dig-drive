using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Load : MonoBehaviour
{
    public GameObject ui;
    public int sceneNumber = 0;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            ui.SetActive(true);
            Camera.main.GetComponent<CameraControl>().stop();
        }
    }
}
