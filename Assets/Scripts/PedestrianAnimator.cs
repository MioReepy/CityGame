using Character;
using UnityEngine;

namespace AnimationSpace
{
    public class PedestrianAnimator : MonoBehaviour
    {
        private Animator _pedestrianAnimator;
        private CharacterNavigatorController _pedestrianMovement;

        private void Start()
        {
            _pedestrianAnimator = GetComponent<Animator>();
            _pedestrianMovement = GetComponent<CharacterNavigatorController>();
        }

        private void FixedUpdate()
        {
            _pedestrianAnimator.SetBool("isWalk", _pedestrianMovement._movementSpeed != 0);
        }
    }
}