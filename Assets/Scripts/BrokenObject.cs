using UnityEngine;

public class BrokenObject : MonoBehaviour
{
    public GameObject[] pieces;
    private int allowedPieces = 50;
    private Rigidbody rb;
    private float timePassed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.AddComponent(typeof(Rigidbody));
        rb = GetComponent<Rigidbody>();
        timePassed = 0;
        
        rb.useGravity = false;
        
        pieces = new GameObject[allowedPieces];
        // for (int i = 0; i < allowedPieces; i++)
        // {
        //     pieces[i] = transform.GetChild(i).gameObject;
        //     Rigidbody pieceRB = pieces[i].AddComponent<Rigidbody>();
        //     BoxCollider pieceBC = pieces[i].AddComponent<BoxCollider>();
        //     pieceRB.useGravity = true;
        //     pieceRB.angularVelocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        //     pieceBC.enabled = true;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= 5.0f)
        {
            //Destroy(gameObject);
        }
    }
}
