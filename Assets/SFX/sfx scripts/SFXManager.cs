using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource WalkingSoundSource;
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
        dialogueAv = Resources.LoadAll<AudioClip>("SFX/dialogueAv");
        doorSounds = Resources.LoadAll<AudioClip>("SFX/doorSounds");
        femmeAv = Resources.LoadAll<AudioClip>("SFX/femmeAv");
        hitSounds = Resources.LoadAll<AudioClip>("SFX/hitSounds");
        mascAv = Resources.LoadAll<AudioClip>("SFX/mascAv");
        swingSounds = Resources.LoadAll<AudioClip>("SFX/swingSounds");
        toiletDoorSounds = Resources.LoadAll<AudioClip>("SFX/toiletDoorSounds");
        walkingSound = Resources.Load<AudioClip>("SFX/gålyd2");
    }
    public void PlayWalkingSound(bool isWalking)
    {
        if (isWalking)
        {
            if (!isWalkingSoundPlaying)
            {
                WalkingSoundSource.clip = walkingSound;
                WalkingSoundSource.Play();
                WalkingSoundSource.loop = true;
                isWalkingSoundPlaying = true;
            }
        }
        else
        {
            if (isWalkingSoundPlaying)
            {
                WalkingSoundSource.Stop();
                WalkingSoundSource.loop = false;
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
        currentRandNum = rand.Next(upperLimit);
        if (currentRandNum == previousRandNum) //Calls itself until it generates a new random number
        {
            NonRepeatingRNG(upperLimit);
            return currentRandNum;
        }
        previousRandNum = currentRandNum;
        return currentRandNum;
    }
    public void PlayFemmeAvSound()
    {
        SFXSource.PlayOneShot(femmeAv[NonRepeatingRNG(femmeAv.Length)]);
    }
    public void PlayHitSound()
    {
        SFXSource.PlayOneShot(hitSounds[NonRepeatingRNG(hitSounds.Length)]);
    }
    public void PlayMascAvSound()
    {
        SFXSource.PlayOneShot(mascAv[NonRepeatingRNG(mascAv.Length)]);
    }
    public void PlaySwingSound()
    {
        SFXSource.PlayOneShot(swingSounds[NonRepeatingRNG(swingSounds.Length)]);
    }
    public void PlayDoorSound()
    {
        SFXSource.PlayOneShot(doorSounds[NonRepeatingRNG(doorSounds.Length)]);
    }
    public void PlayToiletDoorSound()
    {
        SFXSource.PlayOneShot(toiletDoorSounds[NonRepeatingRNG(toiletDoorSounds.Length)]);
    }
    //public void PlayFemmeAvSound()
    //{
    //    currentRandNum = rand.Next(femmeAv.Length);

    //    if (currentRandNum == previousRandNum)
    //    {
    //        PlayFemmeAvSound();
    //        return;
    //    }
    //    SFXSource.PlayOneShot(femmeAv[currentRandNum]);

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
    //    SFXSource.PlayOneShot(hitSounds[currentRandNum]);

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
    //    SFXSource.PlayOneShot(mascAv[currentRandNum]);

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
    //    SFXSource.PlayOneShot(swingSounds[currentRandNum]);

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
    //    SFXSource.PlayOneShot(toiletDoorSounds[currentRandNum]);

    //    previousRandNum = currentRandNum;
    //}
}