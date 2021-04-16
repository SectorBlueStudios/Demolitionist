using UnityEngine;

public class AddExplosive : MonoBehaviour {

    public GameObject bomb;
    public GameObject flyingCube;

public void AddBomb()
    {
        Instantiate(bomb);
    }
}
