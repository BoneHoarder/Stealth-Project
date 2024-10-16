using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorModeControl : MonoBehaviour
{     
    [SerializeField] bool visible;
    [SerializeField] CursorLockMode lockmode;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState= lockmode;
        Cursor.visible= visible;
    }

    
}
