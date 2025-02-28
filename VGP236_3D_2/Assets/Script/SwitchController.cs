using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public GameObject exitDoor;
    public CinemachineCamera switchCamera;
    public CinemachineCamera playerCamera;

    public float cameraDelay = 1.0f;

    private bool isActivated = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
            StartCoroutine(ActivateExit());
        }
    }

    IEnumerator ActivateExit()
    {
        PlayerMovement player = FindFirstObjectByType<PlayerMovement>();
        player.enabled = false;
        GameManager.Instance.PauseAllAI();

        playerCamera.Priority = 0;
        switchCamera.Priority = 10;

        yield return new WaitForSeconds(cameraDelay);

        Animator doorAnimator = exitDoor.GetComponent<Animator>();
        float animLength = doorAnimator.GetCurrentAnimatorStateInfo(0).length;
        if (doorAnimator != null)
        {
            doorAnimator.Play("ExitDoorOpen");
        }

        yield return new WaitForSeconds(animLength + 0.5f);

        playerCamera.Priority = 10;
        switchCamera.Priority = 0;

        player.enabled = true;
        yield return new WaitForSeconds(cameraDelay);
        GameManager.Instance.ResumeAllAI();
    }
}
