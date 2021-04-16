using UnityEngine;

public class TutorialChangeText : MonoBehaviour
{
    public int currentChild;
    public bool isMain;
    public GameObject[] tutorialArray;

    //Disables the current child and enables the next child.
    public void NextText()
    {
        transform.parent.GetChild(transform.parent.GetComponent<TutorialChangeText>().currentChild).gameObject.SetActive(false); //0
        SetCurrentChild();
        transform.parent.GetChild(currentChild).gameObject.SetActive(true); //1
    }

    //Sets the child so it knows the next child it needs to enable.
    public void SetCurrentChild()
    {
        transform.parent.GetComponent<TutorialChangeText>().currentChild += 1;
        currentChild = transform.parent.GetComponent<TutorialChangeText>().currentChild;
    }

    //Creates a new array of the Children's GameObjects
    public void ResetNames()
    {
        tutorialArray = new GameObject[100];
        int i = 0;
        while (i < transform.childCount)
        {
            tutorialArray[i] = transform.GetChild(i).gameObject;
            i++;
        }
    }

    //If it is a child, first check its name, if it has a " - " in it, remove it. Then re-add it with the corresponding child number.
    public void ChangeNames()
    {
        if (!isMain)
        {
            char[] oldNameReverter = new char[200];

            string gameObjectName;
            gameObjectName = gameObject.name;
            for (int k = 0; k < 7; k++)
            {
                int p = k + 1;
                int m = k + 2;

                if (gameObjectName[k] == ' ' && gameObjectName[p] == '-' && gameObjectName[m] == ' ')
                {
                    for (int g = m + 1, f = 0; g < gameObjectName.Length; g++, f++)
                    {
                        oldNameReverter[f] = gameObjectName[g];
                    }
                }
            }
            string tempString = new string(oldNameReverter);
            if (oldNameReverter[0] != 0)
                gameObject.name = new string(oldNameReverter);

            transform.parent.GetComponent<TutorialChangeText>().ResetNames();

            for (int j = 0; j < transform.parent.childCount; j++)
            {
                if (transform.parent.GetComponent<TutorialChangeText>().tutorialArray[j] != null && gameObject.name == transform.parent.GetComponent<TutorialChangeText>().tutorialArray[j].gameObject.name)
                {
                    gameObject.name = j + " - " + gameObject.name;
                }
            }            
            Debug.Log("Created Numbers");
        }
    }

    //If it is a child, first check its name, if it has a " - " in it, remove it. Then re-add it with the corresponding child number.
    public void ChangeNamesClear()
    {
        if (!isMain)
        {
            char[] oldNameReverter = new char[200];

            string gameObjectName;
            gameObjectName = gameObject.name;
            for (int k = 0; k < 7; k++)
            {
                int p = k + 1;
                int m = k + 2;

                if (gameObjectName[k] == ' ' && gameObjectName[p] == '-' && gameObjectName[m] == ' ')
                {
                    for (int g = m + 1, f = 0; g < gameObjectName.Length; g++, f++)
                    {
                        oldNameReverter[f] = gameObjectName[g];
                    }
                }
            }
            string tempString = new string(oldNameReverter);
            if (oldNameReverter[0] != 0)
                gameObject.name = new string(oldNameReverter);

            transform.parent.GetComponent<TutorialChangeText>().ResetNames();

            Debug.Log("Cleared Numbers");
        }
    }

    //Method to add numbers in front of the name.
    public void InitiateChangeNames()
    {
        tutorialArray = new GameObject[100];

        if (isMain == true)
        {
            int i = 0;
            while (i < transform.childCount && transform.GetChild(i) != null)
            {
                tutorialArray[i] = transform.GetChild(i).gameObject;
                i++;
            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            tutorialArray[i].gameObject.GetComponent<TutorialChangeText>().ChangeNames();
        }
    }

    //Method to remove numbers in front of the name.
    public void InitiateChangeNamesClear()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            tutorialArray[i].gameObject.GetComponent<TutorialChangeText>().ChangeNamesClear();
        }
    }
}