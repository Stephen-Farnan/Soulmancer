using System.Collections;
using UnityEngine;

public class Player_Movement_Controls : MonoBehaviour
{
    #region variables
    [Header("References To Be Set")]
    public Player_Manager local_Player_Manager;
    public UI_Manager local_UI_Manager;
    public GameObject player_Model;
    public GameObject camera_Right_Vector;
    public GameObject camera_Left_Vector;

    public GameObject dash_Particles_Trail;
    public GameObject dash_Particles_Origin;
    [Space]
    [Header("Properties To Be Set")]
    public float movement_Speed = 2f;
    public float dash_Distance = 130f;
    public float dash_Cooldown = 3f;
    public float turnSpeed = 300f;
    public float damping = 10f;

    Vector3 current_Direction;
    Vector2 input;
    bool dash_Avalable = true;
    bool is_Playing_Run_Animation;
    Rigidbody player_Rigidbody;
    Facing_Direction local_Facing_Direction;

    private Animator anim;

    enum Facing_Direction
    {
        NEUTRAL,
        FORWARDS,
        LEFT,
        RIGHT,
        BACKWARDS,
        FORWARDS_RIGHT,
        FORWARDS_LEFT,
        BACKWARDS_RIGHT,
        BACKWARDS_LEFT
    }
    #endregion

    void Start()
    {
        //Initialises the forward direction for the player and starting orientation
        player_Model.transform.LookAt(Camera.main.transform);
        player_Model.transform.localEulerAngles = new Vector3(0f, player_Model.transform.localEulerAngles.y, 0);
        player_Rigidbody = GetComponent<Rigidbody>();

        //Referencing the animator specified in the player manager script
        anim = local_Player_Manager.anim;
    }

    /// <summary>
    /// //checks for input to set the direction of the player
    /// </summary>
    void Update()
    {


        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            current_Direction = new Vector3(camera_Right_Vector.transform.forward.x, 0, camera_Right_Vector.transform.forward.z);
            local_Facing_Direction = Facing_Direction.RIGHT;
            transform.Translate(current_Direction.normalized * Time.deltaTime * movement_Speed);
            Set_Direction();
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            current_Direction = new Vector3(camera_Left_Vector.transform.forward.x, 0, camera_Left_Vector.transform.forward.z);
            local_Facing_Direction = Facing_Direction.LEFT;
            transform.Translate(current_Direction.normalized * Time.deltaTime * movement_Speed);
            Set_Direction();
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            current_Direction = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
            local_Facing_Direction = Facing_Direction.FORWARDS;
            transform.Translate(current_Direction.normalized * Time.deltaTime * movement_Speed);
            Set_Direction();
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            current_Direction = new Vector3(-Camera.main.transform.forward.x, 0, -Camera.main.transform.forward.z);
            local_Facing_Direction = Facing_Direction.BACKWARDS;
            transform.Translate(current_Direction.normalized * Time.deltaTime * movement_Speed);
            Set_Direction();
        }

        //Setting animation parameters
        if ((Input.GetAxisRaw("Vertical") == 0 && (Input.GetAxisRaw("Horizontal")) == 0))
        {
            if (is_Playing_Run_Animation)
            {
                anim.SetBool("Run", false);
                is_Playing_Run_Animation = false;
                if (local_Player_Manager.Moving_Sound.isPlaying)
                {
                    local_Player_Manager.Moving_Sound.Stop();
                }
            }

        }
        else
        {
            if (!is_Playing_Run_Animation)
            {
                anim.SetBool("Run", true);
                is_Playing_Run_Animation = true;
                if (!local_Player_Manager.Moving_Sound.isPlaying)
                {
                    local_Player_Manager.Moving_Sound.Play();
                }
            }

        }

        //sets diagonal orientation if the player is moving in both axes
        if (Input.GetAxis("Vertical") < 0 && Input.GetAxisRaw("Horizontal") > 0)
        {
            local_Facing_Direction = Facing_Direction.BACKWARDS_RIGHT;
            Set_Direction();

        }

        if (Input.GetAxis("Vertical") < 0 && Input.GetAxisRaw("Horizontal") < 0)
        {
            local_Facing_Direction = Facing_Direction.BACKWARDS_LEFT;
            Set_Direction();
        }

        if (Input.GetAxis("Vertical") > 0 && Input.GetAxisRaw("Horizontal") < 0)
        {
            local_Facing_Direction = Facing_Direction.FORWARDS_LEFT;
            Set_Direction();
        }

        if (Input.GetAxis("Vertical") > 0 && Input.GetAxisRaw("Horizontal") > 0)
        {
            local_Facing_Direction = Facing_Direction.FORWARDS_RIGHT;
            Set_Direction();
        }

        if (!Input.anyKey)
        {
            local_Facing_Direction = Facing_Direction.NEUTRAL;
            Set_Direction();
        }

        //checks for input to handle the player dodge mechanic
        if (Input.GetButtonDown("Dash"))
        {
            if (dash_Avalable)
            {
                StartCoroutine("Dash_Forward");
                local_UI_Manager.dash_Cooldown_Timer = dash_Cooldown;
            }
        }

        //checks which direction the player needs to face

    }

    public void Set_Direction()
    {
        switch (local_Facing_Direction)
        {
            case Facing_Direction.RIGHT:
                SetPlayerRotation();
                break;

            case Facing_Direction.LEFT:
                SetPlayerRotation();
                break;

            case Facing_Direction.FORWARDS:
                SetPlayerRotation();
                break;

            case Facing_Direction.BACKWARDS:
                SetPlayerRotation();
                break;

            case Facing_Direction.BACKWARDS_LEFT:
                SetPlayerRotation();
                break;

            case Facing_Direction.BACKWARDS_RIGHT:
                SetPlayerRotation();
                break;

            case Facing_Direction.FORWARDS_RIGHT:
                SetPlayerRotation();
                break;

            case Facing_Direction.FORWARDS_LEFT:
                SetPlayerRotation();
                break;
        }
    }


    /// <summary>
    /// rotates the player to their new target direction
    /// </summary>
    public void SetPlayerRotation()
    {
        float newYRotation = Camera.main.transform.eulerAngles.y + Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;
        player_Model.transform.rotation = Quaternion.Lerp(player_Model.transform.rotation, Quaternion.Euler(0, newYRotation, 0), turnSpeed);
    }


    /// <summary>
    /// quickly moves the player forward in the direction they are facing when they dodge ability is pressed
    /// </summary>
    /// <returns></returns>
    public IEnumerator Dash_Forward()
    {
        //Setting animation trigger for dashing
        anim.SetTrigger("Dash");
        local_Player_Manager.Dash_Sound.Play();
        dash_Particles_Trail.SetActive(true);

        player_Rigidbody.AddForce(player_Model.transform.forward * dash_Distance, ForceMode.Impulse);
        dash_Avalable = false;
        yield return new WaitForSeconds(1.1f);
        dash_Particles_Trail.SetActive(false);
        yield return new WaitForSeconds(dash_Cooldown);
        dash_Avalable = true;
    }


}
