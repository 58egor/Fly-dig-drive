using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCheck : MonoBehaviour
{
    Animation animation;
    // Start is called before the first frame update
    public Text text;
    int moneys=0;

    void Start()
    {
        animation = GetComponent<Animation>();
        moneys = SaveLoadOperations.Moneys();
    }

    // Update is called once per frame
    void Update()
    {
        if (moneys != SaveLoadOperations.Moneys())
        {
            animation.Play("moneyAnim");
            text.text = moneys + "";
            moneys = SaveLoadOperations.Moneys();
        }
    }
}
