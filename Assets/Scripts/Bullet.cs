using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    int bulletTouched = 0;

    int lastHitEnemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator ShowRagdollAnim(Enemy animPlayingEnemy)
    {
        yield return new WaitForSeconds(1f);

        animPlayingEnemy.gameObject.SetActive(false);
        CameraController.Instance.targetEnemies.Enqueue(animPlayingEnemy);
        CameraController.Instance.GenerateEnemy();
        Debug.Log("Anim Ends");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && other.gameObject.activeInHierarchy) { 
            Enemy hitEnemy = CameraController.Instance.activeEnemy;
            foreach (Rigidbody part in hitEnemy.BodyParts)
            {
                part.isKinematic = false;
            }
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            StartCoroutine(ShowRagdollAnim(hitEnemy));
        }
    }
}
