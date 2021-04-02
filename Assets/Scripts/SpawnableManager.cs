using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawnableManager : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_RaycastManager;
    List<ARRaycastHit> m_hits = new List<ARRaycastHit>();
    [SerializeField]
    GameObject spawnablePrefab;

    GameObject spawnedObject;
    private Animator Anim;


    // Start is called before the first frame update
    void Start()
    {
        spawnedObject = null;
        Anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount.Equals(0)) return;

        if (m_RaycastManager.Raycast(Input.GetTouch(0).position,m_hits))
        {
            if (Input.GetTouch(0).phase.Equals(TouchPhase.Began))
            {
                SpawnPrefab(m_hits[0].pose.position);
            }
            else if (Input.GetTouch(0).phase.Equals(TouchPhase.Moved) && !spawnedObject.Equals(null))
            {
                Anim.SetBool("isRunning", true);
            }

            if (Input.GetTouch(0).phase.Equals(TouchPhase.Ended))
            {
                Anim.SetBool("isRunning", false);
            }
        }
    }

    private void SpawnPrefab(Vector3 spawnPosition)
    {
        spawnedObject = Instantiate(spawnablePrefab, spawnPosition, Quaternion.Euler(0,180,0));
    }

}
