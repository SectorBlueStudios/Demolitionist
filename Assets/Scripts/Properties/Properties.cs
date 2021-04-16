using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SwitchPrefab))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(SFXVolume))]

public class Properties : MonoBehaviour {

    [Tooltip("How durable is this object?")]
    public float initialDurability = 100.0f;
    public float currentDurability = 100.0f;
    
    [Tooltip("Check this box if it IS a fragment of the original object.")]
    public bool root = false;
    public bool pieceBroken = false;

    public bool particles = false;
    public GameObject particlesystem;

    private float despawnTime = 1.5f;

    private float middleDamage = 0.75f;

    private bool destroyPiece = false;

    private float t = 0.0f;

    private float scaleX;
    private float scaleY;
    private float scaleZ;

    public float timeSave = -2f;
    public bool timeTrigger = true;

    public float audioDuration;

    public GameObject sfxController;

    public GameObject tempBomb;

    public GameObject audioPlayer;
    public GameObject audiobufferGameObject;
    public enum objectState                                     
    {
        state1,     //Used to know which state the object is in.  1 = Original                     
        state2,     //                                            2 = Damaged
        state3      //                                            3 = Destroyed
    };
    public objectState objectstate;

    public List<GameObject> explosives; //Holds which explosives affected it. Used because explosives should only be used ONCE.
     
    
    public void Reset()
    {
        //GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<MeshCollider>().convex = true;
    }

    public void OnValidate()
    {
        sfxController = GameObject.Find("SFX_Controller");
        audioPlayer = GameObject.Find("SFX_Player_Null");
    }

    public void Start()
    {
        //Debug.Log(currentDurability);
        if (pieceBroken == true && root == false) //the root of S3 contains the final audio clip. This should not run for the root.
        {
            GameObject temp = transform.parent.gameObject;
            transform.parent = null;
            Invoke("DestroyPieces", despawnTime);
        }
        if (objectstate == objectState.state1)
        {
            currentDurability = initialDurability;
        }
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;

        if (GetComponent<AudioSource>() != null)    //sets the audio duration to destroy the object.
            audioDuration = GetComponent<AudioSource>().clip.length;

        if (pieceBroken == true && GetComponent<AudioSource>() != null) //this initiates once the destructible changes states. One will have pieceBroken as true. REFINE THIS MAYBE?
            sfxController.GetComponent<sfxcontroller>().AddDestructible(gameObject);
    }

    public void DealDamage(float damage, GameObject bomb)
    {
        if (explosives.Contains(bomb) == false && gameObject.tag == "Destructible") //If the explosive affected it, don't do damage again with the same one.
        {
            explosives.Add(bomb);   //Add the explosive's GameObject to the list.

            if (root == true)
            {

                if (transform.GetChild(0) != null)
                {
                    if (transform.GetChild(0).gameObject != null)
                        transform.GetChild(0).gameObject.SetActive(true); //Activates the HealthBar.cs script.
                }
                if(transform.GetComponentInChildren<HealthBar>() != null)
                    transform.GetComponentInChildren<HealthBar>().ActivateHealthBar();

                currentDurability -= damage;   //Decrease the item's durability.
                if(currentDurability < 0)
                {
                    currentDurability = 0;
                }
                //Debug.Log(currentDurability);  //Check durability. (DEBUGGING)
                //Debug.Log(initialDurability);  //Check durability. (DEBUGGING)
                if (GetComponent<AudioSource>() != null)
                    sfxController.GetComponent<sfxcontroller>().AddDestructible(gameObject); //SFX

                if(currentDurability <= 0 && objectstate == objectState.state1)
                {
                    objectstate = objectState.state3;
                    particles = true;
                    gameObject.GetComponent<SwitchPrefab>().Switch2(particles);
                }
                else if (currentDurability < initialDurability * middleDamage && objectstate == objectState.state1)
                {
                    objectstate = objectState.state2;
                    gameObject.GetComponent<SwitchPrefab>().Switch1();
                }
                else if (currentDurability <= 0 && objectstate == objectState.state2)
                {
                    objectstate = objectState.state3;
                    gameObject.GetComponent<SwitchPrefab>().Switch2(particles);
                }            
            }
        }
    }

    public void CreateAudioObjects()
    {        
        audiobufferGameObject = Instantiate(audioPlayer);
        audiobufferGameObject.GetComponent<AudioSource>().clip = GetComponent<AudioSource>().clip;
        if(GetComponent<SFXVolume>() != null)
            audiobufferGameObject.GetComponent<AudioSource>().volume = (GetComponent<SFXVolume>().volume) / 100;
        //Debug.Log(audiobufferGameObject.GetComponent<AudioSource>().volume);
        audiobufferGameObject.GetComponent<AudioSource>().Play();
        Destroy(audiobufferGameObject, audioDuration + 0.5f);
    }


    private void DestroyPieces()
    {
        destroyPiece = true;
    }

    public void FixedUpdate()
    {
        if(destroyPiece == true)
        {
            transform.localScale = new Vector3(Mathf.Lerp(scaleX, 0, t), Mathf.Lerp(scaleY, 0, t), Mathf.Lerp(scaleZ, 0, t));
            t += 5f * Time.deltaTime;
            if(t >= 1.0f)
            {
                Destroy(gameObject);
            }
        }
        if (transform.position.y < -2 && (objectstate == objectState.state1 || objectstate == objectState.state2))
        {                
            currentDurability = 0;
            DestroyPieces();
        }
    }

    public void AddNewBomb()
    {
        Instantiate(tempBomb);
    }
        
    
    
    void OnCollisionEnter(Collision collision) //FOR WHEN OBJECTS HIT EACH OTHER
    {
        if (collision.rigidbody != null && !collision.gameObject.CompareTag("Explosive"))
        {
            if (root == true)
            {
                //Debug.Log(collision.gameObject);

                if (collision.rigidbody.velocity.magnitude > 0.5f && timeTrigger == true)
                {
                    timeSave = Time.time;
                    transform.GetChild(0).gameObject.SetActive(true); //Activates the HealthBar.cs script.
                    if (transform.GetComponentInChildren<HealthBar>() != null)
                      transform.GetComponentInChildren<HealthBar>().ActivateHealthBar();

                    //Debug.Log(currentDurability);
                    //Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);
                    collision.gameObject.GetComponent<Properties>().currentDurability -= collision.rigidbody.velocity.magnitude; //Object hitting.
                    currentDurability -= GetComponent<Rigidbody>().velocity.magnitude;
                    //Debug.Log(currentDurability);

                    if(GetComponent<AudioSource>() != null)
                        sfxController.GetComponent<sfxcontroller>().AddDestructible(gameObject); //SFX

                    timeTrigger = false;                    
                }

                if (Time.time >= timeSave + .5f) //Can only cause Damage to an object in a .5sec period. After .5Sec, it can Damage it again.
                {
                    timeTrigger = true;
                }

                if (currentDurability < 0)
                {
                    currentDurability = 0;
                }

                if (currentDurability <= 0 && objectstate == objectState.state1)
                {
                    objectstate = objectState.state3;
                    particles = true;
                    gameObject.GetComponent<SwitchPrefab>().Switch2(particles);
                }
                else if (currentDurability < initialDurability * middleDamage && objectstate == objectState.state1)
                {
                    objectstate = objectState.state2;
                    gameObject.GetComponent<SwitchPrefab>().Switch1();
                }
                else if (currentDurability <= 0 && objectstate == objectState.state2)
                {
                    objectstate = objectState.state3;
                    gameObject.GetComponent<SwitchPrefab>().Switch2(particles);
                }
            }
        }
    }
}