using System.Collections;
using UnityEngine;

public class Player_Input : MonoBehaviour
{
    #region variables
    [Header("References To Be Set")]
    public UI_Manager local_UI_Manager;
    public Player_Combat_Controller local_Player_Combat_Controller;
    public Player_Movement_Controls local_Player_Movement_Controls;
    public Soul_Mixer_Manager local_Soul_Mixer_Manager;
    public Player_Manager local_Player_Manager;
    public Aoe_Special_Ability local_Aoe_Special_Ability;
    public Shield_Special_Ability local_Shield_Special_Ability;
    public Rune_Drop_Special_Ability local_Rune_Drop_Special_Ability;
    public RectTransform cursor_Image;
    [Space]
    [Header("Properties To Be Set")]
    public float time_Between_Projectiles = 1.4f;
    public float time_Halted_While_Using_Abilities = 1f;
    public float mouseY_Sensitivity;

    float time_Since_Last_Projectile = 0.2f;
    public float soul_Mixer_Cooldown = 1f;
    float time_Since_Last_Soulmix = 0;
    int projectile_Click_Layermask = 1 << 8;
    int soulMixer_Click_Layermask = 1 << 9;
    float temp_Value;
    RaycastHit hit;
    Ray ray;

    [HideInInspector]
    public bool can_Change_Colours = true;
    [HideInInspector]
    public bool can_Use_AOE = true;
    [HideInInspector]
    public bool can_Use_Shield = true;
    [HideInInspector]
    public bool can_Use_Runes = true;

    private Animator anim;
    #endregion


    private void Start()
    {
        time_Between_Projectiles = local_Player_Manager.fire_Rate;

        //Referencing the animator specified in the player manager script
        anim = local_Player_Manager.anim;
        cursor_Image.anchoredPosition = Vector2.zero;
        temp_Value = local_Player_Movement_Controls.movement_Speed;

    }

