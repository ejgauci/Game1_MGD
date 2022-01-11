using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour, IPunObservable {

	public CharacterController2D controller;
	public Animator animator;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;


	public PhotonView photonView;
    private Vector3 playerPos;


    Rigidbody2D body;
    public FixedJoystick fixedJoystick;


    void Start()
    {
        photonView = PhotonView.Get(this);

        if (!photonView.IsMine)
        {
            //if player object (box) is not mine, then it should automatically be controlled by photon data

            //therefore destroy rigidbody
            Destroy(GetComponent<Rigidbody2D>());
        }
        else
        {
            body = GetComponent<Rigidbody2D>();
            fixedJoystick = GameObject.FindWithTag("Joystick").GetComponent<FixedJoystick>();
        }


    }


	// Update is called once per frame
	void Update () {


        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, this.playerPos, Time.deltaTime * 10);
        }
        else
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            }
            else
            {
                horizontalMove = fixedJoystick.Horizontal * runSpeed;
            }


            

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("isJumping", true);
            }
        }
		

	}

	public void OnLanding ()
	{
		animator.SetBool("isJumping", false);
	}




	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);

        }
        else
        {
            this.playerPos = (Vector3)stream.ReceiveNext();

        }
    }


}
