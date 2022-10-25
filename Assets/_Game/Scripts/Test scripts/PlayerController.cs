using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController = null;
    public Joystick joystick = null;

    public GameObject playerModel = null;

    private Vector3 moveVector = Vector3.zero;

    //  private Vector3 prevMoveVector = Vector3.zero;
    private float prevJoystickMagnitude = 0f;

    private Animator playerAnimator = null;

    private void Awake()
    {
        playerAnimator = playerModel.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
       // playerAnimator.SetTrigger("Walk");
        //playerAnimator.SetFloat("speed", 0f);
        //joystick.Horizontal
    }

    // Update is called once per frame
    void Update()
    {
        //prevMoveVector = moveVector;

        moveVector.x = joystick.Horizontal;
        moveVector.z = joystick.Vertical;
        moveVector *= Time.deltaTime;
        characterController.Move(moveVector);

        if (moveVector != Vector3.zero) playerModel.transform.forward = moveVector.normalized;

        // if(moveVector.magnitude>0){
        playerAnimator.SetFloat("speed", joystick.Direction.magnitude);
        //playerAnimator.
        // Debug.Log(moveVector.x);
        //Debug.Log

        if ((prevJoystickMagnitude == 0f) && (joystick.Direction.magnitude > 0f))
        {
            playerAnimator.SetTrigger("Walk");
        }

        if ((prevJoystickMagnitude > 0f) && (joystick.Direction.magnitude == 0f))
        {
            playerAnimator.SetTrigger("Idle");
        }

/*         if ((prevMoveVector == Vector3.zero) && (moveVector != Vector3.zero))
        {
            playerAnimator.SetTrigger("Walk");
        }

        if ((prevMoveVector != Vector3.zero) && (moveVector == Vector3.zero))
        {
            playerAnimator.SetTrigger("Idle");
        } */

        /*         if (playerAnimator.GetBool("isWalking") != (joystick.Direction.magnitude > 0))
                {
                    playerAnimator.SetBool("isWalking", joystick.Direction.magnitude > 0);
                } */

        //Debug.Log(joystick.Direction.magnitude);
        //}
        prevJoystickMagnitude = joystick.Direction.magnitude;
    }
}
