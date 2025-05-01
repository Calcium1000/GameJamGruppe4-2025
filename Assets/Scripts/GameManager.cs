using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private bool _furnitureDestroyable = false;
    private bool _wallsDestroyable = false;
    private bool _floorDestroyable = false;

    public bool FurnitureDestroyable
    {
        get
        {
            if (GameObject.FindGameObjectsWithTag("Femme mob").Length == 0 && GameObject.FindGameObjectsWithTag("masc mob").Length == 0)
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

    private void Awake()
    {
        if (instance == null) // Makes the class a singleton
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make the GameObject persistent
        }
        else
        {
            Destroy(gameObject);
        }
    }
}