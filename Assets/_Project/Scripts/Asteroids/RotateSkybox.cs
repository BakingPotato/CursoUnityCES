using UnityEngine;

public class RotateSkybox : MonoBehaviour
{
    public float rotate_speed = 1.2f;

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", rotate_speed * Time.time);
    }
}
