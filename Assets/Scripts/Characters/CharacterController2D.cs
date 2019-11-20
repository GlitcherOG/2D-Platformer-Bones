using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
   //RECOMMENT ALL THIS CODE

    // Member Variables
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f; //Movement smoothing for the charactor
    [SerializeField] private bool m_AirControl = false; //Allow control for character in the air
    [SerializeField] private bool m_StickToSlopes = true; //Sitck to slopes to allow for easier controls
    [SerializeField] private LayerMask m_WhatIsGround;                          
    [SerializeField] private Transform m_GroundCheck; //Ground check gameobject
    [SerializeField] private Transform m_FrontCheck;  //Front check gameobject
    [SerializeField] private float m_GroundedRadius = .05f;//Radius for ground check               
    [SerializeField] private float m_FrontCheckRadius = .05f;//Radius for front check  
    [SerializeField] private float m_GroundRayLength = .5f;//Radius for ground ray length check  


    private float m_OriginalGravityScale;
    
    [Header("Events")]
    public UnityEvent OnLandEvent; //Unity event for when the character lands

    public bool IsGrounded; //Is the character grounded
    public bool IsFrontBlocked { get; private set; } //Is the front of the character blocked
    public bool IsFacingRight { get; private set; } = true; //Is the character facing right
    public Rigidbody2D Rigidbody { get; private set; } //Character rigidbody
    public Animator Anim { get; private set; } //Character animatior

    [Header("Abilites")]
    public bool dJump; //If the character can double jump
    private bool temp; //Test variable

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
        //Get rigidbody component and set into variable
        Rigidbody = GetComponent<Rigidbody2D>();
        //Get animator component and set into variable
        Anim = GetComponent<Animator>();
        //Get rigidbody gravity scale and set into a variable 
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
                //transform.SetParent(groundHit.collider.gameObject.transform);
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