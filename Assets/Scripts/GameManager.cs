using System;
using System.IO;
using Mono.Cecil;
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

    private bool[] thingsDestroyable;
    
    [SerializeField] private levelState _levelState;
    

    public bool FurnitureDestroyable
    {
        get
        {
            if (_levelState.destructableGameObjects.Length == 0)
            {
                _furnitureDestroyable = true;
                _levelState.state++;
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
    
    private void Awake()
    {
        thingsDestroyable = new bool[4]
            { _furnitureDestroyable, _wallsDestroyable, _floorDestroyable, _propsDestroyable };
        _levelState = new levelState
        {
            state = 0,
            levelFinished = false,
            
        };
        Debug.Log("There are " + _levelState.destructableGameObjects.Length + " mobs");
        
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

        void setDstructableGameObjects()
        {
            switch (state)
            {
                case 0:
                    destructableGameObjects = GameObject.FindGameObjectsWithTag("Mob");
                    break;
                case 1: destructableGameObjects = GameObject.FindGameObjectsWithTag("Props"); 
                    break;
                case 2: destructableGameObjects = GameObject.FindGameObjectsWithTag("Furniture");
                    break;
                case 3: destructableGameObjects = GameObject.FindGameObjectsWithTag("Walls");
                    break;
                
            }
        }
    }
}