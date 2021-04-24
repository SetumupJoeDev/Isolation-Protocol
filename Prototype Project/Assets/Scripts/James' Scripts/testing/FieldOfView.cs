using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    [Header("Field Of View")]

    [Tooltip("The Mesh component of this GameObject that will be drawn to.")]
    public Mesh m_fieldOfViewMesh;

    [Tooltip("The physics layer that the walls sit. Used to obstruct the field of view.")]
    public LayerMask m_wallLayerMask;

    [Tooltip("The origin of the FOV mesh.")]
    public Vector3 m_fovOrigin;

    [Tooltip("The number of rays used to create the FOV. The more rays, the bigger the impact on performance, but the smoother the FOV shape.")]
    public int m_rayCount;

    [Tooltip("The width of the FOV, in a range between 0 and 180 degrees.")]
    [Range(0.0f, 180.0f)]
    public float m_fovWidth;

    [Tooltip("The furthest distance that this FOV will reach.")]
    public float m_viewDistance;

    private float m_startingAngle;

    // Start is called before the first frame update
    void Start( )
    {
        //Creates a new Mesh for the field of view to use
        m_fieldOfViewMesh = new Mesh();

        //Assigns the newly created mesh to this gameobject's mesh filter
        GetComponent<MeshFilter>( ).mesh = m_fieldOfViewMesh;

        //Sets the origin of the object
        m_fovOrigin = Vector3.zero;

    }

    public static Vector3 GetVectorFromAngle( float angle )
    {
        float angleRad = angle * (Mathf.PI/180f);
        return new Vector3( Mathf.Cos( angleRad ) , Mathf.Sin( angleRad ) );
    }

    public static float GetAngleFromVectorFloat( Vector3 dir )
    {
        //Normalises the vector so it can be used to calculate more accurate values
        dir = dir.normalized;

        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if( n < 0 )
        {
            n += 360;
        }

        return n;

    }

    public GameObject DrawFOV( LayerMask targetLayer, bool checkingForTargets )
    {
        float angle = m_startingAngle;

        float angleIncrease = m_fovWidth / m_rayCount;

        Vector3[] verts = new Vector3[m_rayCount + 1 + 1];
        Vector2[] uv    = new Vector2[verts.Length];
        int[]     tris  = new int[m_rayCount * 3];

        verts[0] = m_fovOrigin;

        int vertexIndex = 1;

        int triangleIndex = 0;

        GameObject newTarget = null;

        for ( int i = 0; i <= m_rayCount; i++ )
        {

            Vector3 vertex;

            RaycastHit2D raycastHit2D = Physics2D.Raycast(m_fovOrigin, GetVectorFromAngle(angle), m_viewDistance, m_wallLayerMask);

            if ( raycastHit2D.collider == null )
            {
                vertex = m_fovOrigin + GetVectorFromAngle( angle ) * m_viewDistance;
            }
            else
            {
                vertex = raycastHit2D.point;
            }

            if ( checkingForTargets )
            {

                RaycastHit2D raycastHit = Physics2D.Raycast(m_fovOrigin, GetVectorFromAngle(angle), m_viewDistance, targetLayer);

                if ( raycastHit.collider != null )
                {
                    newTarget = raycastHit.collider.gameObject;
                }

            }

            verts[vertexIndex] = vertex;

            if ( i > 0 )
            {
                tris[triangleIndex + 0] = 0;
                tris[triangleIndex + 1] = vertexIndex - 1;
                tris[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;

            angle -= angleIncrease;

        }

        m_fieldOfViewMesh.vertices = verts;
        m_fieldOfViewMesh.uv = uv;
        m_fieldOfViewMesh.triangles = tris;
        m_fieldOfViewMesh.bounds = new Bounds( m_fovOrigin , Vector3.one * 1000f );

        return newTarget;

    }

    public void SetOrigin( Vector3 origin )
    {
        m_fovOrigin = origin; 
    }

    public void SetAimDirection( Vector3 aimDirection )
    {
        m_startingAngle = GetAngleFromVectorFloat( aimDirection ) + ( m_fovWidth * 0.5f );
    }

}
