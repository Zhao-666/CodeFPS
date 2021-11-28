using UnityEngine;

public class TestBullet : MonoBehaviour
{
    [Header("Impact Effect Prefabs")] public Transform[] metalImpactPrefabs;

    private Rigidbody rigidbody;
    private Vector3 velocity;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody.velocity = velocity = transform.forward * 400;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rigidbody.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter:"+collision.gameObject.tag);

        if (collision.transform.CompareTag("WoodTarget"))
        {
            Instantiate(metalImpactPrefabs[Random.Range(0, metalImpactPrefabs.Length)],
                transform.position,
                Quaternion.LookRotation(collision.contacts[0].normal),
                collision.gameObject.transform);
            transform.Translate(new Vector3(0, 0, 0.1f), Space.Self);
            rigidbody.velocity = velocity;
            return;
        }


        //If bullet collides with "Metal" tag
        if (collision.transform.CompareTag("Metal")
            || collision.transform.CompareTag("Target"))
        {
            //Instantiate random impact prefab from array
            Instantiate(metalImpactPrefabs[Random.Range(0, metalImpactPrefabs.Length)],
                transform.position,
                Quaternion.LookRotation(collision.contacts[0].normal),
                collision.gameObject.transform);
            //Destroy bullet object
            Destroy(gameObject);
        }

        //If bullet collides with "Target" tag
        if (collision.transform.CompareTag("Target"))
        {
            //Toggle "isHit" on target object
            collision.transform.gameObject.GetComponent
                <TargetScript>().isHit = true;
            //Destroy bullet object
            Destroy(gameObject);
        }

        //If bullet collides with "ExplosiveBarrel" tag
        if (collision.transform.CompareTag("ExplosiveBarrel"))
        {
            //Toggle "explode" on explosive barrel object
            collision.transform.gameObject.GetComponent
                <ExplosiveBarrelScript>().explode = true;
            //Destroy bullet object
            Destroy(gameObject);
        }
    }
}