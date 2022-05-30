using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("External References")] 
    [SerializeField] private SwerveInput swerveInput;
    [SerializeField] private float maxSwerveSpeed= 1f;
    [SerializeField] private float swerveSpeed = 0.5f;

    [Header("Player configs")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float rotateSpeed = 15f;

    [Header("Player References")] 
    [SerializeField] private Transform playerBody;

    private float yRot;
    
    private Transform playerTransform;
    private float moveAmount;
    
    private void Start()
    {
        playerTransform = transform;
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();
        HandleRotation();
    }

    private void HandleInput()
    {
        moveAmount = Time.deltaTime * swerveSpeed * swerveInput.MoveFactorX;
        moveAmount = Mathf.Clamp(moveAmount, -maxSwerveSpeed, maxSwerveSpeed);
    }
    
    private void HandleMovement()
    {
        playerTransform.Translate(moveAmount, 0f, moveSpeed * Time.deltaTime);
    }

    void HandleRotation()
    {
       
    }
}

    