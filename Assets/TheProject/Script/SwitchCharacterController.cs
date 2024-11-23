using System;
using UnityEngine;
using UnityEngine.Serialization;

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
        [SerializeField] private PlayerCheckSlopeAngle _slopeAngle;
        [SerializeField] private GameObject _girlCharacter;
        [SerializeField] private GameObject _girlCharacterBones;
        [SerializeField] private Avatar _girlAvatar;
        [SerializeField] private GameObject _manCharacter;
        [SerializeField] private GameObject _manCharacterBones;
        [SerializeField] private Avatar _manAvatar;
        [SerializeField] private ParticleSystem _particleSystemBlue;
        [SerializeField] private ParticleSystem _particleSystemFire;
        [SerializeField] private Animator _animator;

        [HideInInspector] public bool IsManCharacter;

        private const string ActiveBonesName = "mixamorig:Hips";

        private Vector2 _vector2;

        private void Awake()
        {
            GetInitialState();
        }

        private void GetInitialState()
        {
            if (_manCharacter.activeInHierarchy)
            {
                IsManCharacter = true;
                ChangeCharacter();
                return;
            }

            ChangeCharacter();
            IsManCharacter = false;
        }

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
            _animator = GetComponent<Animator>();
            _slopeAngle = GetComponent<PlayerCheckSlopeAngle>();
        }

        public void ChangeCharacter()
        {
            if (IsManCharacter)
            {
                IsManCharacter = false;
                ChangeToGirl();
            }
            else
            {
                IsManCharacter = true;
                ChangeToMan();
            }
        }

        private void ChangeToMan()
        {
            ChangeMesh(_manAvatar);
            _particleSystemFire.Play();
            SetManParameters();
        }

        private void ChangeToGirl()
        {
            ChangeMesh(_girlAvatar);
            _particleSystemBlue.Play();
            SetGirlParameters();
        }

        private void SetManParameters()
        {
            _playerController.numberOfAirJump = 0;
            _playerController.wallSlidingSpeed = 2f;
            _playerController.wallStickTime = 1f;
            _playerController.hardFallingDistance = 7;
            _playerController.hangingMoveSpeed = 1;
            _playerController.jetForce = 20;
            _playerController.jetpackDrainTimeOut = 4;
            _playerController.speed = 50;
            _playerController.GroundParameter.moveSpeed = 3;
            _playerController.GroundParameter.runSpeed = 5;
            _playerController.GroundParameter.sneakingSpeed = 2;
            _playerController.GroundParameter.maxJumpHeight = 2;
            _playerController.GroundParameter.minJumpHeight = 1;
            _playerController.GroundParameter.gravity = -40;
            _dragableObjectCheck.dragPushMoveSpeed = 2;
            _ropeDetector.swingForce = 35;
            _vector2.y = -1.6f;
            _playerController.hangingOffset = _vector2;
        }

        private void SetGirlParameters()
        {
            _playerController.numberOfAirJump = 1;
            _playerController.wallSlidingSpeed = 0.0001f;
            _playerController.wallStickTime = 10f;
            _playerController.hardFallingDistance = 3;
            _playerController.hangingMoveSpeed = 3;
            _playerController.jetForce = 35;
            _playerController.jetpackDrainTimeOut = 6;
            _playerController.speed = 50;
            _playerController.GroundParameter.moveSpeed = 4;
            _playerController.GroundParameter.runSpeed = 6;
            _playerController.GroundParameter.sneakingSpeed = 3;
            _playerController.GroundParameter.maxJumpHeight = 3;
            _playerController.GroundParameter.minJumpHeight = 1;
            _playerController.GroundParameter.gravity = -30;
            _dragableObjectCheck.dragPushMoveSpeed = 0;
            _ropeDetector.swingForce = 75;
            _vector2.y = -1.25f;
            _playerController.hangingOffset = _vector2;
        }

        private void ChangeMesh(Avatar avatar)
        {
            SetBonesName();

            _animator.avatar = avatar;

            SetActiveGameObjects();

            _ladder.rootBone = IsManCharacter ? _manCharacterBones.transform : _girlCharacterBones.transform;
            _slopeAngle.rootBone = IsManCharacter ? _manCharacterBones.transform : _girlCharacterBones.transform;
        }

        private void SetActiveGameObjects()
        {
            _manCharacter.SetActive(IsManCharacter);
            _manCharacterBones.SetActive(IsManCharacter);
            _girlCharacter.SetActive(!IsManCharacter);
            _girlCharacterBones.SetActive(!IsManCharacter);
        }

        private void SetBonesName()
        {
            _manCharacterBones.name = IsManCharacter ? ActiveBonesName : string.Empty;
            _girlCharacterBones.name = !IsManCharacter ? ActiveBonesName : string.Empty;
        }
    }
}
