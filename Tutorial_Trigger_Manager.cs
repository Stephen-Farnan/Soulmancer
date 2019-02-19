using UnityEngine;

public class Tutorial_Trigger_Manager : MonoBehaviour
{

    [Header("Reference To Be Set")]
    public Tutorial_Manager local_Tutorial_Manager;


    public enum Trigger_Section
    {
        MOVEMENT,
        DASH,
        SHOOTING,
        COLOURS,
        AOE,
        SHIELD,
        RUNES,
        HEALTH,
        MIXING,
        END
    }

    bool ended_Tutorial = false;
    [Header("Properties To Be Set")]
    public Trigger_Section local_Trigger_Section;



    /// <summary>
    /// Sets different stages of the tutorial as the player reaches them
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (local_Trigger_Section)
            {
                case Trigger_Section.MOVEMENT:
                    local_Tutorial_Manager.Trigger_Movement_Tutorial();
                    break;

                case Trigger_Section.DASH:
                    local_Tutorial_Manager.Trigger_Dash_Tutorial();
                    break;

                case Trigger_Section.SHOOTING:
                    local_Tutorial_Manager.Trigger_Shooting_Tutorial();
                    break;

                case Trigger_Section.COLOURS:
                    local_Tutorial_Manager.Trigger_Colours_Tutorial();
                    break;

                case Trigger_Section.AOE:
                    local_Tutorial_Manager.Trigger_AOE_Tutorial();
                    break;

                case Trigger_Section.SHIELD:
                    local_Tutorial_Manager.Trigger_Shield_Tutorial();
                    break;

                case Trigger_Section.RUNES:
                    local_Tutorial_Manager.Trigger_Runes_Tutorial();
                    break;

                case Trigger_Section.HEALTH:
                    local_Tutorial_Manager.Trigger_Health_Fountain_Tutorial();
                    break;

                case Trigger_Section.MIXING:
                    local_Tutorial_Manager.Trigger_Mixing_Crystal_Tutorial();
                    break;

                case Trigger_Section.END:
                    if (!ended_Tutorial)
                    {
                        local_Tutorial_Manager.StartCoroutine("end_Tutorial");
                        ended_Tutorial = true;
                    }

                    break;
            }

            gameObject.SetActive(false);
        }
    }
}
