using UnityEngine;

public class UnbrokenObjects : GameManager
{
    [SerializeField] private GameObject brokenVersion;
    public bool isDestroyed = false;

    public void isAttacked()
    {
        if (GetComponent<RadioBehaviour>() == null || 
           (GetComponent<RadioBehaviour>() != null && RadioBehaviour.hitOnce)) 
           //Evaluates if the game object is the radio. If it is but it hasn't been hit yet, it will set hitOnce = true, meaning second hit on radio will destroy it
        {
            brokenVersion.SetActive(true);
            gameObject.SetActive(false);
            Instantiate(brokenVersion, Vector3.zero, Quaternion.identity);
            DestroyImmediate(gameObject, true);
        }
        if (GetComponent<RadioBehaviour>() != null)
        {
            RadioBehaviour.hitOnce = true;
        }
    }
}
