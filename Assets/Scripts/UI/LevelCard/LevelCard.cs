using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelCard : MonoBehaviour
{
    [SerializeField] Button levelBtn;
    [SerializeField] Image LevelImage;
    [SerializeField] int LevelNum;
    public bool Unlocked;
    public bool Playing;
    public Image BlockImage;
    public TextMeshProUGUI levelText;


    // Start is called before the first frame update
    void Awake()
    {   
        
        
        levelBtn.onClick.AddListener(() =>
        {  
            


            if (GameManager.instance.currentLevel == LevelNum ) {

                if (Playing == false) { 
                    StartNewLevel();
                }
                else
                {
                    UIManager.instance.IngameDialog.gameObject.SetActive(true);
                    UIManager.instance.StartNewLevelBtn.onClick.RemoveAllListeners();
                    UIManager.instance.ContinueCurrentLevelBtn.onClick.RemoveAllListeners();
                    UIManager.instance.StartNewLevelBtn.onClick.AddListener(StartNewLevel);
                    UIManager.instance.ContinueCurrentLevelBtn.onClick.AddListener(ContinueCurrentLevel);
                }

               
            }
            else 
            {
                StartNewLevel();

            }
           

        });

        
        
    } 



    // Update is called once per frame
    void Update()
    {
       
    }

  

    public void SetLevelNum(int levelNum)
    {
        this.LevelNum = levelNum;
    }

    public void SetLevelCardImage(int levelNum)
    {
        string path = "Assets/GameAssets/LevelImage/" + ("Level"+ levelNum.ToString()) + ".png";
        Sprite levelSprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
        LevelImage.sprite = levelSprite;
    }

    public void StartNewLevel()
    {  
        Props.instance.gameObject.SetActive(false);
        GameManager.instance.SetCurrnetLevel(LevelNum);
        GameManager.instance.RestartCurrentLevel();
        SecondCamera.instance.gameObject.SetActive(false);
        LevelSelectMenu.instance.gameObject.SetActive(false);
        LevelSelectMenu.instance.HideMenu();
        UIManager.instance.TurnOnInGameBtn();
        GameLevelList.instance.DisableAllChild();
        GameLevelList.instance.gameObject.transform.GetChild(LevelNum).gameObject.SetActive(true);
        UIManager.instance.IngameDialog.gameObject.SetActive(false);
        GameManager.instance.ResetSelectedBox();
    }

    public void ContinueCurrentLevel()
    {
        Props.instance.gameObject.SetActive(false);
        GameManager.instance.SetCurrnetLevel(LevelNum);
        SecondCamera.instance.gameObject.SetActive(false);
        LevelSelectMenu.instance.gameObject.SetActive(false);
        LevelSelectMenu.instance.HideMenu();
        UIManager.instance.TurnOnInGameBtn();
        GameLevelList.instance.DisableAllChild();
        GameLevelList.instance.gameObject.transform.GetChild(LevelNum).gameObject.SetActive(true);
        UIManager.instance.IngameDialog.gameObject.SetActive(false);
        GameManager.instance.ResetSelectedBox();

    }
}
