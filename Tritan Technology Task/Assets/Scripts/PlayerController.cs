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
    public static UnityAction<float, Vector2> onChangePlayerSpeed;

    private bool _isWalking;

    private ICollectable _collectable;

    private readonly Vector2 _speedClampValue = new Vector2(2, 7);
    private float _agentRemainingDistance;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rayProvider = GetComponent<IRayProvider>();

        UIManager.onClickSpeedBtn += UpdatePlayerSpeed;
        onChangePlayerSpeed?.Invoke(_agent.speed, _speedClampValue);
    }

    private void OnDestroy() => UIManager.onClickSpeedBtn -= UpdatePlayerSpeed;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverUIObject()) return;
            Ray ray = _rayProvider.CreateRay();
            if (!Physics.Raycast(ray, out RaycastHit hit)) return;

            _agent.SetDestination(hit.point);
            _isWalking = true;
            onStartWaking?.Invoke(_isWalking);

            _collectable?.OnDeSelect();
            _collectable = hit.transform.GetComponent<ICollectable>();
            _collectable?.OnSelect();
        }
        else
        {
            _agentRemainingDistance = _agent.remainingDistance;
            if (!_isWalking || !(_agentRemainingDistance <= _agent.stoppingDistance)) return;

            _isWalking = false;
            onStartWaking?.Invoke(_isWalking);
            _collectable?.Collect();
        }
    }

    private void UpdatePlayerSpeed(float value)
    {
        float playerSpeed = _agent.speed;
        playerSpeed += value;
        playerSpeed = Mathf.Clamp(playerSpeed, _speedClampValue.x, _speedClampValue.y);
        _agent.speed = playerSpeed;
        onChangePlayerSpeed?.Invoke(_agent.speed, _speedClampValue);
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}