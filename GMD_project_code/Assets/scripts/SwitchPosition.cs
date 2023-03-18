using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPosition : MonoBehaviour
{
    [SerializeField] private List<Vector3> pos = new List<Vector3>();

    private int currentPos = 0;

    private int nbPos;
    // Start is called before the first frame update
    void Start()
    {
        nbPos = pos.Count;
        transform.localPosition = pos[currentPos%nbPos];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("CameraSwitch"))
        {
            currentPos++;
            transform.localPosition = pos[currentPos%nbPos];
        }
    }
}
