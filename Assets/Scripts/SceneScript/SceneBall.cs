using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBall : MonoBehaviour
{
    public BallColor color;
    private Rigidbody _rb;
    void Start()
    { 
        _rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision == null || collision.gameObject.transform.position.y > transform.position.y) return;

        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.name == "Bottom")
        {
            GameManager.instance.MoveBallCompleted = true;
            GameManager.instance.CheckLevelCompleted(GameManager.instance.currentLevel);
            GameManager.instance.ResetSelectedBox();
            
        }
    }

    public void MoveBall(float height)
    {   
        _rb.isKinematic = true;

        Sequence mySequence = DOTween.Sequence();      
        mySequence.Append(transform.DOMoveY(height, 0.2f).SetEase(Ease.Linear));
        mySequence.Append(transform.DOLocalMoveX(0, 0.4f).SetEase(Ease.Linear));
        mySequence.OnComplete(() =>
        {
            _rb.isKinematic = false;
        });
    }
}
