using System.Collections;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Camera_Controller : MonoBehaviour
{
    #region variables
    [Header("References To Be Set")]
    public GameObject player_GameObject;
    [Header("Properties To Be Set")]
    public float x_Offset;
    public float z_Offset;
    public float max_Distance_To_Player;
    public float player_Velocity;

    public PostProcessingBehaviour local_PostProcessingBehaviour;

    private float x_Movement;
    private float z_Movement;
    #endregion

    /// <summary>
    /// Sets camera to follow the players position with an added offset
    /// </summary>
    void LateUpdate()
    {

        x_Movement = ((player_GameObject.transform.position.x + x_Offset - transform.position.x)) / max_Distance_To_Player;
        z_Movement = ((player_GameObject.transform.position.z + z_Offset - transform.position.z)) / max_Distance_To_Player;

        transform.position += new Vector3((x_Movement * player_Velocity * Time.deltaTime), 0, (z_Movement * player_Velocity * Time.deltaTime));
        transform.LookAt(player_GameObject.transform);

    }

    /// <summary>
    /// Applies chromatic aberration effect to the camera over time
    /// </summary>
    /// <returns></returns>
    public IEnumerator Chromatic()
    {

        local_PostProcessingBehaviour.profile.chromaticAberration.enabled = true;
        local_PostProcessingBehaviour.profile.chromaticAberration.Set_Intensity(1f);

        yield return new WaitForSeconds(.1f);
        local_PostProcessingBehaviour.profile.chromaticAberration.Set_Intensity(.9f);
        yield return new WaitForSeconds(.1f);
        local_PostProcessingBehaviour.profile.chromaticAberration.Set_Intensity(.8f);
        yield return new WaitForSeconds(.1f);
        local_PostProcessingBehaviour.profile.chromaticAberration.Set_Intensity(.7f);
        yield return new WaitForSeconds(.1f);
        local_PostProcessingBehaviour.profile.chromaticAberration.Set_Intensity(.6f);
        yield return new WaitForSeconds(.1f);
        local_PostProcessingBehaviour.profile.chromaticAberration.Set_Intensity(.5f);
        yield return new WaitForSeconds(.1f);
        local_PostProcessingBehaviour.profile.chromaticAberration.Set_Intensity(.4f);
        yield return new WaitForSeconds(.1f);
        local_PostProcessingBehaviour.profile.chromaticAberration.Set_Intensity(.3f);
        yield return new WaitForSeconds(.1f);
        local_PostProcessingBehaviour.profile.chromaticAberration.Set_Intensity(.2f);
        yield return new WaitForSeconds(.1f);
        local_PostProcessingBehaviour.profile.chromaticAberration.Set_Intensity(.1f);
        yield return new WaitForSeconds(.1f);
        local_PostProcessingBehaviour.profile.chromaticAberration.enabled = false;
    }
}
