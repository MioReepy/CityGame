using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerSpace
{
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _playerAnimator;
        private PlayerController _playerMovement;
        private int _moveXAnimationParametrId;
        private int _moveYAnimationParametrId;
        private int _jumpAnimation;
        private bool _isAim;

        [SerializeField] private float _animationPlayTransition = 0.1f;

        private void Awake()
        {
            _moveXAnimationParametrId = Animator.StringToHash("MovementX");
            _moveYAnimationParametrId = Animator.StringToHash("MovementY");
            _jumpAnimation = Animator.StringToHash("Jumping");
        }

        private void Start()
        {
            _playerAnimator = GetComponent<Animator>();
            _playerMovement = GetComponent<PlayerController>();
        }
        
        private void FixedUpdate()
        {
            _playerAnimator.SetFloat(_moveXAnimationParametrId, _playerMovement.currentBlendAnim.x);
            _playerAnimator.SetFloat(_moveYAnimationParametrId, _playerMovement.currentBlendAnim.y);
            _playerAnimator.SetBool("isAim", _isAim);
        }

        internal void StartAim()
        {
            _isAim = true;
            Debug.Log("true");
        }

        internal void CancelAim()
        {
            _isAim = false;
            Debug.Log("false");
        }

        internal void JumpAnimation()
        {
            _playerAnimator.CrossFade(_jumpAnimation, _animationPlayTransition);
        }
    }
}