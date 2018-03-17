using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ShadowTrail : MonoBehaviour
{
    #region publicVariables

    [Tooltip("Indicates if the shadow trail is active or not")]
    public bool isShadowTrailActive;
    [Tooltip("Max Number of objects that we are allowed to use to generate a shadow trail")]
    public int maxNumberOfObjectsInPool = 30;
    [Tooltip("Indicates the time that the shadow will be active in the scene")]
    public float shadowTrailDestroyTime = .275f;
    [Range(0.0f, 1.0f)]
    [Tooltip("Alpha that will be used on the generated shadow sprite renderer")]
    public float shadowStartingAlpha = .25f;
    #endregion publicVariables

    SpriteRenderer mySpriteRenderer; //Reference to the attached sprite renderer. If no sprite renderer is attached this script will be destroyed on the setup method
    GameObject shadowsParent; //Reference to a child object that will be instantiated during the setup, this object will hold all other objects that will be used to generate the shadow trail
    List<GameObject> shadowObjectPool; //List of GameObjects that will be used to handle the shadow objects - Using object pool system to save resources.
   


    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start()
    {
        ///Call to setup method.
        Setup();
    }

    /// <summary>
    /// This method  will do the require setup in order to make this script work
    /// </summary>
    void Setup()
    {
        //Search for the Sprite Renderer attached to this game object - if no sprite renderer is attached this script will be destroyed to avoid errors.
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        if (mySpriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer component found on this game object " + gameObject.name + " Destroying ShadowTrail component");
            Destroy(this);
        }

        
            
        
        //Generate the new List that will hold all the 'shadow' game objects
        shadowObjectPool = new List<GameObject>();
        //Create the parent object that will hold all the shadows (GameObjects)
        shadowsParent = new GameObject("ShadowsPoolParent For " + gameObject.name);

        //Instantiate the max number of objects that can be used to generate shadows.
        shadowsParent.transform.position = Vector3.zero;
        for (int i = 0; i < maxNumberOfObjectsInPool; i++)
        {
            GameObject shadow = GenerateShadow();
            //Add the shadow to the object pool list:
            shadowObjectPool.Add(shadow);
            //Assign the parent of the new shadow - this is only to maintain all shadows inside a GameObject
            shadow.transform.parent = shadowsParent.transform;
        }

    }

    /// <summary>
    /// Called every frame if the behaviour is enabled
    /// </summary>
    private void LateUpdate()
    {
        //if isShadowTraiActive we call the co routine 'GenerateShadowTrail'
        //if not we stop all coroutines and deactivate all shadows in pool
        if (isShadowTrailActive)
        {
            StartCoroutine("GenerateShadowTrail");
        }
        else
        {
            StopAllCoroutines();
            DeactivateAllShadows();
        }
    }


    /// <summary>
    /// Coroutine in charge of activate and deactivate objects that will generate the shadow trail
    /// </summary>
    /// <returns></returns>
    public IEnumerator GenerateShadowTrail()
    {
        GameObject shadow;
        shadow = GetShadowFromPool();

        if (shadow != null)
        {
            ShadowSetup(shadow);
            shadow.SetActive(true);
            StartCoroutine(ShadowDestroy(shadow));
        }

        yield return null;
    }

    /// <summary>
    /// Receive a GameObject with a SpriteRenderer component then it will be configured in order to generate the shadow trail
    /// </summary>
    /// <param name="shadow"></param>
    void ShadowSetup(GameObject shadow)
    {
        ///Sice we don't have access to set the global scale of an object, we assing a null parent to the shadow object, assign the desired scale (equals to the object this script is attached) and then re assign it to his parent
        ///Unity will convert the local scale.
        shadow.transform.parent = null;
        shadow.transform.localScale = transform.localScale;
        shadow.transform.parent = shadowsParent.transform;

        ///GetComponent SpriteRenderer from the shadow object and assign the values of mySpriteRenderer at this frame
        SpriteRenderer shadowSpRenderer = shadow.GetComponent<SpriteRenderer>();
        shadowSpRenderer.flipX = mySpriteRenderer.flipX;
        shadow.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        shadow.transform.rotation = Quaternion.identity;
        shadowSpRenderer.sprite = mySpriteRenderer.sprite;

        //Assign the desired alpha to the shadow
        Color alpha = new Color(mySpriteRenderer.color.r, mySpriteRenderer.color.g, mySpriteRenderer.color.b, shadowStartingAlpha);
        shadowSpRenderer.color = alpha;

    }

    /// <summary>
    /// Coroutine in charge of deactivating the shadows
    /// It will wait the time specified in shadowTrailDestroyTime before deactivating the game object
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    IEnumerator ShadowDestroy(GameObject obj)
    {
        yield return new WaitForSeconds(shadowTrailDestroyTime);
        obj.transform.parent = shadowsParent.transform;
        obj.SetActive(false);

    }

    /// <summary>
    /// This method helps to retrieve a game object from the shadowObjectPool
    /// </summary>
    /// <returns></returns>
    GameObject GetShadowFromPool()
    {
        for (int i = 0; i < shadowObjectPool.Count; i++)
        {
            if (!shadowObjectPool[i].activeSelf)
                return shadowObjectPool[i];
        }

        return null;
    }

    /// <summary>
    /// This method helps to create an object with the required components to create the shadow
    /// </summary>
    /// <returns></returns>
    GameObject GenerateShadow()
    {
        GameObject go = new GameObject("shadow" + shadowObjectPool.Count);
        go.AddComponent<SpriteRenderer>();
        go.SetActive(false);
        return go;
    }

    /// <summary>
    /// This method immediately deactivate all game objects in the ShadowObjectPool
    /// </summary>
    void DeactivateAllShadows()
    {
        foreach (GameObject go in shadowObjectPool)
        {
            go.SetActive(false);
        }
    }
}


