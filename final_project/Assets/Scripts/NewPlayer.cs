using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewPlayer : MonoBehaviour
{

    public float movementSpeed;
    public GameObject camera;

    public GameObject playerObj;

    public GameObject bulletSpawnPoints;
    public float waitTime;
    public GameObject bullet;

    public float points;

    public float maxHealth;
    public float health;

    private Text healthText;
    private Text pointText;

    void Start()
    {
        health = maxHealth;
        healthText = GameObject.Find ("HealthText").GetComponent<Text>();
        pointText = GameObject.Find("PointsText").GetComponent<Text>();

    }

    void Update()
    {
        

        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;

        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            playerObj.transform.rotation = Quaternion.Slerp(playerObj.transform.rotation, targetRotation, 7f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))       
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);

        
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (health <=0)
        {
            Die();
        }

        if (points == 2000)
        {
            Win();
        }

        healthText.text = health.ToString();
        pointText.text = points.ToString();


    }

    void Shoot()
    {
        Instantiate(bullet.transform, bulletSpawnPoints.transform.position, bulletSpawnPoints.transform.rotation);
    }

    public void Die()
    {
        SceneManager.LoadScene(1);
    }

    public void Win()
    {
        SceneManager.LoadScene(2);
    }

    
}
