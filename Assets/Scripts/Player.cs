using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour,  IParentable
{

    public class OnSelectedCounterChangedArgs : EventArgs {
        public BaseCounter SelectedCounter { get; set; }
    }

    public event EventHandler OnPickupObject;

    public static Player Instance {get; private set; }
    
    public event EventHandler<OnSelectedCounterChangedArgs> OnSelectedCounterChanged;
    
    [SerializeField]
    private LayerMask counterLayerMask;
    
    [SerializeField]
    private GameInput gameInput;
    
    [SerializeField]
    private float speed = 5;

    [SerializeField]
    private float rotateSpeed = 10;

    [SerializeField]
    private Transform kitchenObjectLocation;
    
    private KitchenObject kitchenObject;
    
    private Vector3 lastDirection = Vector3.zero;
    
    private float playerHeight = 2f;
    
    private float playerRadius = 0.7f;
    
    private bool isWalking;

    private BaseCounter selectedCounter;
    
    private void Awake() {
        if (Instance != null){
            throw new Exception("Player not null!");
        } else{
            Instance = this;
        }
    }


    void Start() {
        gameInput.OnInteractHandler += GameInputOnInteractHandler;
        gameInput.OnAlternateInteractHandler += GameInputOnAlternateInteractHandler;
    }


    private void GameInputOnInteractHandler(object sender, EventArgs e) {
        if (selectedCounter != null){
            selectedCounter.Interact(this);
        }
    }
    
    private void GameInputOnAlternateInteractHandler(object sender, EventArgs e) {
        if (selectedCounter != null){
            selectedCounter.AlternateInteract(this);
        }
    }    

    void Update() {

        Vector2 input = gameInput.GetNormalizedInput();
        Vector3 direction = new Vector3(input.x, 0, input.y);
        
        if (direction != Vector3.zero){
            lastDirection = direction;
        }
        
        HandleSelectedCounter(lastDirection);
        HandleMove(direction);

    }

    private void HandleSelectedCounter(Vector3 direction) {
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, direction, out RaycastHit raycastHit, interactDistance, counterLayerMask)){
            if (raycastHit.transform.TryGetComponent(out BaseCounter c)){
                SetSelectedCounter(c);
            } else{
                SetSelectedCounter(null);
            }
        } else{
            SetSelectedCounter(null);
        }
    }
    private void SetSelectedCounter(BaseCounter bc) {
        OnSelectedCounterChangedArgs e = new OnSelectedCounterChangedArgs();
        selectedCounter = bc;
        OnSelectedCounterChanged?.Invoke(null, new OnSelectedCounterChangedArgs {
            SelectedCounter = selectedCounter
        });
    }

    private void HandleMove(Vector3 direction) {
        if (GameManager.Instance.GetState() != GameManager.GameState.RUNNING){
            return;
        }        
        isWalking = direction != Vector3.zero;        
        if (direction != Vector3.zero){
            float moveDistance = Time.deltaTime * speed;
            Boolean canMove = CanMove(direction, moveDistance);

            if (canMove){
                Move(moveDistance, direction, direction);
            } else{
                Vector3 xDirection = new Vector3(direction.x, 0, 0).normalized;
                canMove = CanMove(xDirection, moveDistance) && (direction.x > 0.5 || direction.x < -0.5);
                if (canMove){
                    Move(moveDistance, xDirection, direction);
                } else{
                    Vector3 zDirection = new Vector3(0, 0, direction.z).normalized;
                    canMove = CanMove(zDirection, moveDistance) && (direction.z > 0.5 || direction.z < -0.5);
                    if (canMove){
                        Move(moveDistance, zDirection, direction);
                    }
                }
            }
        } 
    }

    private bool CanMove(Vector3 direction, float distance) {
        RaycastHit r;
        return !Physics.CapsuleCast(transform.position , transform.position + Vector3.up * playerHeight,
            playerRadius, direction, out r, distance);
    }
    
    private void Move(float distance, Vector3 moveDirection, Vector3 rotateDirection) {
        transform.position += (distance * moveDirection);
        transform.forward = Vector3.Lerp(transform.forward, rotateDirection, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking() {
        return isWalking;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        if (kitchenObject != null){
            OnPickupObject?.Invoke(this.gameObject, EventArgs.Empty);
        }
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }
    public Transform GetKitchenObjectLocation() {
        return kitchenObjectLocation;
    }
}
