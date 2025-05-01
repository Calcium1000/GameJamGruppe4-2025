using System.IO;
using System.Linq;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;
using Application = UnityEngine.Application;
using Object = System.Object;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UnityEngine.Object[] unbrokenObject;
    
    private string unbrokenObjectFolder;
    private GameObject go;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unbrokenObjectFolder = Path.Combine("Prefabs", "Unbroken");
        unbrokenObject = Resources.LoadAll(unbrokenObjectFolder, typeof(GameObject));
        
        //Instantiate(go, Vector3.zero, Quaternion.identity);
        
        foreach (var obj in unbrokenObject)
        {
            if (obj.GameObject().TryGetComponent<Rigidbody>(out Rigidbody rb) == false)
            {
                obj.GameObject().AddComponent<Rigidbody>();
            }
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     go = unbrokenObject[Random.Range(0, unbrokenObject.Length-1)].GameObject();
        //     Instantiate(go, Vector3.zero, Quaternion.identity);
        //     go.GetComponent<UnbrokenObjects>().isAttacked();
        // }
    }
}
