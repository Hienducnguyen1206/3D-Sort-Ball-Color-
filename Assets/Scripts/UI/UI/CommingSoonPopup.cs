using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommingSoonPopup : MonoBehaviour
{
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
        gameObject.transform.DOLocalMoveY(0f, 0.5f).SetEase(Ease.InOutSine);
    }
    public void HideMenu()
    {
        gameObject.transform.DOLocalMoveY(-645f, 0.5f).SetEase(Ease.InOutSine);
    }
}
