using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// This class is to be attached to each of the explosive prefabs in the game.
/// It allows the user to tweak the important properties and modify the results.
/// </summary>
/// 

[RequireComponent(typeof(SFXVolume))]
public class Explosion : MonoBehaviour
{
    public GameObject bomb; //Main bomb object to be used.
    public GameObject projectile; //Projectile object used to travel.

    public GameObject explosionFX; //The particle system attached to be instantiated after explosion.
    private bool explodeOnce = true; //Used so the explosionFX only happens once.

    /// <summary>
    /// Main explosion properties. This differs for each explosive.
    /// </summary>
    //Power of the explosion (physics system)
    [Tooltip("Force of explosion")]
    public float power = 10.0f;

    //Damage the explosion causes to nearby objects.
    [Tooltip("Damage caused from outer ring")]
    public float damage = 5.0f;
    [Tooltip("Damage caused form inner ring")]
    public float damageCritical = 7.0f;

    //Length in meters from the center of the object.
    [Tooltip("Size of outer ring")]
    public float outerRadius = 5.0f;
    [Tooltip("Size of inner ring")]
    public float innerRadius = 2.0f;

    //Amount of force applied upwards.
    [Tooltip("This causes an upwards gravity modifier to be applied")]
    public float upforce = 0;

    //Length of the explosion.
    [Tooltip("How long the explosive explodes for (Seconds)")]
    public float explosionTime = 1.0f;
    [Tooltip("When does this explode? (Seconds)")]
    public float timer = 2.0f;

    //Cost of the explosive to be charged to the user.
    [Tooltip("How much the explosive costs")]
    public int cost;

    /// <summary>
    /// Explosion Visualizer
    /// </summary>
    [Tooltip("[DEBUGGING] Should this explode?")]
    public bool explode = false;
    [Tooltip("[DEBUGGING] Draw visualization rings")]
    public bool draw;


    /// <summary>
    /// Below are the main variables for the projectile explosive type
    /// </summary>
    private Vector3 startPos;
    private Vector3 endPos;

    /// <summary>
    /// These are used explicitly with the projectile methods.
    /// </summary>

    [Tooltip("Speed the projectile travels")]
    public float projectileSpeed = 1.0F;


        
    //Used to ensure the method is only initialized once.
    private bool runOnce = false;

    private float startTime;
    private float journeyLength;

    /// <summary>
    /// Creates a menu for the user to define which type of explosive is being used.
    /// </summary>
    public enum ExplosiveType //Type of explosive defined using a dropdown menu.
    {
        standard,
        sticky,
        projectile
    };
    public ExplosiveType explosiveType;

    public int explosivePowerIndicator; //How powerful is the explosive (1 - 6) This is for determining the SFX and what should be played.

    /// <summary>
    /// SFX variables
    /// </summary>
    public GameObject audioPlayer;                          //The Empty GameObject that is Instantiated when an Audio Clip needs to be played.
    public GameObject sfxController;                        //Game's SFX Controller. There is one in the scene to link to.
    GameObject audiobufferGameObject;                       //This is the GameObject that is instantiated to play SFX
    public AudioClip travelSFX;                             //This is the AudioClip for the falling whistle (optional - ONLY for Projectiles)
    public AudioClip explosionSFX;                          //This is the AudioClip for the Explosion (REQUIRED)
    public AudioSource audioSFX;                            //This is the AudioSource. This is needed to tie to the audiobufferGameObject,
    private bool projectileExplosionPlayed = false;         //This is so the Projectile Explosion SFX is only played once when it explodes

