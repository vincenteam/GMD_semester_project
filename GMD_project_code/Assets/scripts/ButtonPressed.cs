using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressed : MonoBehaviour
{
    [SerializeField] private GameObject reactingObject;
    
    private void OnCollisionEnter(Collision collision)
    {
        print("collided with button");
        //faire appeler React() de l'interface par le reactingObject (quand je ne serais pas carsick)
    }
}
