using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float stoppingDistance = 0.1f;

    private Camera mainCamera;
    private Vector2 targetPosition;
    private bool isMoving;

    private void Start()
    {
        mainCamera = Camera.main;
        targetPosition = transform.position;
    }

    private void Update()
    {
        // Update target position when right mouse button is clicked
        if (Input.GetMouseButton(1)) // 1 is right mouse button
        {
            targetPosition = GetMouseWorldPosition();
            isMoving = true;
        }

        if (isMoving)
        {
            MoveTowardTarget();
        }
    }

    private Vector2 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -mainCamera.transform.position.z;
        return mainCamera.ScreenToWorldPoint(mousePos);
    }

    private void MoveTowardTarget()
    {
        // Get the current position in 2D
        Vector2 currentPosition = transform.position;

        // Calculate direction to target
        Vector2 direction = (targetPosition - currentPosition).normalized;

        // Calculate distance to target
        float distanceToTarget = Vector2.Distance(currentPosition, targetPosition);

        // Only move if we're not too close to the target
        if (distanceToTarget > stoppingDistance)
        {
            // Move towards target
            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    internal void Initialize(float moveSpeed)
    {
        throw new NotImplementedException();
    }
}