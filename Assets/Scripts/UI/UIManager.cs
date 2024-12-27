using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{   
    public static UIManager instance;
    [SerializeField] GameObject LevelItemPrefab;
    public GameObject LevelItemContainer;
    public Button OpenMenuIngameBtn;  
    public Button ContinueCurrentLevelBtn;
    public Button StartNewLevelBtn;
    public GameObject LevelCompletedPopup;
    public GameObject IngameDialog;
    public GameObject CommingSoonPopUp;
    // Start is called before the first frame update


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
    void Start()
    {
        InitLevelItemList();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitLevelItemList()
    {
        int currentMaxLevel = PlayerPrefs.GetInt("NumberLevelUnlocked",0);

        int NumberLevel = GameLevelList.instance.gameObject.transform.childCount;
        for (int i = 0; i < NumberLevel; i++)
        {    
            GameObject LevelItem = Instantiate(LevelItemPrefab);
            if(i<= currentMaxLevel)
            {
                LevelItem.GetComponent<LevelCard>().BlockImage.gameObject.SetActive(false);
                LevelItem.GetComponent<LevelCard>().Unlocked = true;

            }
           
            LevelItem.transform.SetParent(LevelItemContainer.transform,false);
            
            LevelCard levelCard = LevelItem.GetComponent<LevelCard>();
            if (levelCard != null)
            {
                levelCard.SetLevelNum(i);
                levelCard.SetLevelCardImage(i);
                levelCard.levelText.text = "Level" + " " + (i + 1).ToString();
            }
        }
    }

    public void TurnOnInGameBtn()
    {
        OpenMenuIngameBtn.gameObject.SetActive(true);
    }

    



}
