 using System.Numerics;
 using Leap71.ShapeKernel;
 public class RingParams
{
    public float fRingRadiusMM = 30f; // Big Ring Radius in millimetres
    public float fSectionRadiusMM = 8f; // The Section Radius, maybe generalize for other shapes later in millimetres
    public int nSectionLobes = 5; // number of lobes for the modulated geometry of the cross- section
    public int nRingLobes = 5; // number of lobes for the cross-section changes in Modulation
    public int nTwistSteps = -2; //Lobe steps per revolution
    public float fSectionAmpMM = 10f; //Amplitude for the section lobes in millimetres
    public float fRingAmpMM = 5f; // Amplitude for the Ring Lobes in millimetres
    public LocalFrame oWorldCS = new LocalFrame(new Vector3(0, 100, 50));

    public float fGetTwist()
    {
        return (float)nTwistSteps/nSectionLobes;
        //nSectionLobes =0 causes the expresion to return infinity, to be fixed
        //would be nice to have parameters return a plain ring-->amplitudes =0
    }

}