using System.Numerics;
using System.Windows.Forms;
using PicoGK;

public class ConfigurableBox
{
    public static void Task()
    {
        ConfigurableBox initialBox = new(new Vector3(10f, 10f, 15f));
        Library.oViewer().Add(initialBox.mshConstruct());

        Application.Run(new BoxForm(10f, 10f, 15f, Library.oViewer()));
    }

    public ConfigurableBox(Vector3 vecSize)
    {
        matTransform = Matrix4x4.CreateScale(vecSize / 2);

    }

    public Mesh mshConstruct()
    {
        Vector3[] avec =
        {
            new Vector3(-1.0f, -1.0f, -1.0f),
            new Vector3( 1.0f, -1.0f, -1.0f),
            new Vector3( 1.0f,  1.0f, -1.0f),
            new Vector3(-1.0f,  1.0f, -1.0f),
            new Vector3(-1.0f, -1.0f,  1.0f),
            new Vector3( 1.0f, -1.0f,  1.0f),
            new Vector3( 1.0f,  1.0f,  1.0f),
            new Vector3(-1.0f,  1.0f,  1.0f)
        };

        Mesh msh = new();
        int[] anV = new int[avec.Length];

        int n = 0;
        foreach (Vector3 vec in avec)
        {
            anV[n] = msh.nAddVertex(Vector3.Transform(vec, matTransform));
            n++;
        }

        msh.AddQuad(anV[3], anV[2], anV[1], anV[0]); // Bottom
        msh.AddQuad(anV[4], anV[5], anV[6], anV[7]); // Top
        msh.AddQuad(anV[1], anV[5], anV[4], anV[0]); // Front
        msh.AddQuad(anV[7], anV[6], anV[2], anV[3]); // Back
        msh.AddQuad(anV[4], anV[7], anV[3], anV[0]); // Left
        msh.AddQuad(anV[2], anV[6], anV[5], anV[1]); // Right

        return msh;
    }

    public Matrix4x4 matTransform { get; set; }
}