using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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

    [SerializeField] private List<SwitchPosition.Placement> _pos = new List<SwitchPosition.Placement>();
    private Dictionary<string, int> _posByName = new Dictionary<string, int>();

    private int _currentPos = 0;

    private int _nbPos;
    // Start is called before the first frame update
    void Start()
    {
        Placement pos1 = new Placement( new Vector3(), new Quaternion(0, 0, 0, 0)); 
        Placement pos2 = new SwitchPosition.Placement(new Vector3(0, 1, -5), new Quaternion(0.239380822f,0,0,0.970925748f));

        List<string> names = new List<string>();
        
        
        _pos.Add(pos1);
        names.Add("1person");
        
        _pos.Add(pos2);
        names.Add("3person");

        string name;
        for (int i=0; i < _pos.Count; i++)
        {
            try
            {
                name = names[i];
                _posByName.Add(name, i);
            }
            catch (ArgumentOutOfRangeException)
            {
                _posByName.Add("cam"+i, i);      
            }
        }
        
        _nbPos = _pos.Count;

        Switch();
    }
    
    void Update() // should be move in player input
    {
        if (Input.GetButtonDown("CameraSwitch"))
        {
            Next();
        }
    }

    public void SwitchTo(string name)
    {
        int ind;
        try
        {
            ind = _posByName[name];
            _currentPos = ind;
            Switch();
        }
        catch (KeyNotFoundException){}
        
    }
    
    public void Next()
    {
        _currentPos++;
        Switch();
    }

    private void Switch()
    {
        var transform1 = transform;
        transform1.localPosition = _pos[_currentPos%_nbPos].p;
        transform1.localRotation = _pos[_currentPos%_nbPos].r;
    }
}
