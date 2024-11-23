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

        private void Awake()
        {
            GetInitialState();
        }

        private void GetInitialState()
        {
            if (_manCharacter.activeInHierarchy)
            {
                IsManCharacter = true;
                return;
            }

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
        }

        private void ChangeToGirl()
        {
            ChangeMesh(_girlAvatar);
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
