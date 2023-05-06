using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private static readonly int Jump = Animator.StringToHash("jump");
    private static readonly int Land = Animator.StringToHash("land");
    private static readonly int MoveSpeed = Animator.StringToHash("moveSpeed");
    
    private SkinManager _skinManager;
    private PlayerInput _playerInput;
    private Rigidbody _rb;
    
    
    [SerializeField] private AudioSource audioSourceLand;
    [SerializeField] private AudioSource audioSourceDeath;

    [SerializeField] private GameObject newSkin;

    [SerializeField] private RotateToTarget rotate;
    void Awake()
    {
        _skinManager = gameObject.GetComponent<SkinManager>();
        
        _playerInput = gameObject.GetComponent<PlayerInput>();
        _rb = gameObject.GetComponent<Rigidbody>();
        CharacterMovement charMove = gameObject.GetComponent<CharacterMovement>();
        Alive lifeActions = gameObject.GetComponent<Alive>();
        _playerInput.OnMenuPressed += delegate
        {
            SceneManager.LoadScene("TitleScreen");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        };

        if (_playerInput is not null)
        {
            if (charMove is not null)
            {
                _playerInput.OnJump += charMove.Jump;
                _playerInput.OnForward += charMove.MoveForward;
                _playerInput.OnBackward+= charMove.MoveBackward;
                _playerInput.OnRight += charMove.MoveRight;
                _playerInput.OnLeft += charMove.MoveLeft;
                _playerInput.OnRotateY += charMove.RotateY;
            }
            
            if (lifeActions is not null)
            {
                _playerInput.OnSuicide += delegate { lifeActions.Die(DamageTypes.Suicide); };
                _playerInput.OnSuicide += audioSourceDeath.Play; // move in the appropriate death code in playerlifeactions
            }

            if (_skinManager != null)
            {
                _playerInput.OnChangeSkin += delegate {_skinManager.ChangeSkin(newSkin);};
            }
            
            _playerInput.OnReset += delegate { TransitionManager.TransitionInstance.Transition(SceneManager.GetActiveScene().name);};
        }

        
        HeadMovement headMove = gameObject.GetComponentInChildren<HeadMovement>();
        rotate = GameObject.Find("CM vcam1").GetComponent<RotateToTarget>();
        if (rotate is not null && _playerInput is not null)
        {
            _playerInput.OnRotateX += rotate.RotateX;
        }
        
    }

    private void Start()
    {
        GroundDetector groundDetector = gameObject.GetComponentInChildren<GroundDetector>();
        
        if (groundDetector is not null && _skinManager is not null)
        {
            groundDetector.OnLand += delegate
            {
                _skinManager.AnimatorInstance.SetTrigger(Land);
                audioSourceLand.Play();
            };
            groundDetector.OnLeaveGround += delegate { 
                _skinManager.AnimatorInstance.SetTrigger(Jump);
                _skinManager.AnimatorInstance.ResetTrigger(Land);
            };
        }
    }

    private void FixedUpdate()
    {
        _skinManager.AnimatorInstance.SetFloat(MoveSpeed, _rb.velocity.magnitude);
    }
}