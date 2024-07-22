using Character;
using Player;
using UnityEngine;

namespace AnimationSpace
{
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _playeranimator;
        private PlayerController _playerMovement;

        private void Start()
        {
            _playeranimator = GetComponent<Animator>();
            _playerMovement = GetComponent<PlayerController>();
        }

        // private void FixedUpdate()
        // {
        //     _playeranimator.SetBool("isWalk", _playerMovement._isWalk);
        // }
    }
}