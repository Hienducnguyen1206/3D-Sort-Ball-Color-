using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{   
    public static MainMenu instance;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void ShowMenu()
    {
        gameObject.transform.DOMoveX(960, 0.75f).SetEase(Ease.InOutSine);
    }
    public void HideMenu()
    {
        gameObject.transform.DOMoveX(500, 0.75f).SetEase(Ease.InOutSine);
    }
}
