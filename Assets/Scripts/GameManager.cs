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
    

    public bool FurnitureDestroyable
    {
        get
        {
            bool containsNonEmptyObjects = false;
            foreach (var obj in GameObject.FindGameObjectsWithTag("Mob"))
            {
                if (obj != null)
                {
                    containsNonEmptyObjects = true;
                    break;
                }
            }
            if (!containsNonEmptyObjects)
            {
                _propsDestroyable = true;
                Debug.Log($"Props destroyable: {_propsDestroyable}");
            }
            return _propsDestroyable;
        }
        set
        {
            _propsDestroyable = value;
        }
    }
    public bool FurnitureDestroyable
    {
        get
        {
            bool containsNonEmptyObjects = false;
            foreach (var obj in GameObject.FindGameObjectsWithTag("Props"))
            {
                if (obj != null)
                {
                    containsNonEmptyObjects = true;
                    break;
                }
            }
            if (!containsNonEmptyObjects)
            {
                _furnitureDestroyable = true;
                Debug.Log("Furniture is destroyable!");
            }
            return _furnitureDestroyable;
        }
        set
        {
            _furnitureDestroyable = value;
        }
    }
    public bool WallsDestroyable
    {
        get
        {
            bool containsNonEmptyObjects = false;
            foreach (var obj in GameObject.FindGameObjectsWithTag("Furniture"))
            {
                if (obj != null)
                {
                    containsNonEmptyObjects = true;
                    break;
                }
            }
            if (!containsNonEmptyObjects)
            {
                _wallsDestroyable = true;
                Debug.Log("Walls are destroyable!");
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
            bool containsNonEmptyObjects = false;
            foreach (var obj in GameObject.FindGameObjectsWithTag("Walls"))
            {
                if (obj != null)
                {
                    containsNonEmptyObjects = true;
                    break;
                }
            }
            if (!containsNonEmptyObjects)
            {
                _floorDestroyable = true;
                Debug.Log("Furniture is destroyable!");
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