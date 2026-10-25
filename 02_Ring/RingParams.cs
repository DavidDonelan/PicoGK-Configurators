 using System.Numerics;
 using Leap71.ShapeKernel;
 public class RingParams
{
    public float fRingRadiusMM = 30f; // Big Ring Radius in milimetres
    public float fSectionRadiusMM = 8f; // The Section Radius, maybe generalize for other shapes later in milimetres
    public int nSectionLobes = 5; // number of lobes for the modulated geometry of the cross- section
    public int nRingLobes = 5; // number of lobes for the cross-section changes in Modulation
    public int nTwistSteps = 5; //twisting along the ring
    public float fSectionAmpMM = 2f; //Amplitude for the section lobes in milimetres
    public float fRingAmpMM = 3f; // Amplitude for the Ring Lobes in milimetres
    public LocalFrame oWorldCS = new LocalFrame(new Vector3(0, 0 ,0));

    public float fGetTwist()
    {
        return (float)nTwistSteps/nSectionLobes;
        //nSectionLobes =0 causes the expresion to return infinity, to be fixed
        //would be nice to have parameters return a plain ring
    }

}