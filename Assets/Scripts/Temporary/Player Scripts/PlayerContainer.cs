using UnityEngine;

public class PlayerContainer : MonoBehaviour
{
    [SerializeField] GameObject player_x;
    [SerializeField] GameObject player_y;

    private bool isActive = true;

    private void Start()
    {
        Swap();
    }

    private void Update()
    {
        if(player_y.activeSelf)
        {
            transform.position = player_y.transform.position;
        }
        else
        {
            transform.position = player_x.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.L)) Swap();
    }

    public void Swap()
    {
        player_x.SetActive(isActive);
        player_y.SetActive(!isActive);
        isActive = !isActive;
        GetComponent<PlayerController>().UpdateStats();
    }
}
