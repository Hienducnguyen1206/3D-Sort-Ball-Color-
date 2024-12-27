using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectMenu : MonoBehaviour
{
    public static LevelSelectMenu instance;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMenu()
    {
        gameObject.transform.DOLocalMoveY(0f, 0.5f).SetEase(Ease.InOutSine);
    }
    public void HideMenu()
    {
        gameObject.transform.DOLocalMoveY(-980f, 0.5f).SetEase(Ease.InOutSine);
    }
}
