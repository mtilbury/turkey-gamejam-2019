using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using PathCreation;
using PathCreation.Examples;

public class RealRoadCreator : MonoBehaviour
{
    public GameObject emptyHusk;
    private List<Transform> transforms;    

    public GameObject roadCreator;

    public GameObject player;
    private PathFollower playerPathFollower;
    private List<PathCreator> pathCreators;
    private List<Vector3> finalPositions;
    private List<RoadMeshCreator> roadMeshes;
    private int currentPath = 0;


    // Start is called before the first frame update
    void Start()
    {
        transforms = new List<Transform>();
        pathCreators = new List<PathCreator>();
        finalPositions = new List<Vector3>();
        roadMeshes = new List<RoadMeshCreator>();
        playerPathFollower = player.GetComponent<PathFollower>();

        // Get data
        string path = "Assets/Data/priceData.csv";

        StreamReader reader = new StreamReader(path);
        string priceDataRaw = reader.ReadToEnd();
        reader.Close();

        var priceData = priceDataRaw.Split('\n');
        //Transform[] transforms = new Transform[priceData.Length];
        

        float xDistance = 0.0f;
        //int curTransform = 0;
        foreach(string price in priceData)
        {
            if(price == "")
            {
                continue;
            }
            GameObject tempHusk = GameObject.Instantiate(emptyHusk);
            tempHusk.transform.position = new Vector3(xDistance, float.Parse(price) * 3, 0);
            transforms.Add(tempHusk.transform);
            xDistance += Random.Range(5, 20);
            //curTransform++;
        }

        updateBezier();

        // Create bezier paths
        // Get 5-10 points and make a path
        int last = 0;
        int next = 15;

        while (last + next < transforms.Count)
        {
            // Instantiate a PathCreator
            GameObject newRoadCreator = GameObject.Instantiate(roadCreator);

            PathCreator newPathCreator = newRoadCreator.GetComponent<PathCreator>();
            RoadMeshCreator newRoadMeshCreator = newRoadCreator.GetComponent<RoadMeshCreator>();

            List<Transform> transformSlice = transforms.GetRange(last, next);

            newPathCreator.bezierPath = new BezierPath(transformSlice);
            newRoadMeshCreator.updatePath();

            finalPositions.Add(transformSlice[transformSlice.Count - 1].position);
            pathCreators.Add(newPathCreator);
            roadMeshes.Add(newRoadMeshCreator);

            last += next;
            next = Random.Range(5, 10);

        }

        playerPathFollower.pathCreator = pathCreators[0];
    }

    public void updateBezier()
    {
        //BezierPath bezierPath = new BezierPath(transforms.GetRange(last, next));
        //pathCreator.bezierPath = bezierPath;
        //roadMeshCreator.updatePath();

        //meshCollider.sharedMesh = null;
        //meshCollider.sharedMesh = meshFilter.sharedMesh;

        //last += 15;
        //next += 15;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x >= pathCreators[currentPath].path.GetPointAtTime(1000, EndOfPathInstruction.Stop).x)
        {
            Debug.Log(pathCreators[currentPath].path.GetPointAtTime(1000, EndOfPathInstruction.Stop));
            // If the player exceeds the current path
            playerPathFollower.toggleFollow(false);

            player.GetComponent<PlayerController>().freeFall();

            roadMeshes[currentPath].meshHolder.GetComponent<MeshCollider>().enabled = false;

            currentPath++;
            playerPathFollower.pathCreator = pathCreators[currentPath];
        }
    }
}
