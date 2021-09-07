using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public int time;
    public Text text;
    public Text text2;
    public GameObject canv;
    public bool win = false;
    int money;
    int newM;
    float count = 0.1f;
    float counM=0;
    int level;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = time;
        money = SaveLoadOperations.Moneys();
        if(text!=null)
        text.text = money+"";
        level = SaveLoadOperations.NumberLevel();
        level++;
        if (text2 != null)
        {
            text2.text = text2.text + " " + (level);
        }
            newM = SaveLoadOperations.CollecetdMoneys()+money;
        Debug.Log("newM:"+newM);

    }
    public void buttonStart()
    {
        canv.SetActive(true);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void buttonRestart()
    {
        SaveLoadOperations.SaveMoneys(money);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void buttonNextLevel()
    {
        SaveLoadOperations.SaveMoneys(money);
        int i = SaveLoadOperations.SavedLevel();
        i++;
        if (i > 2)
        {
            i = 0;
        }
        SaveLoadOperations.SaveGame(i,level);
        SceneManager.LoadScene(i);
    }
    private void Update()
    {
        if (win)
        {
            if (money <newM && counM<=0)
            {
                if(money>=newM)
                {
                    money = newM;
                    text.text = money + "";
                }
                else
                {
                    money++;
                    counM = count;
                    text.text = money + "";
                }
            }
            else
            {
                counM -= Time.deltaTime;
            }
        }
    }
}
