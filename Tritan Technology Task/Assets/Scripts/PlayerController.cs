using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private IRayProvider _rayProvider;

    public static UnityAction<bool> onStartWaking;

    private bool isWalking;
    private bool isCollectableSelected;

    private ICollectable collectable;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rayProvider = GetComponent<IRayProvider>();

        UIManager.onClickSpeedBtn += UpdatePlayerSpeed;
    }

    private void OnDestroy() => UIManager.onClickSpeedBtn -= UpdatePlayerSpeed;

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _rayProvider.CreateRay();
            if (!Physics.Raycast(ray, out RaycastHit hit)) return;

            _agent.SetDestination(hit.point);
            isWalking = true;
            onStartWaking?.Invoke(isWalking);

            Debug.Log(hit.collider.gameObject);

            collectable?.OnDeSelect();
            collectable = hit.transform.GetComponent<ICollectable>();
            collectable?.OnSelect();
        }
        else
        {
            if (!isWalking || !(_agent.remainingDistance <= 0)) return;

            isWalking = false;
            onStartWaking?.Invoke(isWalking);
            collectable?.Collect();
        }
    }

    private void UpdatePlayerSpeed(float value)
    {
        float playerSpeed = _agent.speed;
        playerSpeed += value;
        playerSpeed = Mathf.Clamp(playerSpeed, 2, 7);
        _agent.speed = playerSpeed;
    }
}