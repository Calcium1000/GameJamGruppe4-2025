using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

public class UnbrokenObjects : GameManager
{
    [SerializeField] protected GameObject brokenVersion;
    protected SFXManager sFXManager;
    protected static Random random;
    

    private void Start()
    {
        sFXManager = FindAnyObjectByType<SFXManager>();
        random = new();
    }
    public virtual void IsAttacked()
    {
        //Instantiates the broken version of the object with the same transform
        Vector3 parentPos = gameObject.transform.position;
        Vector3 parentScale = gameObject.transform.localScale;
        quaternion parentRotation = gameObject.transform.rotation;
        brokenVersion.transform.localScale = parentScale;
        GameObject instantiatedBrokenVersion = (GameObject)Instantiate((GameObject)brokenVersion, parentPos, parentRotation);

        for (int i = 0; i < (instantiatedBrokenVersion.transform.childCount - 1); i++)
        {
            instantiatedBrokenVersion.transform.GetChild(i).AddComponent<Rigidbody>();
            instantiatedBrokenVersion.transform.GetChild(i).GetComponent<Rigidbody>().angularVelocity =
            new Vector3(random.Next(-10, 10), random.Next(-10, 10), random.Next(-20, 20));
            instantiatedBrokenVersion.gameObject.tag = "Broken object";
        }
        Destroy(gameObject);
        sFXManager.PlayHitSound();
    }
}
