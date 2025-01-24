using UnityEngine;

public class GoalPositionTester : MonoBehaviour
{
    public GameObject cube;
    public Transform goal;
     void Start()
    {
       GameObject go= Instantiate(cube);
      go.transform.position = new Vector3(goal.position.x - 3.35f, goal.position.y + 1f, goal.position.z); // Sol üst
        GameObject go2 = Instantiate(cube);
        go2.transform.position = new Vector3(goal.position.x + 3.35f, goal.position.y + -1.5f, goal.position.z);  // Sağ üst


        GameObject go3 = Instantiate(cube);
        go3.transform.position = new Vector3(goal.position.x - 2f, goal.position.y + 1f, goal.position.z); // Sol üst
        GameObject go4 = Instantiate(cube);
        go4.transform.position = new Vector3(goal.position.x + 2f, goal.position.y + 1f, goal.position.z);  // Sağ üst


        GameObject go5= Instantiate(cube);
        go5.transform.position = new Vector3(goal.position.x, goal.position.y + 2f, goal.position.z - 2f);

    }
}
