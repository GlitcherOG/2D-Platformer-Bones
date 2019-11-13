using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

[RequireComponent(typeof(AudioSource))]

public class PlayerHandler : MonoBehaviour
{
    [Header("Character Variables")]
    public int characterSelected; //Current selected character
    [System.Serializable]
    public struct Character //Character struct containing character varribles
    {
        public string name; //Character name
        public CharacterController2D controller; //Character controller
        public float moveSpeed; //Character movement speed
        public float jumpHeight; //Character jump height
        public Sprite logo; //Character changer logo
        public Collider2D col; //Collider for character
    };

    public Character[] character; //Character struct variable
    public Image logoswitch; //Character changer image
    public Projectile2 arrow; //Arrow script variable 

    [Header("Value Variables")]
    public float curHealth; //Current characters health
    public float maxHealth; //Max health that the characters can have
    private float healthPerSection; //How much health is in one heart section
        
    [Header("Heart Slots")]
    public Image[] heartSlots; //Heart slots on the cavas
    public Sprite[] heartSprites; //The sprites for the stages of the heart

    [Header("Damage Effect Variables")]
    public Image damageImage;
    public Image deathImage;
    public AudioClip deathClip;
    public float flashSpeed = 5;
    public Color flashColour = new Color(1, 0, 0, .2f);
    AudioSource playerAudio;
    static public bool isDead;
    bool damaged;
    private float damageTimer;

    [Header("Camera")]
    public GameObject Camera; //Camera object lock gameobject

    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        healthPerSection = (maxHealth / (heartSlots.Length) * .2f);
        arrow.enabled = false;
        StartCoroutine(PosFix());
    }
    void Update()
    {
        UpdateHeart();
        //Player is Dead
        if (curHealth <= 0 && !isDead)
        {
            Death();
        }

        //Player is Damaged
        if (damaged && !isDead)
        {
            damageImage.color = flashColour;
            damaged = false;
        }
        else
        {
            //damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        if (damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool isJumping = Input.GetButtonDown("Jump");

        if (isJumping)
        {
            character[characterSelected].controller.Jump(character[characterSelected].jumpHeight);
        }
        //Adds the movement to the selected charatcter
        character[characterSelected].controller.Move(horizontal * character[characterSelected].moveSpeed);
        //If E is pushed run change chararcter
        if (Input.GetButtonDown("Switch"))
        {
            ChangeCharacter();
        }
        //Move the camera lock object on to current characters location
        Camera.transform.position = new Vector3(character[characterSelected].controller.gameObject.transform.position.x, Camera.transform.position.y, Camera.transform.position.z);
    }

    //Changes the current character
    public void ChangeCharacter()
    {
        character[characterSelected].controller.Move(0);
        //If character is over the ammout of characters that exist set character back to 0, else go to next character
        if (characterSelected >= character.Length - 1)
        {
            characterSelected = 0;
        }
        else
        {
            characterSelected++;
        }
        //If selected character is 1 turn on arrow script
        if (characterSelected == 1)
        {
            arrow.enabled = true;
        }
        else
        {
            arrow.enabled = false;
        }
        //For all characters start ground test if not character selected run ground check else turn on the collider, set gravity to one, off rigidbody constraints
        for (int i = 0; i < character.Length; i++)
        {
            if (i != characterSelected)
            {
                StartCoroutine(GroundTest(i));
            }
            else
            {
                character[i].col.enabled = true;
                character[i].col.attachedRigidbody.gravityScale = 1;
                character[i].col.attachedRigidbody.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            }
        }
        //Switch sprite for button in the corner
        logoswitch.sprite = character[characterSelected].logo;
    }

    void UpdateHeart()
    {
        //for all the hearts in heart slots        
        for (int i = 0; i < heartSlots.Length; i++)
        {
            if (curHealth >= (healthPerSection * 5) + healthPerSection * 5 * i)
            {
                heartSlots[i].sprite = heartSprites[0];
            }
            else
            {
                heartSlots[i].sprite = heartSprites[1];
            }
        }

    }

    IEnumerator GroundTest(int charTest)
    {
        yield return new WaitUntil(() => character[charTest].controller.IsGrounded);
        if (character[charTest].controller.IsGrounded)
        {
            character[charTest].col.enabled = false;
            character[charTest].col.attachedRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            StartCoroutine(UnfreezeGround(charTest));
        }
    }

    IEnumerator UnfreezeGround(int charTest)
    {
        yield return new WaitForSeconds(0.1f);
        character[charTest].col.attachedRigidbody.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        character[charTest].col.attachedRigidbody.gravityScale = 0;
    }

    //Damages characters health
    public void Damage()
    { 
        if (damageTimer <= 0)
        {
            curHealth -= 5;
            damageTimer = 1;
        }
        
    }

    public void Death()
    {
        // set the death flag to this funciton int's called again
        isDead = true;

        //Set the AudioSource to play the death clip
        playerAudio.clip = deathClip;
        playerAudio.Play();

        deathImage.gameObject.GetComponent<Animator>().SetTrigger("Dead");
        Invoke("Revive", 9f);

    }

    //Allows for a moment of physics at start to fix character locations and make it so that object physics
    IEnumerator PosFix()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 1; i < character.Length; i++)
        {
            character[i].col.enabled = false;
            character[i].col.attachedRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }
    //void Revive()
    //{
    //    isDead = false;
    //    curHealth = maxHealth;

    //    //move and rotate spawn location
    //    this.transform.position = curCheckPoint.position;
    //    this.transform.rotation = curCheckPoint.rotation;
    //    deathImage.gameObject.GetComponent<Animator>().SetTrigger("Alive");
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("CheckPoint"))
    //    {
    //        curCheckPoint = other.transform;
    //    }
    //}

}
