using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
   //RECOMMENT ALL THIS CODE

    // Member Variables
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f; 
    [SerializeField] private bool m_AirControl = false;                        
    [SerializeField] private bool m_StickToSlopes = true;                       
    [SerializeField] private LayerMask m_WhatIsGround;                          
    [SerializeField] private Transform m_GroundCheck;                           
    [SerializeField] private Transform m_FrontCheck;                           
    [SerializeField] private float m_GroundedRadius = .05f;                    
    [SerializeField] private float m_FrontCheckRadius = .05f;                      
    [SerializeField] private float m_GroundRayLength = .2f;                     

    
    private float m_OriginalGravityScale;
    
    [Header("Events")]
    public UnityEvent OnLandEvent;

    public bool IsGrounded { get; private set; }
    public bool IsFrontBlocked { get; private set; }
    public bool IsFacingRight { get; private set; } = true;
    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Anim { get; private set; }
    public bool dJump;
    private bool temp;

    public bool HasParameter(string paramName, Animator animator)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName)
                return true;
        }
        return false;
    }


    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        m_OriginalGravityScale = Rigidbody.gravityScale;

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(m_GroundCheck.position, m_GroundedRadius);
        Gizmos.DrawWireSphere(m_FrontCheck.position, m_FrontCheckRadius);

        Gizmos.color = Color.blue;
        Ray groundRay = new Ray(transform.position, Vector3.down);
        Gizmos.DrawLine(groundRay.origin, groundRay.origin + groundRay.direction * m_GroundRayLength);

    }
    private void FixedUpdate()
    {
        AnimateDefault();

        bool wasGrounded = IsGrounded;
        IsGrounded = false;
        IsFrontBlocked = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, m_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                IsGrounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }

        colliders = Physics2D.OverlapCircleAll(m_FrontCheck.position, m_FrontCheckRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                IsFrontBlocked = true;
            }
        }
        if(IsGrounded)
        {
            temp = false;
        }
    }

    private void AnimateDefault()
    {
        if(HasParameter("IsGrounded", Anim))
            Anim.SetBool("IsGrounded", IsGrounded);

        if(HasParameter("JumpY", Anim))
            Anim.SetFloat("JumpY", Rigidbody.velocity.y);
    }

    public void Flip()
    {
        IsFacingRight = !IsFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    
    public void Jump(float height)
    {
        if (!IsGrounded && dJump == true && temp == false)
        {
            temp = true;
            IsGrounded = false;
            Rigidbody.AddForce(new Vector2(0f, height+Rigidbody.velocity.y), ForceMode2D.Impulse);
        }

        if (IsGrounded && temp==false)
        {

            IsGrounded = false;
            Rigidbody.AddForce(new Vector2(0f, height + Rigidbody.velocity.y), ForceMode2D.Impulse);
        }
    }
    

    public void Move(float offsetX)
    {
        if (HasParameter("IsRunning", Anim))
            Anim.SetBool("IsRunning", offsetX != 0);

        if (IsGrounded || m_AirControl)
        {
            if (m_StickToSlopes)
            {
                Ray groundRay = new Ray(transform.position, Vector3.down);
                RaycastHit2D groundHit = Physics2D.Raycast(groundRay.origin, groundRay.direction, m_GroundRayLength, m_WhatIsGround);
                if (groundHit.collider != null)
                {
                    Vector3 slopeDirection = Vector3.Cross(Vector3.up, Vector3.Cross(Vector3.up, groundHit.normal));
                    float slope = Vector3.Dot(Vector3.right * offsetX, slopeDirection);

                    offsetX += offsetX * slope;

                    float angle = Vector2.Angle(Vector3.up, groundHit.normal);
                    if (angle > 0)
                    {
                        Rigidbody.gravityScale = 0;
                        Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, 0f);
                    }
                }
            }


            Vector3 targetVelocity = new Vector2(offsetX, Rigidbody.velocity.y);

            Vector3 velocity = Vector3.zero;

            Rigidbody.velocity = Vector3.SmoothDamp(Rigidbody.velocity, targetVelocity, ref velocity, m_MovementSmoothing);


            if (offsetX > 0 && !IsFacingRight)
            {
                Flip();
            }

            else if (offsetX < 0 && IsFacingRight)
            {

                Flip();
            }
        }
    }
}