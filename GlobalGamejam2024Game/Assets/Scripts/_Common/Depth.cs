using NaughtyAttributes;
using System.Threading.Tasks;
using UnityEngine;

public class Depth : MonoBehaviour
{
    [SerializeField, Foldout("Controller")] bool useSpriteRenderer = true, useParticleSystem = false,useTrailRenderer = false;

    SpriteRenderer tempRend = null;
    ParticleSystemRenderer tempPS = null;
    TrailRenderer trailRenderer = null;
    public int baseDepth = 0;
    public bool isGetParent = false;
    [OnValueChanged("EnableCustomParent")]public bool useCustomParent = false;
    [ShowIf("useCustomParent")]public Transform customParent = null;
    void EnableCustomParent()
    {
        if (!useCustomParent)
        {
            customParent = null;
        }
    }
    [Header("Only for player")]
    public bool hasEquipment = false;
    public int baseDepthForEquipment = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (useSpriteRenderer)
            tempRend = GetComponent<SpriteRenderer>();
        if (useParticleSystem)
            tempPS = GetComponent<ParticleSystem>().GetComponent<ParticleSystemRenderer>();
        if (useTrailRenderer)
            trailRenderer = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tempPS != null && useParticleSystem)
        {
            tempPS.sortingOrder = baseDepth + (int)Camera.main.WorldToScreenPoint(this.isGetParent ? (useCustomParent?customParent : this.transform.parent).position : this.transform.position).y * -1;
        }
        if (useSpriteRenderer)
        {
            if (!hasEquipment)
                tempRend.sortingOrder = baseDepth + (int)Camera.main.WorldToScreenPoint(this.isGetParent ? (useCustomParent ? customParent : this.transform.parent).position : this.transform.position).y * -1;
            else
                UpdateDepth();
        }
        if (useTrailRenderer)
        {
            trailRenderer.sortingOrder = baseDepth + (int)Camera.main.WorldToScreenPoint(this.isGetParent ? (useCustomParent ? customParent : this.transform.parent).position : this.transform.position).y * -1;
        }
    }

    async void UpdateDepth()
    {
        Task task1 = loadDepth();

        Task task2 = loadDepthEquipment();

        await Task.WhenAll(task1, task2);

    }

    async Task loadDepth()
    {
        tempRend.sortingOrder = baseDepth + (int)Camera.main.WorldToScreenPoint(this.isGetParent ? this.transform.parent.position : this.transform.position).y * -1;
        await Task.Delay(200);
    }

    async Task loadDepthEquipment()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = tempRend.sortingOrder + baseDepthForEquipment;
        await Task.Delay(200);
    }
}
