using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    public Vector2 xBorders;
    public Vector2 zBorders;
    public float speed;
    public bool isCameraZoomed;
    public GameObject backButton;
    public GameObject startButton;
    public Vector3 lastPosition;
    public Vector3 lastRotation;
    public string sceneName;
    public TMPro.TextMeshProUGUI innNameText;

    public void UnlockCamera()
    {
        startButton.SetActive(false);
        backButton.SetActive(false);

        Camera.main.transform.position = lastPosition;
        Camera.main.transform.eulerAngles = lastRotation;

        isCameraZoomed = false;

        sceneName = "";
        innNameText.text = "";
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.None;

        innNameText.gameObject.SetActive(true);

        if (!isCameraZoomed)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(x, 0, z).normalized;

            if (direction.magnitude > 0)
            {
                transform.position += direction * speed * Time.deltaTime;

                transform.position = new Vector3(Mathf.Clamp(transform.position.x, xBorders.x, xBorders.y), transform.position.y, Mathf.Clamp(transform.position.z, zBorders.x, zBorders.y));
            }

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                var inn = hit.transform.gameObject.GetComponent<InnMap>();

                if (inn != null) 
                {
                    innNameText.text = inn.name;

                    if (Input.GetMouseButtonDown(0))
                    {
                        lastPosition = Camera.main.transform.position;
                        lastRotation = Camera.main.transform.eulerAngles;
                        
                        Camera.main.transform.position = hit.transform.position + Vector3.right * 0.4f + Vector3.up * 0.4f + Vector3.back * 0.4f;
                        Camera.main.transform.eulerAngles = new Vector3(40f, -40f, 0);

                        isCameraZoomed = true;

                        startButton.SetActive(true);
                        backButton.SetActive(true);

                        sceneName = inn.innScene;
                    }
                }
            }
            else
            {
                innNameText.gameObject.SetActive(false);
            }
        }
    }

}
