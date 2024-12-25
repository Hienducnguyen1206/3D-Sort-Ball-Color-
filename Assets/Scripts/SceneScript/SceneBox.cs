using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBox : MonoBehaviour
{
    public int size;
    public float height;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveTopBallTo(GameObject newParent)
    {
        if (gameObject.transform.childCount > 1 && newParent.transform.childCount <= newParent.GetComponent<SceneBox>().size)
        { 
            GameManager.instance.MoveBallCompleted= false;
            GameObject ball = gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject;
            ball.transform.SetParent(newParent.transform, true);
            ball.GetComponent<SceneBall>().MoveBall(Mathf.Max(height,newParent.transform.GetComponent<SceneBox>().height));
        }
        
    }
}
