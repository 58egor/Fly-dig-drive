using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveLoadOperations
{
    public static void SaveGame(int level,int number)
    {
        PlayerPrefs.SetInt("Savedlevel", level);
        PlayerPrefs.SetInt("Numberlevel", number);
    }
    public static void SaveMoneys(int moneys)
    {
        PlayerPrefs.SetInt("SavedMoney", moneys);
    }
    public static void SaveCollectedMoneys(int moneys)
    {
        int m = moneys;
        if (PlayerPrefs.HasKey("SavedCollectedMoney"))
        {
            m+=PlayerPrefs.GetInt("SavedCollectedMoney");
        }
        PlayerPrefs.SetInt("SavedCollectedMoney", m);
    }
    public static int SavedLevel()
    {
        if (PlayerPrefs.HasKey("Savedlevel"))
        {
            return PlayerPrefs.GetInt("Savedlevel");
        }
        else
        {
            return 0;
        }
    }
    public static int NumberLevel()
    {
        if (PlayerPrefs.HasKey("Numberlevel"))
        {
            return PlayerPrefs.GetInt("Numberlevel");
        }
        else
        {
            return 0;
        }
    }
    public static int Moneys()
    {
        if (PlayerPrefs.HasKey("SavedMoney"))
        {
            return PlayerPrefs.GetInt("SavedMoney");
        }
        else
        {
            return 0;
        }
    }
    public static int CollecetdMoneys()
    {
        if (PlayerPrefs.HasKey("SavedCollectedMoney"))
        {
            int m= PlayerPrefs.GetInt("SavedCollectedMoney");
            PlayerPrefs.SetInt("SavedCollectedMoney", 0);
            return m;
            
        }
        else
        {
            return 0;
        }
    }
}
