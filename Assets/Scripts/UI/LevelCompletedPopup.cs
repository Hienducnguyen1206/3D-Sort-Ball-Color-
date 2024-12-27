using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletedPopup : MonoBehaviour
{
  public  List<ParticleSystem> _ConfettiEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        foreach(ParticleSystem p in _ConfettiEffect)
        {
            p.Play();
        }
    }
}
