using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTesting : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {

        Mesh mesh = new Mesh();

        Vector3[] verts = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        verts[0] = new Vector3( 0   , 0   );
        verts[1] = new Vector3( 0   , 100 );
        verts[2] = new Vector3( 100 , 100 );
        verts[3] = new Vector3( 100 , 0   );

        uv[0] = new Vector2( 0, 0 );
        uv[1] = new Vector2( 0, 1 );
        uv[2] = new Vector2( 1, 1 );
        uv[3] = new Vector2( 1, 0 );

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        mesh.vertices = verts;
        mesh.uv = uv;
        mesh.triangles = triangles;



        GetComponent<MeshFilter>( ).mesh = mesh;

    }
   
}
