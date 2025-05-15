using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private bool _furnitureDestroyable = false;
    private bool _wallsDestroyable = false;
    private bool _floorDestroyable = false;
    private bool _propsDestroyable = false;

    private void Awake()
    {
        if (instance == null) // Makes the class a singleton
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool PropsDestroyable
    {
        get
        {
            var containsNonEmptyObjects = false;
            foreach (var obj in GameObject.FindGameObjectsWithTag("Mob"))
                if (obj != null)
                {
                    containsNonEmptyObjects = true;
                    break;
                }

            if (!containsNonEmptyObjects)
            {
                _propsDestroyable = true;
                Debug.Log($"Props destroyable: {_propsDestroyable}");
            }

            return _propsDestroyable;
        }
        set => _propsDestroyable = value;
    }

    public bool FurnitureDestroyable
    {
        get
        {
            var containsNonEmptyObjects = false;
            foreach (var obj in GameObject.FindGameObjectsWithTag("Props"))
                if (obj != null)
                {
                    containsNonEmptyObjects = true;
                    break;
                }

            if (!containsNonEmptyObjects)
            {
                _furnitureDestroyable = true;
                Debug.Log("Furniture is destroyable!");
            }

            return _furnitureDestroyable;
        }
        set => _furnitureDestroyable = value;
    }

    public bool WallsDestroyable
    {
        get
        {
            var containsNonEmptyObjects = false;
            foreach (var obj in GameObject.FindGameObjectsWithTag("Furniture"))
                if (obj != null)
                {
                    containsNonEmptyObjects = true;
                    break;
                }

            if (!containsNonEmptyObjects)
            {
                _wallsDestroyable = true;
                Debug.Log("Walls are destroyable!");
            }

            return _wallsDestroyable;
        }
        set => _wallsDestroyable = value;
    }

    public bool FloorDestroyable
    {
        get
        {
            var containsNonEmptyObjects = false;
            foreach (var obj in GameObject.FindGameObjectsWithTag("Walls"))
                if (obj != null)
                {
                    containsNonEmptyObjects = true;
                    break;
                }

            if (!containsNonEmptyObjects)
            {
                _floorDestroyable = true;
                Debug.Log("Furniture is destroyable!");
            }

            return _floorDestroyable;
        }
        set => _floorDestroyable = value;
    }
}