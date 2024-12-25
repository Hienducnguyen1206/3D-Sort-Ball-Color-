using UnityEngine;

public class Pointer  : MonoBehaviour
{
    void Update()
    {
       
        Vector3 mousePosition = Input.mousePosition;

       
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {
       
            GameObject hitObject = hit.collider.gameObject;        
            if (hit.collider.GetComponent<BoxCollider>() != null)
            {           
                Transform parentTransform = hitObject.transform.parent.transform.parent;             
                if (parentTransform != null)
                {                
                  //  Debug.Log("Object cha là: " + parentTransform.name);
                }
                else
                {
                  //  Debug.Log("Đối tượng không có object cha.");
                }
            }
        }
    }
}
