using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject cpOn, cpOff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetSpawnPoint(transform.position);



            CheckPoint[] allCP = FindObjectsOfType<CheckPoint>();
            for (int i = 0; i < allCP.Length; i++)
            {
                if (allCP[i] != this)
                {
                    allCP[i].cpOn.SetActive(false);
                    allCP[i].cpOff.SetActive(true);
                }
            }

            cpOff.SetActive(false);
            cpOn.SetActive(true);
        }
    }
}
