using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPosition : MonoBehaviour
{
    private struct Placement
    {
        public Placement(Vector3 pos, Quaternion rot)
        {
            p = pos;
            r = rot;
        }
        public Vector3 p{ get; }
        public Quaternion r { get; }
    }

    [SerializeField] private List<SwitchPosition.Placement> pos = new List<SwitchPosition.Placement>();

    private int currentPos = 0;

    private int nbPos;
    // Start is called before the first frame update
    void Start()
    {
        pos.Add(new SwitchPosition.Placement( new Vector3(0.247999996f,0.5f,-0.231000006f), new Quaternion(0, 0, 0, 0)));
        pos.Add(new SwitchPosition.Placement(new Vector3(0, 1, -5), new Quaternion(0.239380822f,0,0,0.970925748f)));
        
        nbPos = pos.Count;
        transform.localPosition = pos[currentPos%nbPos].p;
        transform.localRotation = pos[currentPos%nbPos].r;
    }
    
    void Update()
    {
        if (Input.GetButtonDown("CameraSwitch"))
        {
            Next();
        }
    }

    void Next()
    {
        currentPos++;
        transform.localPosition = pos[currentPos%nbPos].p;
        transform.localRotation = pos[currentPos%nbPos].r;
    }
}
