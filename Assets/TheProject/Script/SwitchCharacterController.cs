using UnityEngine;

namespace TheProject.Script
{
    public class SwitchCharacterController : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private MeleeAttack _meleeAttack;
        [SerializeField] private RangeAttack _rangeAttack;
        [SerializeField] private PlayerRopeDetecter _ropeDetector;
        [SerializeField] private PlayerCheckLadderZone _ladder;
        [SerializeField] private PlayerOverrideParametersChecker _overrideParametersChecker;
        [SerializeField] private PlayerCheckSmallBridge _smallBridge;
        [SerializeField] private PlayerCheckDragableObject _dragableObjectCheck;
        [SerializeField] private GameObject _girlCharacter;
        [SerializeField] private GameObject _girlCharacterBones;
        [SerializeField] private GameObject _manCharacter;
        [SerializeField] private GameObject _manCharacterBones;
        [SerializeField] private ParticleSystem _particleSystemBlue;
        [SerializeField] private ParticleSystem _particleSystemFire;
        [SerializeField] private ParticleSystem _particleSystemFire;
        [SerializeField] private Animator _animator;

        private void Reset()
        {
            _playerController = GetComponent<PlayerController>();
            _meleeAttack = GetComponent<MeleeAttack>();
            _rangeAttack = GetComponent<RangeAttack>();
            _ropeDetector = GetComponent<PlayerRopeDetecter>();
            _ladder = GetComponent<PlayerCheckLadderZone>();
            _overrideParametersChecker = GetComponent<PlayerOverrideParametersChecker>();
            _smallBridge = GetComponent<PlayerCheckSmallBridge>();
            _dragableObjectCheck = GetComponent<PlayerCheckDragableObject>();
            _particleSystem = GetComponent<ParticleSystem>();
            _animator = GetComponent<Animator>();
        }
    }
}
