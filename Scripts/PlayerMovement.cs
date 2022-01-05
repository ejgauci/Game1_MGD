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
	


    void Start()
    {
        photonView = PhotonView.Get(this);

        /*
        if (!photonView.IsMine)
        {
            GetComponentInChildren<Camera>().enabled = false;
            GetComponent<AudioListener>().enabled = false;
        }
        */

    }


	// Update is called once per frame
	void Update () {


        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, this.playerPos, Time.deltaTime * 10);
        }
        else
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

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
