using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

public class UnbrokenObjects : GameManager
{
    [SerializeField] private GameObject brokenVersion;
    private SFXManager sFXManager;
    private Random random;
    

    private void Start()
    {
        sFXManager = FindAnyObjectByType<SFXManager>();
        random = new Random();
    }
    public void isAttacked()
    {
        if (GetComponent<RadioBehaviour>() == null || 
           (GetComponent<RadioBehaviour>() != null && RadioBehaviour.hitOnce)) 
           //Evaluates if the game object is the radio. If it is but it hasn't been hit yet, it will set hitOnce = true, meaning second hit on radio will destroy it
        {
            //Instantiates the broken version of the object with the same transform
            Vector3 parentPos = gameObject.transform.position;
            Vector3 parentScale = gameObject.transform.localScale;
            quaternion parentRotation = gameObject.transform.rotation;
            brokenVersion.transform.localScale = parentScale;
            GameObject instatiatedBrokenVersion = (GameObject) Instantiate((GameObject)brokenVersion, parentPos, parentRotation);

                for (int i = 0; i < (instatiatedBrokenVersion.transform.childCount - 1); i++)
                {
                    instatiatedBrokenVersion.transform.GetChild(i).AddComponent<Rigidbody>();
                    instatiatedBrokenVersion.transform.GetChild(i).GetComponent<Rigidbody>().angularVelocity = 
                    new Vector3(random.Next(-10, 10), random.Next(-10, 10), random.Next(-20, 20));
                }
                
            
            Destroy(gameObject);

            sFXManager.PlayHitSound();
        }
        if (GetComponent<RadioBehaviour>() != null)
        {
            RadioBehaviour.hitOnce = true;
        }
    }
}
