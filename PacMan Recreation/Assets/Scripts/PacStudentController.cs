using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PacStudentController : MonoBehaviour
{
    private Vector3 movement;
    private float movementSqrMagnitude;
    private Quaternion rotation;
    public Animator pacStudentAnimator;
    public float walkSpeed = 1.75f;
    public AudioSource walkingSound;
    public AudioSource IntroBGMusic;
    public AudioSource BGMusic;
    private Tweener tweener;
    private Vector3 playerInitialPos;
    private string lastInput;
    private string currentInput;
    private float timer;
    public ParticleSystem dust;
    private Rigidbody2D playerRB;
    public AudioSource wallHitSound;
    public int score;
    private Vector3Int playerCurrentPos;
    public Tilemap pelletMap;
    private bool pelletHit = false; 
    //public Tile empty;

    // Start is called before the first frame update
    void Start()
    {
        IntroBGMusic.Play();
        playerInitialPos = new Vector3(-12.5f, 13.5f, 0);
        tweener = gameObject.GetComponent<Tweener>();
        Invoke("PlayBG", 1.5f);
        gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -45);
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        score = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
        //CharacterPosition();
        //CharacterRotation();
        WalkingAnimation();
        WalkingAudio();

        // Left
        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") == -1)
        {
            StartCoroutine(CharacterRotate(45.0f));
            //StartCoroutine(CharacterMove(movement));
            lastInput = "left";
            CharacterPosition();
            //StartCoroutine(CharacterMove(movement));

        }
        // Right
        else if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") == 1)
        {
            StartCoroutine(CharacterRotate(-135.0f));
            //StartCoroutine(CharacterMove(movement));
            lastInput = "right";
            CharacterPosition();
            //StartCoroutine(CharacterMove(movement));
        }
        // Up
        else if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") == 1)
        {
            StartCoroutine(CharacterRotate(-45.0f));
            lastInput = "up";
            CharacterPosition();
            //StartCoroutine(CharacterMove(movement));
        }
        // Down
        else if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") == -1)
        {
            StartCoroutine(CharacterRotate(135.0f));
            lastInput = "down";
            CharacterPosition();
            //StartCoroutine(CharacterMove(movement));
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pellet")
        {
            
            Destroy(collision.gameObject);
            score += 10;
            /*
            if (score == 0)
            {
                score = 10;
                pelletHit = true;
            }
            else if (score == 10 && pelletHit)
            {
                score = 10;
                pelletHit = false;
            }
            else
            {
                score += 10;
            }
            */
            //playerCurrentPos = pelletMap.WorldToCell(gameObject.transform.position);
            //foreach (ContactPoint2D hit in collision.co)
            //{
            //Debug.Log(collision);
            //pelletMap.SetTile(pelletMap.WorldToCell(playerCurrentPos), null);
            //}
            //DestroyImmediate(collision.gameObject);
            //if (score > 2360)
            //{
                // Game Over
            //}

        }

        if (collision.gameObject.tag == "Teleporter")
        {
            Debug.Log("Hit " + collision + ", Teleport Me Baby!");
            if (gameObject.transform.position.x <= -14.5f && currentInput == "left")
            {
                gameObject.transform.position = new Vector3 (13.5f, gameObject.transform.position.y, 0);
                Debug.Log("Teleport Me!");
            }
            else if (gameObject.transform.position.x >= 14.5f && currentInput == "right")
            {
                gameObject.transform.position = new Vector3(-13.5f, gameObject.transform.position.y, 0);
            }
        }
        
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pellet"))
        {
            score = score + 10;
            //Destroy(collision.gameObject);
            Vector3 hitPosition = Vector3.zero;
            playerCurrentPos = pelletMap.WorldToCell(collision.gameObject.transform.position);
            foreach (ContactPoint2D hit in collision.contacts)
            {
                //Debug.Log(collision);
                hitPosition.x = hit.point.x - 0.01f;
                hitPosition.y = hit.point.y - 0.01f;
                pelletMap.SetTile(pelletMap.WorldToCell(hitPosition), null);
            }
        }
    }
    */
    //TilemapCollider2D.OnTriggerEnter

    public void SaveScore()
    {
        PlayerPrefs.SetInt("Score", ScoreManager.CurrentScoreKeeper);
    }

    void PlayBG()
    {
        BGMusic.Play();
    }

    void GetMovementInput()
    {
        timer += Time.deltaTime;
        movement = new Vector3((int) Input.GetAxis("Horizontal"), (int)Input.GetAxis("Vertical"), 0);
        movement = Vector3.ClampMagnitude(movement, 1.0f);
        movementSqrMagnitude = movement.sqrMagnitude;
        //Debug.Log(movement);
        //CreateTween(movement, 1.5f);
    }

    void CharacterPosition()
    {
        //gameObject.transform.Translate(walkSpeed * movement * Time.deltaTime, Space.World);
        //CreateTween(gameObject.transform.position + movement, walkSpeed);
        //RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.up, 1.5f, LayerMask.GetMask("Map"));

        //if ()
        if (lastInput == "up")
        {
            //CreateTween(gameObject.transform.position + movement, walkSpeed);
            //if (gameObject.transform.position + Vector3.forward)
            currentInput = "up";
            StartCoroutine(CharacterMove(movement));
        }
        else if (lastInput == "down")
        {
            //CreateTween(gameObject.transform.position + movement, walkSpeed);
            currentInput = "down";
            StartCoroutine(CharacterMove(movement));
        }
        else if (lastInput == "left")
        {
            //CreateTween(gameObject.transform.position + Vector3.left, walkSpeed);
            currentInput = "left";
            StartCoroutine(CharacterMove(movement));
        }
        else if (lastInput == "right")
        {
            currentInput = "right";
            StartCoroutine(CharacterMove(movement));
            //CreateTween(gameObject.transform.position + Vector3.right, walkSpeed);
        }
        

    }

    private IEnumerator CharacterMove(Vector3 endPos)
    {

        if (currentInput == "up")
        {
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.up, 1.0f, LayerMask.GetMask("Wall"));
            if (hit.collider == null)
            {
                CreateTween(gameObject.transform.position + Vector3.up, walkSpeed);
                yield return new WaitForSeconds(walkSpeed);
                //WalkingAudio();
                
            }
            else
            {
                pacStudentAnimator.speed = 0;
                wallHitSound.Play();
            }
        }
        else if (currentInput == "down")
        {
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 1.0f, LayerMask.GetMask("Wall"));
            if (hit.collider == null)
            {
                CreateTween(gameObject.transform.position + Vector3.down, walkSpeed);
                yield return new WaitForSeconds(walkSpeed);
                //WalkingAudio();
                
            }
            else
            {
                //walkingSound.Stop();
                //dust.Stop();
                pacStudentAnimator.speed = 0;
                wallHitSound.Play();
            }
        }
        else if (currentInput == "left")
        {
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.left, 1.0f, LayerMask.GetMask("Wall"));
            //Debug.DrawRay(gameObject.transform.position, Vector2.left, Color.yellow);
            if (hit.collider == null)
            {
                CreateTween(gameObject.transform.position + Vector3.left, walkSpeed);
                yield return new WaitForSeconds(walkSpeed);
                //WalkingAudio();

            }
            else
            {
                pacStudentAnimator.speed = 0;
                wallHitSound.Play();
            }
        }
        else if (currentInput == "right")
        {
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.right, 1.0f, LayerMask.GetMask("Wall"));
            if (hit.collider == null)
            {
                CreateTween(gameObject.transform.position + Vector3.right, walkSpeed);
                yield return new WaitForSeconds(walkSpeed);
                //WalkingAudio();
                
            }
            else
            {
                //wallHitSound.Play();
                pacStudentAnimator.speed = 0;
                //wallHitSound.volume = movementSqrMagnitude;
                wallHitSound.Play();
            }
        }
        
    }

   

    void CharacterRotation()
    {
        //timer += Time.deltaTime;

        if (movement != playerInitialPos)
        {
            // Find a way to make it fixed at 90 degrees 
            //rotation = Quaternion.FromToRotation(gameObject.transform.position, movement * 90);
            rotation = transform.rotation * Quaternion.Euler(0.0f, 0.0f, 90); 
            gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, rotation, walkSpeed);
            
        }

    }

    private IEnumerator CharacterRotate(float angle)
    {
        if (movement != playerInitialPos)
        {
            float currRot = gameObject.transform.rotation.eulerAngles.z;
            rotation = Quaternion.Euler(0.0f, 0.0f, angle);
            gameObject.transform.rotation = rotation;
        }
        yield return new WaitForSeconds(0.5f);
    }

    void WalkingAnimation()
    {
        //pacStudentAnimator = gameObject.GetComponent<Animator>();
        //pacStudentAnimator.SetFloat("MoveSpeed", movementSqrMagnitude);
        //if
        pacStudentAnimator.speed = movementSqrMagnitude;
    }

    void WalkingAudio()
    {
        if (movementSqrMagnitude > 0.25f)
        {
            if (!walkingSound.isPlaying && !dust.isPlaying)
            {
                walkingSound.volume = movementSqrMagnitude;
                walkingSound.Play();
                dust.Play();
                BGMusic.volume = 0.2f;
            }
        }
        else if (movementSqrMagnitude <= 0.3f && walkingSound.isPlaying && dust.isPlaying)
        {
            walkingSound.Stop();
            dust.Stop();
            BGMusic.volume = 0.5f;
        }
    }

    public void CreateTween(Vector3 endPosition, float duration)
    {
        //if (endPosition.x > -13.5f && endPosition.x < 13.5f && endPosition.y > -14.5f && endPosition.y < 14.5f)
        //{
            bool addedTween = tweener.AddTween(gameObject.transform, gameObject.transform.position, endPosition, duration);
        //}
    }

}
