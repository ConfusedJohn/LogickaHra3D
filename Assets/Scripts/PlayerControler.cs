using UnityEngine;
using Cinemachine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour
{
    public float speed = 0;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float maxSpeed = 10.0F;
    public Transform respawn;
    public float rotationSpeed = 25.0F;
    public GameObject prefab;
    public AudioSource jump;
    public AudioSource death;
    public AudioSource finish;
    public AudioSource bounce;
    public AudioSource unlock;
    public bool isKey=false;
    public AudioMixerGroup MyMixerGroup;


    [SerializeField]
    private Canvas myCanvas;

    private Vector3 moveDirection;
    private Rigidbody rb;
    private bool isFalling = false;
    private Camera cameraMain;
    private GameObject cinemachineGameObject;
    private CinemachineFreeLook cinemachine;
    private Transform cameraTransform;
    private bool pos=false;

    private void Awake()
    {
        if (cinemachine == null)
        {
            cinemachineGameObject = GameObject.Find("CM FreeLook1");
            cinemachine = cinemachineGameObject.GetComponent<CinemachineFreeLook>();
        }
    }

    void Start()
    {
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        cameraMain = Camera.main;
        cameraTransform = cameraMain.transform;
        cinemachineGameObject = GameObject.Find("CM FreeLook1");
        cinemachine = cinemachineGameObject.GetComponent<CinemachineFreeLook>();

        jump.outputAudioMixerGroup = MyMixerGroup;
        death.outputAudioMixerGroup = MyMixerGroup;
        finish.outputAudioMixerGroup = MyMixerGroup;
        bounce.outputAudioMixerGroup = MyMixerGroup;
        unlock.outputAudioMixerGroup = MyMixerGroup;
    }

    private void OnEnable()
    {
        if(cinemachine!=null)cinemachine.LookAt = transform;
        if(cinemachine!= null)cinemachine.Follow = transform;
    }

    void FixedUpdate()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = cameraTransform.TransformDirection(moveDirection);
        moveDirection *= speed;
        //moveDirection.y = rb.velocity.y;
        //Debug.Log("speed x"+ rb.velocity.x + "  speed y" + rb.velocity.z);
        if (rb.velocity.x < maxSpeed && rb.velocity.z < maxSpeed)
        {
            rb.AddForce(moveDirection);
        }

        //rb.velocity = moveDirection;
        //rb.AddForce(moveDirection);
        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);Impulse
    }
    void Update() {
        DetectObjectWithRaycast();
        if (Input.GetButtonDown("Jump") && !isFalling)
        {
            //Jump
            jump.Play();
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }
        if (Input.GetButtonDown("Duplicate"))
        {
            if (Time.timeScale == 0)
            {
                Cursor.visible = false;
                Time.timeScale = 1;
                pos = false;
            }
            else
            {
                Cursor.visible = true;
                Time.timeScale = 0;
                pos = true;
            }
        }
        if (Input.GetButtonDown("Restart"))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
        
        if (Input.GetButtonDown("Cancel"))
        {
            if (Time.timeScale == 1 || Time.timeScale == 0.5f)
            {
                Time.timeScale = 0;
                Cursor.visible = true;
                myCanvas.GetComponent<Canvas>().gameObject.SetActive(true);
            }
            else
            {           
                Time.timeScale = 1;
                Cursor.visible = false;
                myCanvas.GetComponent<Canvas>().gameObject.SetActive(false);
            }
        }
    }
    public void OnCollisionStay(Collision col)
    { //Takes parameter of Collision so unity doesn't complain
        isFalling = false;
        
    }

    public void OnCollisionExit()
    {
        
        isFalling = true;
    }
    public void OnCollisionEnter(Collision collision)
    {
        bounce.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Killbox"))
        {
            death.Play();
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = respawn.position;
        }
        if (other.gameObject.CompareTag("Lock"))
        {
            if(this.isKey)
            {
                unlock.Play();
                other.gameObject.SetActive(false);
            }
            
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            finish.Play();
            Time.timeScale = 0;
            Cursor.visible = true;                          
        }

    }
    public void DetectObjectWithRaycast()
    {
        if (pos) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cameraMain.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        Time.timeScale = 1;
                        Cursor.visible = false;
                        if (hit.collider.gameObject != gameObject) 
                        {
                            hit.collider.gameObject.GetComponent<PlayerControler>().enabled = true;

                            GetComponent<PlayerControler>().enabled = false;
                        }
                        
                        pos = false;
                    }

                }
            }
        }
        
    }

}