    /// <summary>
    /// Function runs every frame and is adapted to the physics system.
    /// </summary>
    void FixedUpdate ()
    {
        //Only occurs (each frame) if the currently selected explosive is of "sticky" type.
        if (explosiveType == ExplosiveType.sticky)
        {
            /*This function works by casting rays out of the object's right, left, forward, backward, and down directions.
             * If there is a hit, it stores the hit in a list. It then chooses a random hit from the list if there are more than one.
             * It then uses that hit and takes its normal. The normal is now the new rotation of the object.
            */
            List<RaycastHit> hits = new List<RaycastHit>();
            RaycastHit left, right, front, back, down;
            
            if(Physics.Raycast(new Vector3(transform.position.x + .001f, transform.position.y + .001f, transform.position.z), transform.TransformDirection(Vector3.left), out left, .005f))
                hits.Add(left);
            if (Physics.Raycast(new Vector3(transform.position.x - .001f, transform.position.y + .001f, transform.position.z), transform.TransformDirection(Vector3.right), out right, .005f))
                hits.Add(right);
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + .001f, transform.position.z - .001f), transform.TransformDirection(Vector3.forward), out front, .005f))
                hits.Add(front);
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + .001f, transform.position.z + .001f), transform.TransformDirection(Vector3.back), out back, .005f))
                hits.Add(back);
            if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out down, .005f))
                hits.Add(down);  
            
            int randVal;
            
            if(hits.Count() > 0)
            {
                randVal = Random.Range(0, hits.Count() - 1);
                bomb.transform.rotation = Quaternion.FromToRotation(Vector3.up, hits[randVal].normal);
            }
        }

        if (explode == true)
        {
            //==========STANDARD BOMB EXPLOSION METHOD============//
            if (explosiveType == ExplosiveType.standard)
            {
                Detonate();
                Invoke("DestroyExplosive", explosionTime);
            }

            //==========PROJECTILE BOMB EXPLOSION METHOD==========//
            else if (explosiveType == ExplosiveType.projectile)
            {
                if (!runOnce) //This is used just so things are initialized once.
                {
                    InitializeProjectile();
                    runOnce = true;
                }
                ProjectileJourney(); //This moves the projectile from start to finish.
            }

            //==========STICKY BOMB EXPLOSION METHOD==============//
            else if(explosiveType == ExplosiveType.sticky)
            {
                Detonate();
                Invoke("DestroyExplosive", explosionTime);
            }
        }
    }

    
    public void Awake() //Assigns it the SFX_Controller
    {
        sfxController = GameObject.Find("SFX_Controller");
    }

    /// <summary>
    /// Used for debugging. Explicitly used to show wireframes.
    /// </summary>
    void OnDrawGizmosSelected() 
    {
        if (draw == true) //Only shows if the "Draw" box is checked.
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, outerRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, innerRadius);
        }
    }

    /// <summary>
    /// Removes the explosive from the scene.
    /// </summary>
    void DestroyExplosive()
    {
        Destroy(bomb);
    }


    /// <summary>
    /// This is called from the sfxController. This creates GameObjects that play the sound of the Explosive. 
    /// This is because the original explosive's GameObjects are destroyed when DEMOLISH is pressed. It then removes it after X seconds.
    /// </summary>
    public void CreateAudioObjects()
    {
        audiobufferGameObject = Instantiate(audioPlayer);

        //Only projectiles will have travelSFX assigned
        if(GetComponent<Explosion>().travelSFX != null)
            audiobufferGameObject.GetComponent<Explosion>().travelSFX = GetComponent<Explosion>().travelSFX;

        audiobufferGameObject.GetComponent<Explosion>().explosionSFX = GetComponent<Explosion>().explosionSFX;
        audiobufferGameObject.GetComponent<Explosion>().audioSFX = audiobufferGameObject.GetComponent<AudioSource>();
        audiobufferGameObject.GetComponent<Explosion>().audioSFX.volume = (GetComponent<SFXVolume>().volume)/100;



        //if it is a Standard or a Sticky, it will assign explosionSFX to the AudioClip. Otherwise, it is a projectile and assigns travelSFX.
        if (GetComponent<Explosion>().travelSFX != null)
            audiobufferGameObject.GetComponent<Explosion>().audioSFX.clip = travelSFX;
        else
            audiobufferGameObject.GetComponent<Explosion>().audioSFX.clip = explosionSFX;

        audiobufferGameObject.GetComponent<Explosion>().audioSFX.Play();
        Destroy(audiobufferGameObject, 4f); //Destroys the GameObject after 4 seconds.
    }

    /// <summary>
    /// Detonates all explosives when the "Detonate" button is pressed. Called from an outside function.
    /// </summary>
    public void DemolishTrigger()
    {
        var explodeAll = GameObject.FindGameObjectsWithTag("Explosive");

        for (int i = 0; i < explodeAll.Length; i++)
        {
            explodeAll[i].GetComponent<Explosion>().explode = true;
        }
    }

    /// <summary>
    /// Main detonation function. Causes damage to nearby objects.
    /// </summary>
    void Detonate()
    {
        if (explodeOnce)
        {
            GameObject explodeFX = Instantiate(explosionFX, transform.position, Quaternion.identity);
            Destroy(explodeFX, 2);
        }
        explodeOnce = false;

        Vector3 explosionPosition = bomb.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, outerRadius, 1 << 10 );
        Collider[] colliders2 = Physics.OverlapSphere(explosionPosition, outerRadius/2, 1 << 10);

        foreach (Collider hit in colliders2) //INNER EXPLOSION
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null && rb.gameObject.tag != "Explosive")
            {
                rb.GetComponent<Properties>().DealDamage(damageCritical, bomb); //Deal damage to the objects around it.
                rb.AddExplosionForce(power * 2, explosionPosition, innerRadius, upforce, ForceMode.Impulse);
            }
        }
        foreach (Collider hit in colliders) //OUTER EXPLOSION
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if(rb != null && rb.gameObject.tag != "Explosive")
            {
                rb.GetComponent<Properties>().DealDamage(damage, bomb); //Deal damage to the objects around it.
                rb.AddExplosionForce(power, explosionPosition, outerRadius, upforce, ForceMode.Impulse);
            }
        }
    }

    /// <summary>
    /// Defines a random point for the projectile GameObject to spawn at.
    /// </summary>
    private Vector3 RandomPlane()
    {
        float randX = Random.Range(-5f, 5f);
        float randZ = Random.Range(-5f, 5f);
        return new Vector3(randX, 15, randZ);
    }

    /// <summary>
    /// Initializes the current projectile.
    /// </summary>
    private void InitializeProjectile()
    {
        var randPos = RandomPlane();                                            //Choose a random position in the sky to spawn the projectile at.
        projectile = Instantiate(projectile, randPos, Quaternion.identity); //Create the new GameObject (projectile) at a random point in the sky.

        startPos = randPos;                                                     //Start position is where the new GameObject is instantiated at.
        endPos = bomb.transform.position;                                       //End position is where the initial GameObject (flag) was dragged to.

        startTime = Time.time;                                                  //Sets the time for the Lerp.
        journeyLength = Vector3.Distance(startPos, endPos);                     //Calculates the distance of the Lerp.
    }

    /// <summary>
    /// Traverses the projectile from the start position to the end position.
    /// </summary>
    private void ProjectileJourney()
    {
        float distCovered = (Time.time - startTime) * projectileSpeed; //Lerp variables.
        float fracJourney = distCovered / journeyLength;     //Lerp vatiables.

        if (projectile != null)
        {
            projectile.transform.LookAt(bomb.transform);                                 //Faces the tip of the projectile towards the marker.
            projectile.transform.position = Vector3.Lerp(startPos, endPos, fracJourney); //Lerps the projectile to the marker.
        }

        if (fracJourney >= 1)
        {
            //Only Projectiles will trigger this. It stops the travelSFX, assigns the explosionSFX, and then plays the explosionSFX.
            if (audiobufferGameObject != null)
            {
                if (!projectileExplosionPlayed)
                {
                    audiobufferGameObject.GetComponent<Explosion>().audioSFX.Stop();
                    audiobufferGameObject.GetComponent<Explosion>().audioSFX.clip = explosionSFX;
                    audiobufferGameObject.GetComponent<Explosion>().audioSFX.Play();
                    projectileExplosionPlayed = true;
                }
            }
            Destroy(projectile);                    //Destroy the projectile GameObject the second it makes it to the destination.
            Detonate();                                 //When the projectile GameObject reaches the marker, the marker explodes and causes the explosion. (***NOTE: maybe change the upwards modifier to less than 0***)
            Invoke("DestroyExplosive", explosionTime);
        }
    }
}