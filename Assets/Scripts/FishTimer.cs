using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishTimer : MonoBehaviour
{
    private PoplavokController pc;
    [SerializeField] private GameObject btns;
    [SerializeField] private GameObject poplavok;
    [SerializeField] GameObject prefabFish;
    private Vector3 pcTarget;
    [SerializeField] private Collider zone;
    private float timeToFailed = 4;
    [SerializeField] private TextMeshProUGUI timertxt;
    [SerializeField] private Image radialTimer;
    private float curMaxTF;
    [SerializeField] private Animator cat;
    private CatController catController;

    public static bool IsEnd { get; internal set; }

    private IEnumerator Start()
    {
        pc = FindObjectOfType<PoplavokController>();
        catController = FindObjectOfType<CatController>();
        pcTarget = pc.GetMainTPos();
        StartCoroutine(nameof(UpdatePCTargt));
        cat.Play("Start");
        catController.StartLine();
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(15, 31));
            if (!btns.activeInHierarchy && !IsEnd)
            {
                pc.Call();
                timeToFailed = Random.Range(3f, 10f);
                curMaxTF = timeToFailed;
                btns.gameObject.SetActive(true);
            }
        }
    }
    private IEnumerator UpdatePCTargt()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);
            pcTarget = CalculateSpawnPosition(zone.transform, zone);
        }
    }
    public Vector3 CalculateSpawnPosition(Transform transform, Collider collider)
    {
        var width = collider.bounds.size.x;
        var height = collider.bounds.size.y;
        var center = transform.position;
        return new Vector3(GetRandomDot(center.x, width, 0),
                            GetRandomDot(center.y, height, 0),
                            GetRandomDot(center.z, width, 0));
    }

    private float GetRandomDot(float center, float sideLength, float axisLowestLimit)
    {
        var LowestVertex = center + axisLowestLimit - (sideLength / 2);
        var HeighestVertex = center + (sideLength / 2);
        var RandomDot = Random.Range(LowestVertex, HeighestVertex);
        return RandomDot;
    }
    private void Update()
    {
        if (btns.activeInHierarchy)
            pc.GetMain().position = Vector3.MoveTowards(pc.GetMain().position, pcTarget, Time.deltaTime / 5);
        if (btns.activeInHierarchy)
        {
            timeToFailed -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpawnAFish();
                btns.SetActive(false);
                pc.ReturnAnimation();                
                cat.Play("Back");
                catController.Back();
            }
            if (timeToFailed <= 0)
            {
                btns.SetActive(false);
                pc.ReturnAnimation();
            }
            timertxt.text = ((int)timeToFailed).ToString();
            radialTimer.fillAmount = timeToFailed / curMaxTF;
        }
    }
    private void SpawnAFish()
    {
        var fish = Instantiate(prefabFish, poplavok.transform.position, Quaternion.identity);
        fish.AddComponent<Fish>().target = cat.transform.position;
        float insScale = Random.Range(1f, 6f);
        fish.transform.localScale *= insScale;
        //5 100
        float cost = (insScale / 6) * 100;
        FindObjectOfType<Shop>().AddMoney((int)cost);
    }
}
