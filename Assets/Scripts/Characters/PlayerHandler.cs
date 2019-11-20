using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

[RequireComponent(typeof(AudioSource))]

public class PlayerHandler : MonoBehaviour
{
    public GameManager gameManager;
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
    private float damageTimer; //How long till character can be damaged again

    [Header("Camera")]
    public GameObject Camera; //Camera object lock gameobject

    void Start()
    {
        healthPerSection = (maxHealth / (heartSlots.Length) * .2f);
        //Set variable to false
        arrow.enabled = false;
        //Start coruntine PosFix to fix character begining positions
        StartCoroutine(PosFix());
    }
    void Update()
    {
        UpdateHeart();
        //Player is Dead
        if (curHealth <= 0)
        {
            Death();
        }

        if (damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;
        }

        //Gets all input variables
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool isJumping = Input.GetButtonDown("Jump");
        
        //If jumping is true add the characters jump height to that
        if (isJumping)
        {
            character[characterSelected].controller.Jump(character[characterSelected].jumpHeight);
        }
        //Adds the movement to the selected charatcter
        character[characterSelected].controller.Move(horizontal * character[characterSelected].moveSpeed);
        //If switch input is pushed run change chararcter
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
        //Removes all force being added on to the character
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
                //Start ground test to freeze when character hits ground
                StartCoroutine(GroundTest(i));
            }
            else
            {
                //Enable colider
                character[i].col.enabled = true;
                //Change gravity scale to 1
                character[i].col.attachedRigidbody.gravityScale = 1;
                //Unfreeze rigidbody x and y
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
            //if current heal is greater than the health persection change the spirte to sprite 0 else sprite 1
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

    //Freezes the character when they touch the ground
    IEnumerator GroundTest(int charTest)
    {
        //Waits untill the bool in controller IsGrounded is true
        yield return new WaitUntil(() => character[charTest].controller.IsGrounded);
        //Enables collisons with character
        character[charTest].col.enabled = false;
        //Freezes rigidbody 
        character[charTest].col.attachedRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        //Starts Corutine on Unfreeze ground
        StartCoroutine(UnfreezeGround(charTest));
    }

    //Unfreezes the character
    IEnumerator UnfreezeGround(int charTest)
    {
        //Wait 1 milisecond
        yield return new WaitForSeconds(0.1f);
        //Unfreeze the character
        character[charTest].col.attachedRigidbody.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        //Change the gravity to zero
        character[charTest].col.attachedRigidbody.gravityScale = 0;
    }

    //Damages characters health
    public void Damage()
    {
        //If damage timer is less than zero then remove 5 health and reset timer
        if (damageTimer <= 0)
        {
            curHealth -= 5;
            damageTimer = 1;
        }
    }

    public void Death()
    {
        //Restart Level
        gameManager.Restart();
    }

    //Allows for a moment of physics at start to fix character locations and make it so that object physics
    IEnumerator PosFix()
    {
        //Waits 1 millisecond for physics to fix character positions
        yield return new WaitForSeconds(0.1f);
        //For all characters excpet active disable collisons and freeze position
        for (int i = 1; i < character.Length; i++)
        {
            character[i].col.enabled = false;
            character[i].col.attachedRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            character[i].col.attachedRigidbody.gravityScale = 0;
        }
    }
}
