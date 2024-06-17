using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject playerCamera; // Reference to the camera following the player
    public GameObject lockedCamera; // Reference to the locked camera
    public GameObject Block1;
    public GameObject Block2;
    public GameObject CameraSwitcher;
    public EnemyPhysics BossPhysics;

    private bool switched = false; // Flag to track if the camera has been switched

    void Start()
    {
        GameObject Boss = GameObject.Find("TreeBoss");
        BossPhysics = Boss.GetComponent<EnemyPhysics>();
    }

    void Update()
    {
        if(BossPhysics.died)
        {
            Block1.SetActive(false);

            Block2.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collides with the preset collider
        if (other.CompareTag("BossArena") && !switched)
        {
            Debug.Log("Player collided");
            // Disable the player camera
            playerCamera.SetActive(false);
            
            // Enable the locked camera
            lockedCamera.SetActive(true);

            Block1.SetActive(true);

            Block2.SetActive(true);

            CameraSwitcher.SetActive(false);
            
            // Set the flag to true
            switched = true;
        }
        else if (other.CompareTag("BossArenaExit") && switched)
        {
            Debug.Log("Player Exit");

            playerCamera.SetActive(true);
            
            // Enable the locked camera
            lockedCamera.SetActive(false);

            switched = false;
        }
    }
}
