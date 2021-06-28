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

        //Sets the origin of the FOV
        m_fovOrigin = transform.parent.position;

    }

    public static Vector3 GetVectorFromAngle( float angle )
    {
        //Calculates a Vector3 based on the angle passed in to the method
        float angleRad = angle * (Mathf.PI/180f);
        return new Vector3( Mathf.Cos( angleRad ) , Mathf.Sin( angleRad ) );
    }

    public static float GetAngleFromVectorFloat( Vector3 direction )
    {
        //Normalises the vector so it can be used to calculate more accurate values
        direction = direction.normalized;

        //Calculates an angle based on the Vector3 passed in
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //If the angle is less than 0, it has 360 added to it to create its positive equivalent
        if( angle < 0 )
        {
            angle += 360;
        }

        //Returns the newly calculated angle
        return angle;

    }

    public GameObject DrawFOV( LayerMask targetLayer, bool checkingForTargets )
    {

        //Creates a new float based on the starting angle of the FOV
        float angle = m_startingAngle;

        //Calculates how much the angle has to increase between each ray by dividing the width by the number of rays
        float angleIncrease = m_fovWidth / m_rayCount;

        //Creates an array of Vector3s for use in drawing the FOV mesh
        Vector3[] vertices = new Vector3[m_rayCount + 2];

        //Creates an array of Vector2s for use in drawing the FOV mesh
        Vector2[] uv    = new Vector2[vertices.Length];

        //Creates an array of ints for use in drawing the FOV mesh
        int[]     tris  = new int[m_rayCount * 3];

        //Sets the first vector in the array to be the origin of the FOV so that the mesh is drawn from there
        vertices[0] = m_fovOrigin;

        int vertexIndex = 1;

        int triangleIndex = 0;

        //Creates a new local GameObject variable that the method will return
        GameObject newTarget = null;

        //Loops for the number of rays that will be used in drawing the FOV
        for ( int i = 0; i <= m_rayCount; i++ )
        {

            Vector3 vertex;

            //Sends out a raycast on the wall layer to determine if there is anything that will block the mesh
            RaycastHit2D raycastHit2D = Physics2D.Raycast(m_fovOrigin, GetVectorFromAngle(angle), m_viewDistance, m_wallLayerMask);

            //Checks to see if the raycast hit any walls
            if ( raycastHit2D.collider == null )
            {
                //If the raycast doesn't hit anything, the vertex in the mesh is set at the view distance from the origin using the current angle
                vertex = m_fovOrigin + GetVectorFromAngle( angle ) * m_viewDistance;

                //If the FOV should be checking for targets, another ray is sent out on the player layer
                if ( checkingForTargets )
                {

                    RaycastHit2D raycastHit = Physics2D.Raycast(m_fovOrigin, GetVectorFromAngle(angle), m_viewDistance, targetLayer);

                    //If the raycast hits a player, then newTarget is assigned as that object
                    if ( raycastHit.collider != null )
                    {
                        newTarget = raycastHit.collider.gameObject;
                    }
                }
            }
            //Otherwise, if the raycast hit a wall, the vertex position is set as the point where the ray hit a collider
            else
            {
                vertex = raycastHit2D.point;
            }

            //The vertex in the array of vertices at the current vertex index is set as the newly calculated vertex
            vertices[vertexIndex] = vertex;

            //Sets up the positions of the next 3 tris in the mesh
            if ( i > 0 )
            {
                tris[triangleIndex ]    = 0;
                tris[triangleIndex + 1] = vertexIndex - 1;
                tris[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            //Increments the value of vertexIndex to alter the correct vertex in the next loops
            vertexIndex++;

            //Alters the value of the angle by the angleIncrease calculated previously to ensure the next ray is fired in the correct angle
            angle -= angleIncrease;

        }

        //Updates the mush to use all of the newly calculated values
        m_fieldOfViewMesh.vertices = vertices;
        m_fieldOfViewMesh.uv = uv;
        m_fieldOfViewMesh.triangles = tris;

        //Alters the bounds so that the mesh doesn't disappear when the player is a certain distance from it
        m_fieldOfViewMesh.bounds = new Bounds( m_fovOrigin , Vector3.one * 1000f );

        //Returns the newly aquired target, which will either be null or the player
        return newTarget;

    }

    public void SetOrigin( Vector3 origin )
    {
        //Sets the origin of the FOV to be the Vector3 passed in
        m_fovOrigin = origin; 
    }

    public void SetAimDirection( Vector3 aimDirection )
    {
        //Sets the aiming direction of the FOV, adding half of the FOV's width to center it
        m_startingAngle = GetAngleFromVectorFloat( aimDirection ) + ( m_fovWidth * 0.5f );
    }

}
