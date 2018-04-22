using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DemonController : MonoBehaviour
{
    /* public float speed;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    } */

    public float speed = 5f;
    public float jumpSpeed = 5f;
    public float disToGround = 0.5f;
    Rigidbody rb;

    // Collectibles. 
    public Text winText;
    public Text countText;
    private int count;

    // All Coins Achieved. 
    public GameObject objToDestroy;
    public GameObject effect;
    public AudioSource explosionAudio;
    public AudioSource coinAchieved;

    // Restart. 
    public float interval;

    // Demonic Boss. 
    public AudioSource witchAudio;

    // Demonic Boss Killed.  
    public float intervalDestruction;

    // Kills. 
    public GameObject demonicBoss;
    public AudioSource demonicAudio;
    public AudioSource attackMovement;

    private bool hasAniComp = false;

    public Collider[] attackHitBoxes;

    // Use this for initialization
    void Start()
    {
        if (null != GetComponent<Animation>())
        {
            hasAniComp = true;
        }

        // onGround = true;
        // rb = GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        // winText.text = "";

        /* if (null != GetComponent<Animation>())
        {
            hasAniComp = true;
        } */
    }

    void AttackSound()
    {
        attackMovement.Play();
    }

    void FixedUpdate()
    {
        // rb.AddForce(0, 0, 0);
        Debug.Log(isGrounded());
        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            // rb.AddForce(0, 0, 0);
            Vector3 jumpVelocity = new Vector3(0f, jumpSpeed, 0f);
            rb.velocity = rb.velocity + jumpVelocity;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime);
        rb.MovePosition(transform.position + movement);

        if (Input.GetKey(KeyCode.L)) { 
            GetComponent<Animation>().CrossFade("attack_short_001", 0.0f); 
            GetComponent<Animation>().CrossFadeQueued("idle_combat");
            Invoke("AttackSound", 0.5f);
            LaunchAttack(attackHitBoxes[0]);
        }
    }

    private void LaunchAttack(Collider col)
    {
        Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("Hitbox"));
        foreach (Collider c in cols)
        {
            if (c.transform.parent == transform)
                continue;
            switch(c.name){ case "demon": 
                Destroy(demonicBoss);
                demonicAudio.Play();
                Instantiate(effect, objToDestroy.transform.position, objToDestroy.transform.rotation);
                    Destroy(objToDestroy, 4f);
                    Invoke("PlayDemonicExplosion", 4f);
                    // if (intervalDestruction > 3f){intervalDestruction -= Time.deltaTime;}
                break; }
        }
    }

	bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, disToGround);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            coinAchieved.Play();
        }

        if (other.gameObject.CompareTag("RedDemon"))
        {
            // if (CheckAniClip("dead") == false) return;

            GetComponent<Animation>().CrossFade("dead", 0.2f);
            Destroy(gameObject, 9f);
            FindObjectOfType<GameManager>().EndGame();
            if (interval > 0)
            {
                interval -= Time.deltaTime;
            }
            witchAudio.Play();
        }
    }

        void PlayDemonicExplosion()
        {
            explosionAudio.Play();
        }

        void SetCountText()
        {
            countText.text = "Coins: " + count.ToString();
            if (count >= 21)
            {
                // winText.text = "Go To The Skull Evil Platform!";
                Instantiate(effect, objToDestroy.transform.position, objToDestroy.transform.rotation);
                Invoke("PlayDemonicExplosion", 4f);
                Destroy(objToDestroy, 4f);
            }
        }

        /* bool CheckAniClip(string clipname)
        {
            if (this.GetComponent<Animation>().GetClip(clipname) == null)
                return false;
            else if (this.GetComponent<Animation>().GetClip(clipname) != null)
                return true;

            return false;
        } */
}