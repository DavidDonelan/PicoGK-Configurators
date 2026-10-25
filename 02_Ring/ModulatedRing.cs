using System.Windows.Forms;//later
using PicoGK;
using Leap71.ShapeKernel;


public class ModulatedRing
{
    public static void Task()
    {
        RingParams oInitialParams = new();
        ModulatedRing oInitialRing = new(oInitialParams);
        float fWorldCSSize = oInitialRing.m_oRingParams.fSectionRadiusMM;
        Sh.PreviewFrame(new(),fWorldCSSize); //preview default origin to confirm transformations
        Sh.PreviewFrame(oInitialRing.m_oWorldCS,fWorldCSSize); //preview the local CS of the ring
        Sh.PreviewVoxels(oInitialRing.m_voxRingBody, Cp.clrLavender); //The Initial ring is shown in colour Purple
        //App Run here for later
    }

    public ModulatedRing(RingParams oParams)
    {
        m_oRingParams = oParams;
        m_oWorldCS = oParams.oWorldCS;
        m_voxRingBody = voxBuildRing();

    }

    private Voxels voxBuildRing()
    {
        BaseRing oShape = new BaseRing(m_oWorldCS, m_oRingParams.fRingRadiusMM);
        oShape.SetRadius(new SurfaceModulation(fGetRingRadius));
        return oShape.voxConstruct();
    }

    float fGetRingRadius(float fPhi, float fAlpha)
    {
        fPhi += m_oRingParams.fGetTwist()*fAlpha;
        float fRadius = m_oRingParams.fSectionRadiusMM - m_oRingParams.fSectionAmpMM * MathF.Cos(m_oRingParams.nSectionLobes*fPhi)
         + m_oRingParams.fRingAmpMM * MathF.Cos(m_oRingParams.nRingLobes*fAlpha);

        return fRadius;
    }

    public LocalFrame oGetWorldCS()
    {
        return m_oWorldCS;
    }

    private RingParams m_oRingParams;
    private LocalFrame m_oWorldCS;
    private Voxels m_voxRingBody;

}