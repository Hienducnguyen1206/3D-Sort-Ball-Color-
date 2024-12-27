using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelList : MonoBehaviour
{
   public static GameLevelList instance;
   public List<GameObject> levelList = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
          
        }
        else
        {
            Destroy(gameObject);
        }
    }

   

    public void RestartLevel(int level)
    {
        Destroy(transform.GetChild(level).gameObject);
        CreateLevelObject(level);
    }
    public void CreateLevelObject(int level)
    {   
        GameObject newLevelObj = GameObject.Instantiate(levelList[level],gameObject.transform.position,Quaternion.identity);
        newLevelObj.gameObject.SetActive(true);
        newLevelObj.transform.parent = transform;
        newLevelObj.transform.SetSiblingIndex(level);
    }


    public void DisableAllChild()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }


}
