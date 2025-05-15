using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] AudioSource sFXSource;
    [SerializeField] AudioSource walkingSoundSource;
    private static SFXManager instance;

    private AudioClip[] dialogueAv;
    private AudioClip[] doorSounds;
    private AudioClip[] femmeAv;
    private AudioClip[] hitSounds;
    private AudioClip[] mascAv;
    private AudioClip[] swingSounds;
    private AudioClip[] toiletDoorSounds;
    private AudioClip walkingSound;

    private bool isWalkingSoundPlaying = false;

    System.Random rand = new(); //RNG used for playing random sound from folders of sounds
    int currentRandNum;
    int previousRandNum;

    // Makes the game object persist between scenes
    private void Awake()
    {
        if (instance == null) //Makes the class a singleton
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make the GameObject persistent
        }
        else
        {
            Destroy(gameObject);
        }
        //Adds AudioSource to the game object and assigns it to sFXSource. Gets all SFX from their folders
        //sFXSource = gameObject.AddComponent<AudioSource>();
        dialogueAv = Resources.LoadAll<AudioClip>("SFX/dialogueAv");
        doorSounds = Resources.LoadAll<AudioClip>("SFX/doorSounds");
        femmeAv = Resources.LoadAll<AudioClip>("SFX/femmeAv");
        hitSounds = Resources.LoadAll<AudioClip>("SFX/hitSounds");
        mascAv = Resources.LoadAll<AudioClip>("SFX/mascAv");
        swingSounds = Resources.LoadAll<AudioClip>("SFX/swingSounds");
        toiletDoorSounds = Resources.LoadAll<AudioClip>("SFX/toiletDoorSounds");

        //Adds AudioSource to the game object and assigns it to walkingSound
        //walkingSoundSource = gameObject.AddComponent<AudioSource>();
        walkingSound = Resources.Load<AudioClip>("SFX/gålyd2"); // Gets the walking sound SFX
    }
    public void PlayWalkingSound(bool isWalking)
    {
        if (isWalking)
        {
            if (!isWalkingSoundPlaying)
            {
                walkingSoundSource.clip = walkingSound;
                walkingSoundSource.Play();
                walkingSoundSource.loop = true;
                isWalkingSoundPlaying = true;
            }
        }
        else
        {
            if (isWalkingSoundPlaying)
            {
                walkingSoundSource.Stop();
                walkingSoundSource.loop = false;
                isWalkingSoundPlaying = false;
            }
        }
    }
    /// <summary>
    /// Uses upperLimit to return a number a non-negative number lower than upperLimit. Will never return the same number twice in a row
    /// </summary>
    /// <param name="upperLimit"></param>
    /// <returns></returns>
    public int NonRepeatingRNG(int upperLimit)
    {
        if (upperLimit < 2) return 0;
        int currentRandNum;
        do {
            currentRandNum = rand.Next(upperLimit);
        }
        while (currentRandNum == previousRandNum);

        previousRandNum = currentRandNum;
        return currentRandNum;
    }
    public void PlayFemmeAvSound()
    {
        sFXSource.PlayOneShot(femmeAv[NonRepeatingRNG(femmeAv.Length)]);
    }
    public void PlayHitSound()
    {
        sFXSource.PlayOneShot(hitSounds[NonRepeatingRNG(hitSounds.Length)]);
    }
    public void PlayMascAvSound()
    {
        sFXSource.PlayOneShot(mascAv[NonRepeatingRNG(mascAv.Length)]);
    }
    public void PlaySwingSound()
    {
        sFXSource.PlayOneShot(swingSounds[NonRepeatingRNG(swingSounds.Length)]);
    }
    public void PlayDoorSound()
    {
        sFXSource.PlayOneShot(doorSounds[NonRepeatingRNG(doorSounds.Length)]);
    }
    public void PlayToiletDoorSound()
    {
        sFXSource.PlayOneShot(toiletDoorSounds[NonRepeatingRNG(toiletDoorSounds.Length)]);
    }
    //public void PlayFemmeAvSound()
    //{
    //    currentRandNum = rand.Next(femmeAv.Length);

    //    if (currentRandNum == previousRandNum)
    //    {
    //        PlayFemmeAvSound();
    //        return;
    //    }
    //    sFXSource.PlayOneShot(femmeAv[currentRandNum]);

    //    previousRandNum = currentRandNum;
    //}
    //public void PlayHitSound()
    //{
    //    currentRandNum = rand.Next(hitSounds.Length);

    //    if (currentRandNum == previousRandNum)
    //    {
    //        PlayHitSound();
    //        return;
    //    }
    //    sFXSource.PlayOneShot(hitSounds[currentRandNum]);

    //    previousRandNum = currentRandNum;
    //}
    //public void PlayMascAvSound()
    //{
    //    currentRandNum = rand.Next(mascAv.Length);

    //    if (currentRandNum == previousRandNum)
    //    {
    //        PlayMascAvSound();
    //        return;
    //    }
    //    sFXSource.PlayOneShot(mascAv[currentRandNum]);

    //    previousRandNum = currentRandNum;
    //}
    //public void PlaySwingSound()
    //{
    //    currentRandNum = rand.Next(swingSounds.Length);

    //    if (currentRandNum == previousRandNum)
    //    {
    //        PlaySwingSound();
    //        return;
    //    }
    //    sFXSource.PlayOneShot(swingSounds[currentRandNum]);

    //    previousRandNum = currentRandNum;
    //}
    //public void PlayToiletDoorSound()
    //{
    //    currentRandNum = rand.Next(toiletDoorSounds.Length);

    //    if (currentRandNum == previousRandNum)
    //    {
    //        PlayToiletDoorSound();
    //        return;
    //    }
    //    sFXSource.PlayOneShot(toiletDoorSounds[currentRandNum]);

    //    previousRandNum = currentRandNum;
    //}
}