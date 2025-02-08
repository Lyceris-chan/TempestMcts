using System.ComponentModel;

namespace TempestAnalysis;

public class MainWindow : Form
{
    private Container? _components;

    protected override void Dispose(bool disposing)
    {
        if (disposing && _components != null)
        {
            _components.Dispose();
        }

        base.Dispose(disposing);
    }
    
    private void InitializeComponent()
    {
        _components = new Container();
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1200, 800);
        Text = "Tempest Analysis";
    }
    
    public MainWindow()
    {
        InitializeComponent();
    }
}