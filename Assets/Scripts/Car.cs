using System;
using UnityEngine;

public class Car : MonoBehaviour
{
    private Animator animator;
    private float moveSpeed = 1f;
    [SerializeField] private Path currentPath;
    
    private Vector3 _targetPosition;
    private int _currentWaypoint;

    private void Awake()
    {
        currentPath = GameObject.Find("Path1").GetComponent<Path>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _currentWaypoint = 0;
        _targetPosition = currentPath.GetPosition(_currentWaypoint);
    }

    private void Update()
    {
        // move towards target position
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, moveSpeed * Time.deltaTime);
        
        // when target reached, set new target position
        Vector3 relativeDistance = transform.position - _targetPosition;
        
        animator.SetFloat("speedX", relativeDistance.x * -1);
        animator.SetFloat("speedY", relativeDistance.y * -1);
        
        if (relativeDistance.magnitude < 0.01f)
        {
            if (_currentWaypoint < currentPath.Waypoints.Length - 1)
            {
                _currentWaypoint++;
                _targetPosition = currentPath.GetPosition(_currentWaypoint);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}