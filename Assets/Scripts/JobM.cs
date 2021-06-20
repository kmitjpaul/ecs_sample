using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
using static UnityEngine.Random;

public class JobM : MonoBehaviour
{
    private TransformAccessArray ar;

    private Job job;
    private JobHandle jobH;
    public GameObject go;

    private int _spawnedCount;

    // Start is called before the first frame update
    private void Start()
    {
        ar = new TransformAccessArray(0);
    }

    // Update is called once per frame
    private void Update()
    {
        jobH.Complete();

        ProcessSpawn();

        job = new Job
        {
            rotV = insideUnitSphere
        };

        job.Schedule(ar);

        JobHandle.ScheduleBatchedJobs();
    }

    private void ProcessSpawn()
    {
        if (!Input.GetKeyDown("space")) return;

        var i = 0;
        for (; i <= _spawnedCount; i++)
        {
            var spawnedObj = Instantiate(go.transform);

            spawnedObj.position = insideUnitSphere * 5;

            ar.Add(spawnedObj);
            // _rotVectors.Add(insideUnitSphere);
        }

        _spawnedCount += i;
        Debug.Log(_spawnedCount);
    }
    
    private void OnDisable()
    {
        jobH.Complete();
        ar.Dispose();
    }
}

public struct Job : IJobParallelForTransform
{
    public Vector3 rotV;

    public void Execute(int index, TransformAccess transform)
    {
        transform.rotation *= Quaternion.AngleAxis(1f, rotV);
    }
}