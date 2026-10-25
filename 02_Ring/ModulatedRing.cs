using System.Windows.Forms;//later
using System.Numerics; //just in case
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
        //constructor should read like a process plan
        m_oRingParams = oParams;
        List<String> astrErrors = oParams.astrGetErrors();
        m_oWorldCS = new LocalFrame (new(0, 0, 0));

        if (astrErrors.Count > 0)
        {
            foreach (String str in astrErrors)
            {
                Library.Log(str);
            }
            Leap71.ShapeKernel.BaseBox oDummy = new(m_oWorldCS);
            m_voxRingBody = oDummy.voxConstruct();
        }
        else
        {
            m_oWorldCS = oParams.oWorldCS;
            m_voxRingBody = voxBuildRing();
        }

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

    public Voxels voxGetBody()
    {
        return m_voxRingBody;
    }

    private RingParams m_oRingParams;
    private LocalFrame m_oWorldCS;
    private Voxels m_voxRingBody;

}