using System.Windows.Forms; // Form, TrackBar (slider), Label, MouseEventArgs
using PicoGK;               // Viewer (the 3D window)
using Leap71.ShapeKernel;


public class ModulatedRingForm : Form
{

    private TrackBar tbrSectionLobes;
    private Label lblSectionLobes;
    private Viewer oViewer;

    private RingParams m_oRingParams;


    public ModulatedRingForm(RingParams oParams, Viewer oViewer)
    {

        this.oViewer = oViewer;
        this.m_oRingParams = oParams;


        Text = "Ring Configurator"; // title bar text
        Width = 300;

        Height = 250;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;


        Label lblTitle = new() { Text = "Modulated Ring Parameters", Left = 10, Top = 10, Width = 260 };

        lblSectionLobes = new Label() { Text = $"Section Lobes: {m_oRingParams.nSectionLobes}", Left = 10, Top = 40, Width = 260 };
        tbrSectionLobes = CreateTrackBar(m_oRingParams.nSectionLobes, 50);


        Controls.AddRange(new Control[] { lblTitle, lblSectionLobes, tbrSectionLobes });
    }


    private TrackBar CreateTrackBar(float initialValue, int top)
    {
        TrackBar tbr = new()
        {

            Minimum = 1,
            Maximum = 10,

            Value = (int)initialValue,
            Left = 10,
            Top = top,
            Width = 260,
            TickFrequency = 1
        };


        tbr.MouseUp += OnSliderReleased;
        return tbr;
    }


    private void OnSliderReleased(object? sender, MouseEventArgs e)
    {

        m_oRingParams.nSectionLobes = tbrSectionLobes.Value;

        lblSectionLobes.Text = $"Section Lobes: {m_oRingParams.nSectionLobes}";

        oViewer.RemoveAllObjects();

        ModulatedRing oCurrentRing = new(m_oRingParams);
        
        Sh.PreviewVoxels(oCurrentRing.voxGetBody(), Cp.clrLavender);
    }
}