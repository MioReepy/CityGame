using Character;
using UnityEngine;

namespace AnimationSpace
{
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _playeranimator;
        private CCMovement _playerMovement;

        private void Start()
        {
            _playeranimator = GetComponent<Animator>();
            _playerMovement = GetComponent<CCMovement>();
        }

        private void FixedUpdate()
        {
            _playeranimator.SetBool("isWalk", _playerMovement._isWalk);
        }
    }
}