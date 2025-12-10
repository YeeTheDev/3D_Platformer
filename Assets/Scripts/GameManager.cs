using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Vector3 respawnPosition;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        respawnPosition = PlayerController.Instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCO());
    }

    public IEnumerator RespawnCO()
    {
        PlayerController.Instance.gameObject.SetActive(false);

        CameraController.Instance.theCMBrain.enabled = false;

        yield return new WaitForSeconds(2f);

        PlayerController.Instance.transform.position = respawnPosition;

        CameraController.Instance.theCMBrain.enabled = true;

        PlayerController.Instance.gameObject.SetActive(true);
    }
}
