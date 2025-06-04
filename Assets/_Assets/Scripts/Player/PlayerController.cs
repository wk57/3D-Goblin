using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalMove;
    private float verticalMove;
    public float fallVelocity;

    [SerializeField] Camera mainCamera;
    private Vector3 camFoward;
    private Vector3 camRight;
    public float jumpForce;
    private bool isOnSlop = false;
    private Vector3 hitNormal;

    public float speed;
    private Vector3 movePlayer;
    public float slideVelocity;
    public float slopeForceDown;

    private Animator playerAnimatorController;

    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource footstepSource; // Nuevo: Para reproducir sonidos de pasos
    [SerializeField] private AudioClip[] footstepSounds; // Nuevo: Lista de sonidos de pasos
    [SerializeField] private float footstepInterval = 0.5f; // Intervalo entre pasos
    private float footstepTimer = 0f; // Temporizador para los pasos

    [SerializeField] float gravity = 9.8f;

    private Vector3 playerInput;

    [SerializeField] CharacterController player;

    void Start()
    {
        player = GetComponent<CharacterController>();
        playerAnimatorController = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        playerAnimatorController.SetFloat("walkVelocity", playerInput.magnitude * speed);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camFoward;
        movePlayer = movePlayer * speed;

        player.transform.LookAt(player.transform.position + movePlayer);

        SetGravity();

        PlayerSkills();

        player.Move(movePlayer * Time.deltaTime);

        HandleFootsteps(); // Nuevo: Manejar sonidos de pasos
    }

    void camDirection()
    {
        camFoward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camFoward.y = 0;
        camRight.y = 0;

        camFoward = camFoward.normalized;
        camRight = camRight.normalized;
    }

    void SetGravity()
    {
        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
            playerAnimatorController.SetFloat("verticalVelocity", player.velocity.y);
        }
        playerAnimatorController.SetBool("isGrounded", player.isGrounded);
        SlideDown();
    }

    public void PlayerSkills()
    {
        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            jumpSound.Play();
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
            playerAnimatorController.SetTrigger("jump");
        }
    }

    public void SlideDown()
    {
        isOnSlop = Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit;

        if (isOnSlop)
        {
            movePlayer.x += ((1f - hitNormal.y) * hitNormal.x) * slideVelocity;
            movePlayer.z += ((1f - hitNormal.y) * hitNormal.z) * slideVelocity;

            movePlayer.y += slopeForceDown;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
    }

    private void HandleFootsteps()
    {
        if (player.isGrounded && playerInput.magnitude > 0) // Solo reproducir pasos si el jugador se mueve
        {
            footstepTimer += Time.deltaTime;

            if (footstepTimer >= footstepInterval)
            {
                PlayFootstep();
                footstepTimer = 0f; // Reinicia el temporizador
            }
        }
        else
        {
            footstepTimer = 0f; // Resetea el temporizador si el jugador no se mueve
        }
    }

    private void PlayFootstep()
    {
        if (footstepSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, footstepSounds.Length);
            footstepSource.PlayOneShot(footstepSounds[randomIndex]);
        }
    }
}
