using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class sfxcontroller : MonoBehaviour
{
    public List<GameObject> projectileList = new List<GameObject>();
    public List<GameObject> explosiveList = new List<GameObject>();

    public List<GameObject> destructibleList = new List<GameObject>();

    public GameObject testObject;

    public void AddExplosive(GameObject explosive)
    {
        if (explosive.GetComponent<Explosion>().explosiveType == Explosion.ExplosiveType.standard ||
           explosive.GetComponent<Explosion>().explosiveType == Explosion.ExplosiveType.sticky)
        {
            explosiveList.Add(explosive);
        }

        else if (explosive.GetComponent<Explosion>().explosiveType == Explosion.ExplosiveType.projectile)
        {
            projectileList.Add(explosive);
        }
    }

    public void RemoveExplosive(GameObject explosive)
    {
        if (explosive.GetComponent<Explosion>().explosiveType == Explosion.ExplosiveType.standard ||
            explosive.GetComponent<Explosion>().explosiveType == Explosion.ExplosiveType.sticky)
        {
            explosiveList.Remove(explosive);
        }

        else if (explosive.GetComponent<Explosion>().explosiveType == Explosion.ExplosiveType.projectile)
        {
            projectileList.Remove(explosive);
        }
    }

    public void CalculateSounds()
    {
        if (explosiveList.Count > 5)
        {
            List<GameObject> sortedList = new List<GameObject>();
            sortedList = explosiveList.OrderByDescending(o => o.GetComponent<Explosion>().explosivePowerIndicator).ToList();

            List<GameObject> doublySortedList = new List<GameObject>();
            doublySortedList.Add(sortedList.ElementAt(0));

            int explosivePowerTracker = doublySortedList.ElementAt(0).GetComponent<Explosion>().explosivePowerIndicator;
            int explosivePowerCount = 1;
            for (int i = 1; i < sortedList.Count; i++)
            {
                if (sortedList.ElementAt(i).GetComponent<Explosion>().explosivePowerIndicator == explosivePowerTracker && explosivePowerCount < 4)
                {
                    doublySortedList.Add(sortedList.ElementAt(i));
                    explosivePowerCount++;
                }
                else if (sortedList.ElementAt(i).GetComponent<Explosion>().explosivePowerIndicator != explosivePowerTracker)
                {
                    doublySortedList.Add(sortedList.ElementAt(i));
                    explosivePowerTracker = doublySortedList.ElementAt(0).GetComponent<Explosion>().explosivePowerIndicator;
                    explosivePowerCount = 1;
                }
                if (doublySortedList.Count == 5)
                    break;
            }

            while (doublySortedList.Count < 5)
            {
                doublySortedList.Add(doublySortedList.ElementAt(doublySortedList.Count - 1));
            }

            foreach (GameObject gameObject in doublySortedList)
            {
                gameObject.GetComponent<Explosion>().CreateAudioObjects(); //Creates GameObjects that will play the explosion sound. This is because the system destroys the explosive otherwise and the audio stops.
            }
        }
        else
        {
            foreach (GameObject gameObject in explosiveList)
            {
                gameObject.GetComponent<Explosion>().CreateAudioObjects(); //Creates GameObjects that will play the explosion sound. This is because the system destroys the explosive otherwise and the audio stops.
            }
            explosiveList = new List<GameObject>(); //Clears the list after Demolish is triggered.
        }

        if (projectileList.Count > 3)
        {
            List<GameObject> sortedList = new List<GameObject>();
            sortedList = projectileList.OrderByDescending(o => o.GetComponent<Explosion>().explosivePowerIndicator).ToList();

            List<GameObject> doublySortedList = new List<GameObject>();
            doublySortedList.Add(sortedList.ElementAt(0));

            int explosivePowerTracker = doublySortedList.ElementAt(0).GetComponent<Explosion>().explosivePowerIndicator;
            int explosivePowerCount = 1;
            for (int i = 1; i < sortedList.Count; i++)
            {
                if (sortedList.ElementAt(i).GetComponent<Explosion>().explosivePowerIndicator == explosivePowerTracker && explosivePowerCount < 2)
                {
                    doublySortedList.Add(sortedList.ElementAt(i));
                    explosivePowerCount++;
                }
                else if (sortedList.ElementAt(i).GetComponent<Explosion>().explosivePowerIndicator != explosivePowerTracker)
                {
                    doublySortedList.Add(sortedList.ElementAt(i));
                    explosivePowerTracker = doublySortedList.ElementAt(0).GetComponent<Explosion>().explosivePowerIndicator;
                    explosivePowerCount = 1;
                }
                if (doublySortedList.Count == 3)
                    break;
            }

            while (doublySortedList.Count < 3)
            {
                doublySortedList.Add(doublySortedList.ElementAt(doublySortedList.Count - 1));
            }

            foreach (GameObject gameObject in doublySortedList)
            {
                gameObject.GetComponent<Explosion>().CreateAudioObjects(); //Creates GameObjects that will play the explosion sound. This is because the system destroys the explosive otherwise and the audio stops.
            }
        }
        else
        {
            foreach (GameObject gameObject in projectileList)
            {
                gameObject.GetComponent<Explosion>().CreateAudioObjects(); //Creates GameObjects that will play the explosion sound. This is because the system destroys the explosive otherwise and the audio stops.
            }
        }

        explosiveList = new List<GameObject>();
        projectileList = new List<GameObject>(); //Clears the list after Demolish is triggered.
    }

    public void AddDestructible(GameObject destructible)
    {
        int sameObjectLimit = 2;
        int totalObjectLimit = 8;

        if (destructibleList.Count <= totalObjectLimit)
        {
            int sameObjectCount = 1;
            foreach (GameObject sameObject in destructibleList)
            {
                if (sameObject.GetComponent<AudioSource>().clip == destructible.GetComponent<AudioSource>().clip)
                    sameObjectCount++;
            }

            if (sameObjectCount <= sameObjectLimit)
            {
                destructible.GetComponent<Properties>().CreateAudioObjects();                
                GameObject tempDestructible = RetrieveTempDestructible(destructible);                
                destructibleList.Add(tempDestructible);
                StartCoroutineAudioClip(destructible, tempDestructible);
                testObject = tempDestructible; //for test purposes
                
                //destructible.GetComponent<AudioSource>().Play();
                //destructible.GetComponent<Properties>().StartCoroutineAudioClip();
            }
        }
    }

    public GameObject RetrieveTempDestructible(GameObject destructible)
    {
        return destructible.GetComponent<Properties>().audiobufferGameObject;
    }

    public void RemoveDestructible(GameObject destructible)
    {
        destructibleList.Remove(destructible);
    }

    public IEnumerator WaitForAudioClip(float audioDuration, GameObject tempDestructible)
    {
        //Debug.Log("HI!");
        yield return new WaitForSeconds(audioDuration);
        RemoveDestructible(tempDestructible);
    }

    public void StartCoroutineAudioClip(GameObject destructible, GameObject tempDestructible)
    {
        StartCoroutine(WaitForAudioClip(destructible.GetComponent<Properties>().audioDuration, tempDestructible));
    }
}