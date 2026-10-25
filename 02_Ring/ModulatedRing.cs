using System.Numerics;
using System.Windows.Forms;
using PicoGK;
using Leap71.ShapeKernel;


public class ModulatedRing
{
    public static void Task()
    {
        RingParams oInitialParams = new();
        ModulatedRing oInitialRing = new(oInitialParams);
        Sh.PreviewVoxels(oInitialRing.voxConstruct(), Cp.clrLavender);
    }

    public ModulatedRing(RingParams oInput) //not sure if vector as arguments or some parameter list object
    {
        m_oRingParams = oInput;
        m_oWorldCS = oInput.oWorldCS;

    }

    public Voxels voxConstruct()
    {
        Voxels voxModRing = new();
        LocalFrame oLocalFrame  = m_oWorldCS;
        BaseRing oShape         = new BaseRing(oLocalFrame, m_oRingParams.fRingRadiusMM);
        oShape.SetRadius(new SurfaceModulation(fGetRingRadius));
        voxModRing = oShape.voxConstruct();
        return voxModRing;
    }

    float fGetRingRadius(float fPhi, float fAlpha)
    {
        fPhi += m_oRingParams.fGetTwist()*fAlpha;
        float fRadius = m_fRadius2 - m_fAmplitude1 * MathF.Cos(m_fRingLobes*fPhi)
         + m_fAmplitude2 * MathF.Cos(m_fProfileLobes*fAlpha);


        return fRadius;
    }

    public RingParams m_oRingParams;
    public LocalFrame m_oWorldCS;



    //goal here is too create the next level of a configurator
    //Pass the parameters as a set
    //Also the boolean variable test for the Delegate function for Surface modulation
    //Methods: Task,constructor, rather a vox construct, surfmod function
    //atributes: switch case for different delegate functions within the surfacemod
    //Parameters: Ringlobes, Profilelobes, radialtwist, r1, r2 
}