using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject render;
    
    // Start is called before the first frame update
    void Start()
    {
        render.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
