 using System.Numerics;
 using Leap71.ShapeKernel;
 public class RingParams
{
    public float fRingRadiusMM = 30f; // Big Ring Radius in millimetres
    public float fSectionRadiusMM = 8f; // The Section Radius, maybe generalize for other shapes later in millimetres
    public int nSectionLobes = 5; // number of lobes for the modulated geometry of the cross- section
    public int nRingLobes = 5; // number of lobes for the cross-section changes in Modulation
    public int nTwistSteps = 1; //Lobe steps per revolution
    public float fSectionAmpMM = 2f; //Amplitude for the section lobes in millimetres
    public float fRingAmpMM = 3f; // Amplitude for the Ring Lobes in millimetres
    public LocalFrame oWorldCS = new LocalFrame(new Vector3(0, 0, 0));

    public RingParams()
    {
        
    }

    public float fGetTwist()
    {
        return (float)nTwistSteps/nSectionLobes;
        //nSectionLobes =0 causes the expresion to return infinity, to be fixed
        //would be nice to have parameters return a plain ring-->amplitudes =0
    }

    public List<String> astrGetErrors()
    {
        List<String> astr = [];
        if (nSectionLobes < 1)
        {
            astr.Add("nSectionLobes error: Section Lobe count invalid"); //should carry the actual value eg nSectionLobes should be 1 or more
        }
        if (nRingLobes < 1)
        {
            astr.Add("nRingLobes error: Ring Lobe count invalid");
        }
        if(fRingRadiusMM <= 0)
        {
            astr.Add("fRingRadiusMM error: Ring radius invalid");
        }
        if(fSectionRadiusMM <= 0)
        {
            astr.Add("fSectionRadiusMM error: Section radius invalid");
        }
        if(fSectionAmpMM < 0)
        {
            astr.Add("fSectionAmpMM error: Section Amplitude invalid");
        }

        if (fRingAmpMM < 0)
        {
            astr.Add("fRingAmpMM error: Ring Amplitude invalid");
        }

        return astr;
    }
/*Ring against tube: fRingRadiusMM must exceed the thickest tube, fSectionRadiusMM + fSectionAmpMM + fRingAmpMM. 
Thinnest tube: fSectionRadiusMM − fSectionAmpMM − fRingAmpMM should be at least 1.
Message wording: name the parameter, the rule and the actual value, for example “nSectionLobes must be at least 1, but is 0”.
Test each rule by breaking it on purpose in the defaults, then restore the value. Items 1 to 3 are the ones I expect to matter.*/
}