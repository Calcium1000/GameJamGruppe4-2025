using UnityEngine;

public class UnbrokenObjects : GameManager
{
    [SerializeField] private GameObject brokenVersion;


    void Start()
    {
        
    }
    
    public void isAttacked()
    {
        brokenVersion.SetActive(true);
        gameObject.SetActive(false);
        Instantiate(brokenVersion, Vector3.zero, Quaternion.identity);
        DestroyImmediate(gameObject, true);
    }
}
