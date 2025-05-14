using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] Object[] unbrokenObjects, brokenObjects;

    private bool _furnitureDestroyable = false;
    private bool _wallsDestroyable = false;
    private bool _floorDestroyable = false;
    private bool _propsDestroyable = false;
    
    [SerializeField] private levelState _levelState;
    

    public bool FurnitureDestroyable
    {
        get
        {
            if (GameObject.FindGameObjectsWithTag("Mob").Length == 0)
            {
                _furnitureDestroyable = true;
            }
            return _furnitureDestroyable;
        }
        set
        {
            _furnitureDestroyable = value;
        }
    }

    public bool PropsDestroyable
    {
        get
        {
            if (GameObject.FindGameObjectsWithTag("Props").Length == 0)
            {
                _propsDestroyable = true;
            }
            return _propsDestroyable;
        }
        set
        {
            _propsDestroyable = value;
        }  
    }
    public bool WallsDestroyable
    {
        get
        {
            if (GameObject.FindGameObjectsWithTag("Furniture").Length == 0)
            {
                _wallsDestroyable = true;
            }
            return _wallsDestroyable;
        }
        set
        {
            _wallsDestroyable = value;
        }
    }

    public bool FloorDestroyable
    {
        get
        {
            if (GameObject.FindGameObjectsWithTag("Walls").Length == 0)
            {
                _floorDestroyable = true;
            }
            return _floorDestroyable;
        }
        set
        {
            _floorDestroyable = value;
        }
    }

    private void Update()
    {
        
    }

    private void Start()
    {
        StartCoroutine(setDestroySign());
    }

    private IEnumerator setDestroySign()
    {
        Debug.Log("Summoning DESTROY Sign");
        yield return new WaitForSeconds(3);
        
    }

    private void Awake()
    {
        _levelState = new levelState();
        _levelState.state = 0;
        _levelState.levelFinished = false;
        _levelState.destructableGameObjects = GameObject.FindGameObjectsWithTag("Mob");
        if (instance == null) // Makes the class a singleton
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make the GameObject persistent
        }
        else
        {
            //Destroy(gameObject);
        }
        
        unbrokenObjects = Resources.LoadAll(Path.Combine("Prefabs", "Unbroken"), typeof(GameObject));
        foreach (var obj in unbrokenObjects)
        {
            GameObject go = obj as GameObject;
            if (go.TryGetComponent(out Rigidbody rb) && go != null)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
            
        }
    }

    
    private struct levelState
    {
        public int state;
        public GameObject[] destructableGameObjects;
        public bool levelFinished;
    }
}