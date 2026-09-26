using UnityEngine;

public class NPCMovement : MonoBehaviour //place holder script until npc movement is defined in my head huhuhu
{
    [Header("Points")]
    public Transform entrancePoint;
    public Transform counterPoint;
    public Transform exitPoint;

    [Header("Settings")]
    public float moveSpeed = 2f;
    public float stopDistance = 0.15f;

    private Animator anim;
    private bool isMoving;

    // --- ADDED ---
    private bool playingFootsteps = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        isMoving = false;
        if (entrancePoint != null)
            transform.position = entrancePoint.position;
    }

    public bool MoveTo(Vector3 target)
    {
        Vector3 delta = target - transform.position;
        if (delta.magnitude <= stopDistance)
        {
            isMoving = false;
            Idle();
            StopFootsteps(); // ADDED
            return true;
        }

        Vector3 dir = delta.normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;

        anim.SetBool("isWalking", true);
        anim.SetFloat("lastX", dir.x);
        anim.SetFloat("lastY", dir.y);

        isMoving = true;

        if (!playingFootsteps)   // ADDED
            StartFootsteps();

        return false;
    }

    public void Idle()
    {
        anim.SetBool("isWalking", false);
        anim.Play("Idle", -1, 0f);
        isMoving = false;
        StopFootsteps(); // ADDED
    }

    public void FacePlayer(Transform player)
    {
        Vector3 dir = (player.position - transform.position).normalized;
        anim.SetFloat("lastX", dir.x);
        anim.SetFloat("lastY", dir.y);
        Idle();
    }

    public bool IsMoving() => isMoving;

    // --- FOOTSTEP METHODS ADDED BELOW ---

    void StartFootsteps()
    {
        playingFootsteps = true;
        InvokeRepeating(nameof(PlayFootstep), 0f, 0.45f);
    }

    void StopFootsteps()
    {
        playingFootsteps = false;
        CancelInvoke(nameof(PlayFootstep));
    }

    void PlayFootstep()
    {
        SoundManager.instance.PlaySound2D("Footsteps");
    }
}