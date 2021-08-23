using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Queue <Enemy> targetEnemies;

    public Enemy activeEnemy;

    public Enemy enemyPrefab;

    public Bullet bulletPrefab;

    public GameObject gun;

    public Material hitMaterial;

    public static CameraController Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        targetEnemies = new Queue<Enemy>();
        for (int i = 0; i < 10; i++)
        {
            Enemy newEnemy = Instantiate(enemyPrefab);
            newEnemy.gameObject.SetActive(false);
            targetEnemies.Enqueue(newEnemy);
        }
        GenerateEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeEnemy != null)
        {
            var targetRotation = Quaternion.LookRotation(
                new Vector3(
                    activeEnemy.transform.position.x - transform.position.x,
                    0,
                    activeEnemy.transform.position.z - transform.position.z
               )
            );
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(r.origin, r.direction * 10, Color.cyan);            
            Bullet bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.LookRotation(r.direction));
            bullet.GetComponent<Rigidbody>().velocity = r.direction * 10;
        }
    }

    private IEnumerator SmoothLerp(float time)
    {
        if (activeEnemy != null)
        {
            Vector3 startingPos = transform.position;
            Vector3 finalPos = new Vector3(activeEnemy.transform.position.x, activeEnemy.transform.position.y + 1.5f, activeEnemy.transform.position.z - 5);
            float elapsedTime = 0;

            while (elapsedTime < time)
            {
                transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }

    public void GenerateEnemy()
    {
        activeEnemy = targetEnemies.Dequeue();
        activeEnemy.transform.position = new Vector3( 
            (transform.position.x + Random.Range(-20,20)),
            0,
            (transform.position.z+ Random.Range(10, 20))
        ); 
        activeEnemy.gameObject.SetActive(true);
        StartCoroutine(SmoothLerp(3f));
    }
}
