using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class Classic : MonoBehaviour
{
    public GameObject go;

    private readonly List<Transform> _transforms = new List<Transform>();
    private readonly List<Vector3> _rotVectors = new List<Vector3>();

    private int _spawnedCount;

    void Update()
    {
        ProcessSpawn();
        ProcessRotation();
    }

    private void ProcessSpawn()
    {
        if (!Input.GetKeyDown("space")) return;

        var i = 0;
        for (; i <= _spawnedCount; i++)
        {
            var spawnedObj = Instantiate(go.transform);

            spawnedObj.position = insideUnitSphere * 5;

            _transforms.Add(spawnedObj);
            _rotVectors.Add(insideUnitSphere);
        }

        _spawnedCount += i;
        Debug.Log(_spawnedCount);
    }


    private void ProcessRotation()
    {
        var isNewRot = Input.GetKeyDown("space");

        if (isNewRot)
        {
            Debug.Log(_spawnedCount);
        }

        for (var i = 0; i < _transforms.Count; i++)
        {
            var t = _transforms[i];
            var rotV = _rotVectors[i];

            if (isNewRot)
            {
                rotV = insideUnitSphere;
                _rotVectors[i] = rotV;
            }

            t.rotation *= Quaternion.AngleAxis(1f, rotV); 
            
            // t.Rotate(rotV);
        }
    }
}