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
        pos.Add(new SwitchPosition.Placement( new Vector3(0.247999996f,0.5f,-0.231000006f), new Quaternion(0, 180, 0, 0)));
        pos.Add(new SwitchPosition.Placement( new Vector3(0.309830904f,2.23320103f,2.87866449f), new Quaternion(0.00801466685f,0.955599427f,-0.293402433f,0.0260875877f)));
        
        nbPos = pos.Count;
        transform.localPosition = pos[currentPos%nbPos].p;
        transform.rotation = pos[currentPos%nbPos].r;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("CameraSwitch"))
        {
            currentPos++;
            transform.localPosition = pos[currentPos%nbPos].p;
            transform.rotation = pos[currentPos%nbPos].r;
        }
    }
}