    /// <summary>
    /// Checks for player input
    /// </summary>
    void Update()
    {

        if (Input.GetAxis("Joystick_Mouse Y") > 0)
        {
            Vector2 temp;
            cursor_Image.anchoredPosition += (Vector2.up * Time.deltaTime * mouseY_Sensitivity);
            temp = cursor_Image.anchoredPosition;


            temp.y = Mathf.Clamp(cursor_Image.anchoredPosition.y, -Screen.height / 2, Screen.height / 2);
            cursor_Image.anchoredPosition = temp;

        }

        if (Input.GetAxis("Joystick_Mouse Y") < 0)
        {

            Vector2 temp;
            cursor_Image.anchoredPosition += (Vector2.down * Time.deltaTime * mouseY_Sensitivity);
            temp = cursor_Image.anchoredPosition;

            temp.y = Mathf.Clamp(cursor_Image.anchoredPosition.y, -Screen.height / 2, Screen.height / 2);
            cursor_Image.anchoredPosition = temp;

        }
        if (Input.GetAxis("Joystick_Mouse X") > 0)
        {

            Vector2 temp;
            cursor_Image.anchoredPosition += (Vector2.right * Time.deltaTime * mouseY_Sensitivity);
            temp = cursor_Image.anchoredPosition;
            temp.x = Mathf.Clamp(cursor_Image.anchoredPosition.x, -Screen.width / 2, Screen.width / 2);

            cursor_Image.anchoredPosition = temp;

        }

        if (Input.GetAxis("Joystick_Mouse X") < 0)
        {

            Vector2 temp;
            cursor_Image.anchoredPosition += (Vector2.left * Time.deltaTime * mouseY_Sensitivity);
            temp = cursor_Image.anchoredPosition;
            temp.x = Mathf.Clamp(cursor_Image.anchoredPosition.x, -Screen.width / 2, Screen.width / 2);

            cursor_Image.anchoredPosition = temp;

        }



        if (Input.GetAxis("Mouse X") > 0)
        {
            cursor_Image.transform.position = Input.mousePosition;

        }
        if (Input.GetAxis("Mouse X") < 0)
        {
            cursor_Image.transform.position = Input.mousePosition;
        }

        if (Input.GetAxis("Mouse Y") > 0)
        {
            cursor_Image.transform.position = Input.mousePosition;
        }
        if (Input.GetAxis("Mouse Y") < 0)
        {
            cursor_Image.transform.position = Input.mousePosition;
        }

        //first checks if the mouse was clicked on the soulmixer object, and if it wasn't, then checks if the player clicked the ground
        if (Input.GetButton("Fire1") || Input.GetAxis("Fire Trigger") > 0)
        {
            ray = Camera.main.ScreenPointToRay(cursor_Image.transform.position);

            if ((Physics.Raycast(ray, out hit, 250f, soulMixer_Click_Layermask)))
            {
                if (Input.GetButtonDown("Fire1") || Input.GetAxis("Fire Trigger") > 0)
                {
                    if (time_Since_Last_Soulmix >= soul_Mixer_Cooldown)
                    {
                        local_Soul_Mixer_Manager = hit.collider.gameObject.GetComponent<Soul_Mixer_Manager>();
                        local_Soul_Mixer_Manager.Take_In_Colour();
                        time_Since_Last_Soulmix = 0;
                        //Setting animation parameters
                        anim.SetBool("Fire", true);
                    }
                    else
                    {
                        anim.SetBool("Fire", false);
                    }


                }
                else
                {
                    anim.SetBool("Fire", false);
                }
            }

            else if (Input.GetButton("Fire1") || Input.GetAxis("Fire Trigger") > 0)
            {
                if (Physics.Raycast(ray, out hit, 250f, projectile_Click_Layermask))
                {
                    if (time_Since_Last_Projectile >= time_Between_Projectiles)
                    {
                        local_Player_Combat_Controller.Detect_Mouse_Click(hit);
                        time_Since_Last_Projectile = 0f;
                        //Setting animation parameters
                        anim.SetBool("Fire", true);
                    }
                    else
                    {
                        anim.SetBool("Fire", false);
                    }
                }
                else
                {
                    anim.SetBool("Fire", false);
                }
            }
            else
            {
                anim.SetBool("Fire", false);
            }


        }
        else
        {
            anim.SetBool("Fire", false);
        }

        //if the mouse wheel is scrolled, changes the players selected colour

        if (can_Change_Colours)
        {
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f || Input.GetButtonDown("Increase_Spell_Colour"))
            {
                if (local_Player_Manager.local_Selected_Colour != Player_Manager.Selected_Colour.DEFAULT)
                {
                    local_Player_Manager.local_Selected_Colour++;

                }
                else
                {
                    local_Player_Manager.local_Selected_Colour = Player_Manager.Selected_Colour.RED;
                }

                local_UI_Manager.Change_Player_UI_Colour();

            }
            if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f || Input.GetButtonDown("Decrease_Spell_Colour"))
            {
                if (local_Player_Manager.local_Selected_Colour != Player_Manager.Selected_Colour.RED)
                {
                    local_Player_Manager.local_Selected_Colour--;
                }
                else
                {
                    local_Player_Manager.local_Selected_Colour = Player_Manager.Selected_Colour.DEFAULT;
                }

                local_UI_Manager.Change_Player_UI_Colour();
            }
        }




        //checks for input to cast the special abilities
        if (can_Use_AOE)
        {
            if (Input.GetButtonDown("Ability One"))
            {
                local_Aoe_Special_Ability.Aoe_Abilitiy((int)local_Player_Manager.local_Selected_Colour);

            }
        }

        if (can_Use_Shield)
        {
            if (Input.GetButtonDown("Ability Two"))
            {
                local_Shield_Special_Ability.Shield_Ability((int)local_Player_Manager.local_Selected_Colour);
            }
        }

        if (can_Use_Runes)
        {
            if (Input.GetButtonDown("Ability Three"))
            {
                local_Rune_Drop_Special_Ability.Rune_Drop((int)local_Player_Manager.local_Selected_Colour);

            }
        }
        if (local_UI_Manager.aoe_Cooldown_Timer > 0)
        {
            local_UI_Manager.aoe_Cooldown_Timer -= Time.deltaTime;
        }
        else
        {
            local_UI_Manager.aoe_Cooldown_Timer = 0;
        }

        if (local_UI_Manager.shield_Cooldown_Timer > 0)
        {
            local_UI_Manager.shield_Cooldown_Timer -= Time.deltaTime;
        }

        else
        {
            local_UI_Manager.shield_Cooldown_Timer = 0;
        }


        if (local_UI_Manager.dash_Cooldown_Timer > 0)
        {
            local_UI_Manager.dash_Cooldown_Timer -= Time.deltaTime;
        }
        else
        {
            local_UI_Manager.dash_Cooldown_Timer = 0;
        }




        time_Since_Last_Projectile += Time.deltaTime;
        time_Since_Last_Soulmix += Time.deltaTime;
    }

    /// <summary>
    /// stops player moving while casting abilities
    /// </summary>
    /// <returns></returns>
    IEnumerator Halt_Player()
    {
        local_Player_Movement_Controls.movement_Speed = 0;
        yield return new WaitForSeconds(time_Halted_While_Using_Abilities);
        local_Player_Movement_Controls.movement_Speed = temp_Value;
    }
}
