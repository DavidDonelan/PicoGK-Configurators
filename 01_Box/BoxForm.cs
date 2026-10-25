using System.Windows.Forms;
using System.Numerics;
using PicoGK;

public class BoxForm : Form
{
    private TrackBar tbrX, tbrY, tbrZ;
    private Label lblX, lblY, lblZ;
    private Viewer oViewer;

    public BoxForm(float x, float y, float z, Viewer oViewer)
    {
        this.oViewer = oViewer;

        Text = "Box Parameters";
        Width = 300;
        Height = 250;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        // Header
        Label lblTitle = new() { Text = "Box Parameters", Left = 10, Top = 10, Width = 260 };

        // X
        lblX = new Label() { Text = $"X: {x}", Left = 10, Top = 40, Width = 260 };
        tbrX = CreateTrackBar(x, 50);

        // Y
        lblY = new Label() { Text = $"Y: {y}", Left = 10, Top = 100, Width = 260 };
        tbrY = CreateTrackBar(y, 110);

        // Z
        lblZ = new Label() { Text = $"Z: {z}", Left = 10, Top = 160, Width = 260 };
        tbrZ = CreateTrackBar(z, 170);

        Controls.AddRange(new Control[] { lblTitle, lblX, tbrX, lblY, tbrY, lblZ, tbrZ });
    }

    private TrackBar CreateTrackBar(float initialValue, int top)
    {
        TrackBar tbr = new()
        {
            Minimum = 1,
            Maximum = 50,
            Value = (int)initialValue,
            Left = 10,
            Top = top,
            Width = 260,
            TickFrequency = 5
        };
        tbr.MouseUp += OnSliderReleased;
        return tbr;
    }

    private void OnSliderReleased(object sender, MouseEventArgs e)
    {
        float x = tbrX.Value;
        float y = tbrY.Value;
        float z = tbrZ.Value;

        lblX.Text = $"X: {x}";
        lblY.Text = $"Y: {y}";
        lblZ.Text = $"Z: {z}";

        oViewer.RemoveAllObjects();
        ConfigurableBox box = new(new Vector3(x, y, z));
        oViewer.Add(box.mshConstruct());
    }
}