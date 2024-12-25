using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager instance;
    public static Func<bool> CheckGameComplete;

    public List<Box> boxes = new List<Box>();
    public List<GameObject> gameObjects = new List<GameObject>();

    public GameObject selectedBox1;
    public GameObject selectedBox2;

    public LevelDesign levelDesign;
    public bool MoveBallCompleted;
    public bool GamePlaying = false;
    public int currentMaxLevel;

    [SerializeField] int currentLevel;
    

    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
      //  PlayerPrefs.SetInt("NumberLevelUnlocked", 0);
        currentMaxLevel = PlayerPrefs.GetInt("NumberLevelUnlocked", 0);
        MoveBallCompleted = true;
        currentLevel = 0;
        
    }



    // Update is called once per frame
    void Update()
    {
        BoxAction();


            
    }


    public void BoxAction()
    {
        if (Input.GetMouseButtonDown(0) ) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {   
                if(hit.collider.gameObject.transform.parent == null || hit.collider.transform.parent.transform.parent == null)
                {
                    return;
                }
                GameObject clickedObject = hit.collider.gameObject.transform.parent.transform.parent.gameObject;

                if (clickedObject == null) return;
                

                
                if ( clickedObject.gameObject.CompareTag("Box") && MoveBallCompleted ==true  )
                {  
                    if(clickedObject != selectedBox1)
                    {
                        if (selectedBox1 == null)
                        {
                            selectedBox1 = clickedObject;
                          //  Debug.Log(selectedBox1 + " box1 đã được chọn");
                            return;
                        } else {
                            selectedBox2 = clickedObject;
                          //  Debug.Log(selectedBox2 + "box2 đã được chọn");
                            GamePlaying = true;
                            PerformAction(selectedBox1, selectedBox2);
                            MoveBallInBackend(currentLevel,selectedBox1.transform.GetSiblingIndex(),selectedBox2.transform.GetSiblingIndex());
                            CheckLevelCompleted(currentLevel);
                            selectedBox1 = null;
                            selectedBox2 = null;    

                        }
                    }
                    
                }

                
                
            }
        }
    }

    private void PerformAction(GameObject box1, GameObject box2)
    {   
       
        box1.GetComponent<SceneBox>().MoveTopBallTo(box2 as GameObject);

    }

    private void MoveBallInBackend(int Level, int BoxIndex1, int BoxIndex2)
    { int Box2Size = levelDesign.levels[Level].boxes[BoxIndex2].ballList.Count;
        if (levelDesign.levels[Level].boxes[BoxIndex1].ballList.Count > 0 && Box2Size < levelDesign.levels[Level].boxes[BoxIndex2].size) {
           
            Ball b = levelDesign.levels[Level].boxes[BoxIndex1].PopBall();
            levelDesign.levels[Level].boxes[BoxIndex2].PushBall(b);
        }
       
    }

    private void CheckLevelCompleted(int Level) { 
        foreach(Box box in levelDesign.levels[Level].boxes)
        {
            if (!box.CheckAllBalls()) { 
                
                return;
            }
           
        }
        Debug.Log("Level completed");
        if (currentLevel == currentMaxLevel) {
            currentMaxLevel = currentLevel+1;
            PlayerPrefs.SetInt("NumberLevelUnlocked",currentMaxLevel);
            GameObject levelContainer = UIManager.instance.transform.GetComponent<UIManager>().LevelItemContainer.gameObject;
            if (levelContainer.transform.GetChild(currentMaxLevel) != null) {
                levelContainer.transform.GetChild(currentMaxLevel).GetComponent<LevelCard>().BlockImage.gameObject.SetActive(false);
            }
        }
       
        return;
    }

    public void SetCurrnetLevel(int Level) { 
        this.currentLevel = Level;
       
    }

    public void RestartCurrentLevel()
    {
        GamePlaying = false;
        GameLevelList.instance.RestartLevel(currentLevel);
        LevelDesign.Instance.ResetLevelState(currentLevel);
    }

}
