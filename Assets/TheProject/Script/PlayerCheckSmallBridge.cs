using System;
using System.Collections;
using System.Collections.Generic;
using TheProject.Script;
using UnityEngine;

public class PlayerCheckSmallBridge : MonoBehaviour
{
    [SerializeField] private SwitchCharacterController _switchCharacterController;

    public float radius = 0.5f;
    public float distance = 0.5f;

    [ReadOnly] public bool isHasGroundOnBothSide = false;
    CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CheckGroundOnBothSide();

        //check player crouching
        if(Mathf.Approximately(GameManager.Instance.Player.input.y, -1) && GameManager.Instance.Player.isGrounded  && !GameManager.Instance.Player.playerCheckWater.isUnderWater)
        {
            isHasGroundOnBothSide = false;
        }
    }

    void CheckGroundOnBothSide()
    {
        RaycastHit hit;
        //if (Physics.Raycast(transform.position + Vector3.up * 0.5f + Vector3.forward * radius, Vector3.down, out hit, distance + 0.5f))
        //{
        //    isHasGroundOnBothSide = true;
        //}
        //else if (Physics.Raycast(transform.position - Vector3.up * 0.5f + Vector3.forward * radius, Vector3.down, out hit, distance + 0.5f))
        //{
        //    isHasGroundOnBothSide = true;
        //}
        //else
        //    isHasGroundOnBothSide = false;

        if (Physics.SphereCast(transform.position + Vector3.up * 0.5f + Vector3.forward * radius,
                characterController.radius * 0.9f, Vector3.down, out hit, distance + 0.5f))
        {
            isHasGroundOnBothSide = true;
        }
        else if (Physics.SphereCast(transform.position + Vector3.up * 0.5f - Vector3.forward * radius,
                     characterController.radius * 0.9f, Vector3.down, out hit, distance + 0.5f))
        {
            isHasGroundOnBothSide = true;
        }
        else
        {
            isHasGroundOnBothSide = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position + Vector3.up * 0.5f + Vector3.forward * radius, Vector3.down * (distance + 0.5f));
        Gizmos.DrawRay(transform.position + Vector3.up * 0.5f - Vector3.forward * radius, Vector3.down * (distance + 0.5f));
    }
}
