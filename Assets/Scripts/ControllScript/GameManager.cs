using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public int currentMaxLevel;
    public int currentLevel;
    

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
       
       // PlayerPrefs.SetInt("NumberLevelUnlocked", 0);
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
            if (EventSystem.current.IsPointerOverGameObject())
            {
               
                return; 
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && MoveBallCompleted == true)
            {   
                if(hit.collider.gameObject.transform.parent == null || hit.collider.transform.parent.transform.parent == null)
                {
                    return;
                }
                GameObject clickedObject = hit.collider.gameObject.transform.parent.transform.parent.gameObject;

            
                if ( clickedObject.gameObject.CompareTag("Box") )
                {
                         AudioManager.instance.PlayButtonSFX();
                        if (selectedBox1 == null && clickedObject.transform.childCount > 1 )
                        {
                           selectedBox1 = clickedObject;
                        selectedBox1.gameObject.transform.GetChild(0).gameObject.transform.GetChild(5).gameObject.GetComponent<TextMeshPro>().text = "1";
                        return;
                        } else if (selectedBox1 != null && clickedObject != selectedBox1)
                        {
                            selectedBox2 = clickedObject;
                            selectedBox2.gameObject.transform.GetChild(0).gameObject.transform.GetChild(5).gameObject.GetComponent<TextMeshPro>().text = "2";
                        GameObject levelContainer = UIManager.instance.transform.GetComponent<UIManager>().LevelItemContainer.gameObject;
                            levelContainer.transform.GetChild(currentLevel).GetComponent<LevelCard>().Playing = true;
                            
                         
                            PerformAction(selectedBox1, selectedBox2);
                            MoveBallInBackend(currentLevel,selectedBox1.transform.GetSiblingIndex(),selectedBox2.transform.GetSiblingIndex());

                        }
                        else if( selectedBox1 == clickedObject ) {
                        
                           ResetSelectedBox();
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
        else
        {
            ResetSelectedBox();
        }
       
    }

    public void CheckLevelCompleted(int Level) { 
        foreach(Box box in levelDesign.levels[Level].boxes)
        {
            if (!box.CheckAllBalls()) { 
                
                return;
            }
           
        }


        
        UIManager.instance.LevelCompletedPopup.gameObject.SetActive(true);

        GameObject levelContainer = UIManager.instance.transform.GetComponent<UIManager>().LevelItemContainer.gameObject;
        levelContainer.transform.GetChild(currentLevel).GetComponent<LevelCard>().Playing = false;
        if (currentLevel == currentMaxLevel) {
            currentMaxLevel = currentLevel+1;
            PlayerPrefs.SetInt("NumberLevelUnlocked",currentMaxLevel);
           

          

            if (levelContainer.transform.GetChild(currentMaxLevel) != null) {
                levelContainer.transform.GetChild(currentMaxLevel).GetComponent<LevelCard>().BlockImage.gameObject.SetActive(false);
            }
        }
       
        return;
    }

    public void SetCurrnetLevel(int Level) { 
        this.currentLevel = Level;
       
    }

    public void ResetSelectedBox()
    {
        if (selectedBox1 != null && selectedBox2 != null)
        {
            selectedBox1.gameObject.transform.GetChild(0).gameObject.transform.GetChild(5).gameObject.GetComponent<TextMeshPro>().text = "";
            selectedBox2.gameObject.transform.GetChild(0).gameObject.transform.GetChild(5).gameObject.GetComponent<TextMeshPro>().text = "";
            selectedBox1 = null;
            selectedBox2 = null;
        }else if(selectedBox1 != null)
        {
            selectedBox1.gameObject.transform.GetChild(0).gameObject.transform.GetChild(5).gameObject.GetComponent<TextMeshPro>().text = "";
            selectedBox1 = null;
        }
    }

    public void RestartCurrentLevel()
    {
       ResetSelectedBox();
        GameLevelList.instance.RestartLevel(currentLevel);
        LevelDesign.Instance.ResetLevelState(currentLevel);
    }

    public void NextLevel()
    {
        ResetSelectedBox();
        currentLevel += 1;
        if (currentLevel < GameLevelList.instance.gameObject.transform.childCount)
        {
            GameLevelList.instance.DisableAllChild();
            GameLevelList.instance.gameObject.transform.GetChild(currentLevel).gameObject.SetActive(true);
        }

    }

}
