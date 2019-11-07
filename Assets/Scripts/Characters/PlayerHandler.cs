using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

[RequireComponent(typeof(AudioSource))]

public class PlayerHandler : MonoBehaviour
{
    [Header("Character Variables")]
    public int characterSelected;
    [System.Serializable]
    public struct Character
    {
        public string name;
        public CharacterController2D controller;
        public float moveSpeed;
        public float jumpHeight;
        public Sprite logo;
        public Collider2D col;
    };

    public Character[] character; //= new Character[3];
    public Image logoswitch;
    public Projectile2 arrow;

    [Header("Value Variables")]
    public float curHealth;
    public float maxHealth;
    private float healthPerSection;
    private float prevHealth;
        
    [Header("Heart Slots")]
    public Image[] heartSlots;
    public Sprite[] heartSprites;

    [Header("Damage Effect Variables")]
    public Image damageImage;
    public Image deathImage;
    public AudioClip deathClip;
    public float flashSpeed = 5;
    public Color flashColour = new Color(1, 0, 0, .2f);
    AudioSource playerAudio;
    static public bool isDead;
    bool damaged;

    [Header("Check Point")]
    public Transform curCheckPoint;
    private float damageTimer;

    [Header("Camera")]
    //public CharacterController2D[] controller;
    public GameObject Camera;

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
        //If character is over the ammout of characters that exist set character back to 0, else go to next character
        if (characterSelected >= character.Length - 1)
        {
            characterSelected = 0;
        }
        else
        {
            characterSelected++;
        }
        if (characterSelected == 1)
        {
            arrow.enabled = true;
        }
        else
        {
            arrow.enabled = false;
        }
        for (int i = 0; i < character.Length; i++)
        {
            if (i != characterSelected)
            {
                if (character[i].controller.IsGrounded == true)
                {
                    character[i].col.enabled = false;
                    character[i].col.attachedRigidbody.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                }
            }
            else
            {
                character[i].col.enabled = true;
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

    IEnumerator PosFix()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 1; i < character.Length; i++)
        {
            character[i].col.enabled = false;
            character[i].col.attachedRigidbody.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        }
    }
    void Revive()
    {
        isDead = false;
        curHealth = maxHealth;

        //move and rotate spawn location
        this.transform.position = curCheckPoint.position;
        this.transform.rotation = curCheckPoint.rotation;
        deathImage.gameObject.GetComponent<Animator>().SetTrigger("Alive");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            curCheckPoint = other.transform;
        }
    }

}
