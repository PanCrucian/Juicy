using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PlayerCamera : MonoBehaviour {

    public Camera mainCamera;
    public float smooth = 1f;
    public Vector3 cameraOffset = new Vector3(0, 0, -30);

    private Vector3 positionVelo;
    private Player player;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        if(player == null)
            player = GameObject.FindObjectOfType<Player>();

        Vector3 toDotPosition = new Vector3(
            player.currentDot.transform.position.x,
            player.currentDot.transform.position.y,
            0f
            );

        if (!Application.isPlaying || smooth == 0)
            mainCamera.transform.position = toDotPosition + cameraOffset;
        else
            mainCamera.transform.position = Vector3.SmoothDamp(
                mainCamera.transform.position,
                toDotPosition + cameraOffset, 
                ref positionVelo, 
                smooth * Time.deltaTime);
    }
}
