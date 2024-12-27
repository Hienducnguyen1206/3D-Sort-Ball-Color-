using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Level
{
    
    public List<Box> boxes = new List<Box>();
    public int HighScore;
    public int MediumScore;
    public int LowScore;

   
}

public class LevelDesign : MonoBehaviour
{
    public static LevelDesign Instance;
    public List<Level> levels = new List<Level>();
 
    private string initialStateJson;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        initialStateJson = JsonUtility.ToJson(new LevelListWrapper { levels = levels });

    }




    // Update is called once per frame
    void Update()
    {

    }

    public void ResetLevelState(int levelIndex)
    {

        
        LevelListWrapper savedState = JsonUtility.FromJson<LevelListWrapper>(initialStateJson);
        levels[levelIndex] = savedState.levels[levelIndex];

    }

    [System.Serializable]
    public class LevelListWrapper
    {
        public List<Level> levels;
    }




}
