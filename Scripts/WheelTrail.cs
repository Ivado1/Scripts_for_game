using UnityEngine;
using UnityEngine.SceneManagement;

public class WheelTrail : MonoBehaviour
{
    private float groundCheckDistance;
    private TrailRenderer trailRenderer;
    public Transform PlaceRayBegin;
    private LayerMask whichFloor;

    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        groundCheckDistance = 0.48f;

        if (SceneManager.GetActiveScene().buildIndex >= 1 && SceneManager.GetActiveScene().buildIndex <= 15)
        {
            whichFloor = LayerMask.GetMask("Grass");
            trailRenderer.startColor = new Color32(97, 148, 34, 150); //RGB Alpha(255 Transparancy)
            //Debug.Log("Current Scene Grass");
        }
        if (SceneManager.GetActiveScene().buildIndex >= 16 && SceneManager.GetActiveScene().buildIndex <= 30)
        {
            whichFloor = LayerMask.GetMask("Desert");
            trailRenderer.startColor = new Color32(195,91,39,150); //RGB Alpha(255 Transparancy)
            //Debug.Log("Current Scene Desert");
        }
        if (SceneManager.GetActiveScene().buildIndex >= 31 && SceneManager.GetActiveScene().buildIndex <= 45)
        {
            whichFloor = LayerMask.GetMask("Snow");
            trailRenderer.startColor = new Color32(184, 197, 200, 150); //RGB Alpha(255 Transparancy)
            trailRenderer.endColor = new Color32(164, 177, 190, 150); //RGB Alpha(255 Transparancy)
            //Debug.Log("Current Scene Snow");
        }
    }
    void Update()
    {
        Ray ray = new Ray(PlaceRayBegin.position, -transform.up * groundCheckDistance);
        RaycastHit hit;

        if (Physics.Raycast(PlaceRayBegin.position, -transform.up, out hit, groundCheckDistance, whichFloor))
        {
            trailRenderer.emitting = true;
            //Debug.Log("Touch");
        }
        else
        {            
            trailRenderer.emitting = false;
            //Debug.Log("OFF-Touch");
        }

    }
}
