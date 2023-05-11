using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static TwoPlayer;

public class GameController : MonoBehaviour, IGameplayActions
{
    [SerializeField]
    GameObject player1GameObject;

    [SerializeField]
    GameObject player2GameObject;


    private TwoPlayer inputs;

    public float moveSpeed = 2.0f;

    public float xMinPlayer1 = -3.0f;

    public float xMaxPlayer1 = 3.0f;

    public float xMinPlayer2 = -3.0f;

    public float xMaxPlayer2 = 3.0f;

    Vector2 moveAmountPlayer1;

    Vector2 moveAmountPlayer2;


    // Start is called before the first frame update
    void Awake()
    {
        inputs = new TwoPlayer();


    }

    private void OnEnable()
    {
        inputs.gameplay.Enable();
        inputs.gameplay.SetCallbacks(this);
    }

    private void OnDisable()
    {
        inputs.gameplay.Disable();
        inputs.gameplay.RemoveCallbacks(this);
    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontallyPlayer1(moveAmountPlayer1);
        MoveHorizontallyPlayer2(moveAmountPlayer2);
    }

    private void MoveHorizontallyPlayer1(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.01)
            return;

        // check if the direction being moved will go past the horizontal limits
        float xPos = player1GameObject.transform.position.x;
        float moveAmount = direction.x * moveSpeed;


        if (direction.x < 0 && (xPos + moveAmount < xMinPlayer1)) // direction < 0 - left
        {
            return;
        }

        if (direction.x > 0 && (xPos + moveAmount > xMaxPlayer1)) // direction > 0 - right
        {
            return;
        }

        var scaledMoveSpeed = moveSpeed * Time.deltaTime;
        var move = Quaternion.Euler(0, player1GameObject.transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, 0);
        player1GameObject.transform.position += move * scaledMoveSpeed;
    }

    private void MoveHorizontallyPlayer2(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.01)
            return;

        // check if the direction being moved will go past the horizontal limits
        float xPos = player1GameObject.transform.position.x;
        float moveAmount = direction.x * moveSpeed;


        if (direction.x < 0 && (xPos + moveAmount < xMinPlayer2)) // direction < 0 - left
        {
            return;
        }

        if (direction.x > 0 && (xPos + moveAmount > xMaxPlayer2)) // direction > 0 - right
        {
            return;
        }

        var scaledMoveSpeed = moveSpeed * Time.deltaTime;
        var move = Quaternion.Euler(0, player2GameObject.transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, 0);
        player2GameObject.transform.position += move * scaledMoveSpeed;
    }


    public void OnKeyboardLeft(InputAction.CallbackContext context)
    {
        Debug.Log("player 1 move");
        // read the value for the "move" action each event call
        moveAmountPlayer1 = context.ReadValue<Vector2>();
    }

    public void OnKeyboardRight(InputAction.CallbackContext context)
    {
        Debug.Log("player 2 move");
        // read the value for the "move" action each event call
        moveAmountPlayer2 = context.ReadValue<Vector2>();
    }
}
