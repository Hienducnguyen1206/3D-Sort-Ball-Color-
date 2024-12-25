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
        if (collision == null) return;

        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.name == "Bottom")
        {
            GameManager.instance.MoveBallCompleted = true;
        }
    }

    public void MoveBall(float height)
    {   
        _rb.isKinematic = true;

        Sequence mySequence = DOTween.Sequence();      
        mySequence.Append(transform.DOMoveY(height, 0.5f).SetEase(Ease.Linear));
        mySequence.Append(transform.DOLocalMoveX(0, 0.5f).SetEase(Ease.Linear));
        mySequence.OnComplete(() =>
        {
            _rb.isKinematic = false;
        });
    }
}
