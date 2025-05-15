using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

public class UnbrokenObjects : GameManager
{
    [SerializeField] private GameObject brokenVersion;
    protected SFXManager sFXManager;
    protected static Random random;


    private void Start()
    {
        sFXManager = FindAnyObjectByType<SFXManager>();
        random = new Random();
    }

    public virtual void IsAttacked()
    {
        //Instantiates the broken version of the object with the same transform
        var parentPos = gameObject.transform.position;
        var parentScale = gameObject.transform.localScale;
        quaternion parentRotation = gameObject.transform.rotation;
        brokenVersion.transform.localScale = parentScale;
        var instantiatedBrokenVersion = (GameObject)Instantiate((GameObject)brokenVersion, parentPos, parentRotation);

        for (var i = 0; i < instantiatedBrokenVersion.transform.childCount - 1; i++)
        {
            instantiatedBrokenVersion.transform.GetChild(i).AddComponent<Rigidbody>();
            instantiatedBrokenVersion.transform.GetChild(i).GetComponent<Rigidbody>().angularVelocity =
                new Vector3(random.Next(-10, 10), random.Next(-10, 10), random.Next(-20, 20));
            instantiatedBrokenVersion.gameObject.tag = "Broken object";
        }

        sFXManager.PlayHitSound();
        Destroy(gameObject);
    }
}