using System.Collections;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour
{
    public GameObject SpawnPoint;
    public GameObject DarkCoffeePrefab;
    public GameObject WhiteCoffeePrefab;

    private bool running;
    private UIManager uimanager;
    private void Start()
    {
        uimanager = FindObjectOfType<UIManager>();
    }
    void Update()
    {
        if (running == false && triggerIsEmpty)
        {
            Ray ray = Camera.main.ScreenPointToRay(
              new Vector3(Screen.width / 2, Screen.height / 2)
            );

            if (Physics.Raycast(ray, out RaycastHit hit, 2)) // 2 is maximum distance
            {
                if (hit.collider.gameObject.CompareTag("White Coffee Button"))
                {
                    uimanager.MakeCoffeeHint.SetActive(true);
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartCoroutine(MakingCoffeeCor(WhiteCoffeePrefab));
                    }
                }
                else if (hit.collider.gameObject.CompareTag("Dark Coffee Button"))
                {
                    uimanager.MakeCoffeeHint.SetActive(true);
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartCoroutine(MakingCoffeeCor(DarkCoffeePrefab));
                    }
                }
                else
                {
                    uimanager.MakeCoffeeHint.SetActive(false);
                }
            }
        }
        else
        {
            uimanager.MakeCoffeeHint.SetActive(false);
        }
    }
    IEnumerator MakingCoffeeCor(GameObject objToSpawn)
    {
        running = true;
        yield return new WaitForSeconds(5);
        Instantiate(objToSpawn, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
        running = false;
    }

    private bool triggerIsEmpty = true;
    private void OnTriggerEnter(Collider other)
    {
        triggerIsEmpty = false;
    }
    private void OnCollisionStay(Collision collision)
    {
        triggerIsEmpty = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        triggerIsEmpty = true;
    }
}
