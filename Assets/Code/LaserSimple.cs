using UnityEngine;
using TMPro;
using System.Collections;

public class LaserSimple : MonoBehaviour
{

    int rayDistance = 100;
    public LayerMask controllerLayer;
    ManagerSimple managerSimple;
    bool canFire = true;

    void Start()
    {
      managerSimple = FindAnyObjectByType<ManagerSimple>();  
    }

    void FixedUpdate()
    {
        if (canFire && Physics.Raycast(transform.position,transform.forward,out RaycastHit hit, rayDistance, controllerLayer))
        {
            
            canFire = false;
            StartCoroutine(FireReset());
            managerSimple.UpdateScore(-10);
        }
    }
    
    public IEnumerator FireReset(){
        yield return new WaitForSeconds(2);
        canFire = true;
    }
}