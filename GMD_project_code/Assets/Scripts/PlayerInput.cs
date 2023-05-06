using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;
    private float _sensitivityY;
    private float _sensitivityX;

    private float _yAccumulator;
    private float _xAccumulator;

    [SerializeField] private float snappiness = 10.0f;

    private float _forwardInput;
    private float _rgtLftInput;
    private float _mouseY;
    private float _mouseX;
    
    private bool _jumpBtnDown = false;
    private bool _jumpBtnUp = false;

    private bool _deathInput;
    private bool _changeSkinInput;

    private string _forwardKeyboardType;
    private string _rightleftKeyboardType;

    public delegate void ActionsDelegate();
    private ActionsDelegate _jump;
    public ActionsDelegate OnJump
    {
        get => _jump;
        set => _jump = value;
    }
    
    private ActionsDelegate _forward;
    public ActionsDelegate OnForward
    {
        get => _forward;
        set => _forward = value;
    }
    
    private ActionsDelegate _backward;
    public ActionsDelegate OnBackward
    {
        get => _backward;
        set => _backward = value;
    }
    
    private ActionsDelegate _right;
    public ActionsDelegate OnRight
    {
        get => _right;
        set => _right = value;
    }

    private ActionsDelegate _left;
    public ActionsDelegate OnLeft
    {
        get => _left;
        set => _left = value;
    }
    
    private ActionsDelegate _suicide;
    public ActionsDelegate OnSuicide
    {
        get => _suicide;
        set => _suicide = value;
    }
    
    private ActionsDelegate _changeSkin;
    public ActionsDelegate OnChangeSkin
    {
        get => _changeSkin;
        set => _changeSkin = value;
    }
    
    public delegate void RotationDelegate(float amount);
    private RotationDelegate _rotateY;
    public RotationDelegate OnRotateY
    {
        get => _rotateY;
        set => _rotateY = value;
    }
    
    private RotationDelegate _rotateX;
    public RotationDelegate OnRotateX
    {
        get => _rotateX;
        set => _rotateX = value;
    }

    public delegate void QuitDelegate();
    private QuitDelegate _quitDelegate;
    public QuitDelegate OnMenuPressed
    {
        get => _quitDelegate;
        set => _quitDelegate = value;
    }
    
    private ActionsDelegate _reset;
    public ActionsDelegate OnReset
    {
        get => _reset;
        set => _reset = value;
    }

    private void Awake()
    {
        Load();
    }

    private void Load()
    {
        _forwardKeyboardType = PlayerPrefs.GetString("Forward");
        _rightleftKeyboardType = PlayerPrefs.GetString("RightLeft");
    }
    
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");
        
        _rgtLftInput = Input.GetAxis(_rightleftKeyboardType);

        _forwardInput = Input.GetAxis(_forwardKeyboardType);

        if (Input.GetButtonDown("EscapePressed")) _quitDelegate();
        if (Input.GetButtonDown("Reset")) _reset();

        if (Input.GetButtonDown("Jump")) _jumpBtnDown = true;
        if (Input.GetButtonUp("Jump")) _jumpBtnUp = true;

        _deathInput = Input.GetButtonDown("Death") || _deathInput;
        _changeSkinInput = Input.GetButtonDown("ChangeSkin") || _changeSkinInput;

        _sensitivityX = mouseSensitivity/4;
        _sensitivityY = mouseSensitivity;
    }
    
    void FixedUpdate()
    {
        if (_deathInput)
        {
            _suicide();
            _deathInput = false;
        }

        if (_changeSkinInput)
        {
            _changeSkin();
            _changeSkinInput = false;
        }
        
        if (_jumpBtnDown)
        {
            _jump();
        }
        if (_jumpBtnDown && _jumpBtnUp)
        {
            _jumpBtnDown = false;
            _jumpBtnUp = false;
        }

        
        if (_forwardInput > 0)
        {
            _forward();
        }else if (_forwardInput < 0)
        {
            _backward();
        }
        
        if (_rgtLftInput > 0)
        {
            _right();
        }else if (_rgtLftInput < 0)
        {
            _left();
        }
        
        // rotation
        _xAccumulator = Mathf.Lerp( _xAccumulator, _mouseX, snappiness * Time.deltaTime);
        _rotateY(_xAccumulator*_sensitivityX);
        
        _yAccumulator = Mathf.Lerp( _yAccumulator, _mouseY, snappiness * Time.deltaTime);
        _rotateX(_yAccumulator*_sensitivityY);
    }
}
