using UnityEngine;
using TMPro;

public class LaserCheck : MonoBehaviour
{

    int rayDistance = 10;
    public LayerMask controllerLayer;
    public TextMeshPro outText;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position,transform.forward,out RaycastHit hit, rayDistance, controllerLayer))
        {
            outText.text = "Failed! Hit +" + hit.transform.name;
        }
    }
}