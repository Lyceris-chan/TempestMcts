using System.ComponentModel;
using System.Text.Json;
using MarshalLib;

namespace MctsDetective;

public class MainWindow : Form
{
    private readonly IContainer? _components = null;
    
    private SplitContainer _splitContainer = new()
    {
        Dock = DockStyle.Fill
    };
    private ListView _listView = new()
    {
        Dock = DockStyle.Fill,
        FullRowSelect = true
    };
    private TabControl _contentContainer = new()
    {
        Dock = DockStyle.Fill
    };
    private TabPage _treeViewPage = new()
    {
        Text = "TreeView"
    };
    private TabPage _jsonPage = new()
    {
        Text = "JSON"
    };
    private TabPage _rawPage = new()
    {
        Text = "Raw"
    };
    
    private byte[] _buffer = [];
    private MctsPacket _selectedPacket = new();
    
    public MainWindow()
    {
        Controls.Add(_splitContainer);
        
        _splitContainer.Panel1.Controls.Add(_listView);
        _splitContainer.Panel2.Controls.Add(_contentContainer);
        
        _contentContainer.TabPages.Add(_treeViewPage);
        _contentContainer.TabPages.Add(_jsonPage);
        _contentContainer.TabPages.Add(_rawPage);
        
        _treeViewPage.Enter += TreeViewPage_Load;
        _jsonPage.Enter += JsonPage_Load;
        _rawPage.Enter += RawPage_Load;
        
        InitializeComponent();
    }
    
    private void RawPage_Load(object? sender, EventArgs e)
    {
        var rawTextBox = new TextBox()
        {
            Dock = DockStyle.Fill,
            Multiline = true,
            ReadOnly = true,
            Text = Convert.ToHexString(_buffer)
        };
        
        _rawPage.Controls.Clear();
        _rawPage.Controls.Add(rawTextBox);
    }
    
    private void JsonPage_Load(object? sender, EventArgs e)
    {
        var jsonTextBox = new TextBox()
        {
            Dock = DockStyle.Fill,
            Multiline = true,
            ReadOnly = true,
            Text = JsonSerializer.Serialize(_selectedPacket, new JsonSerializerOptions()
            {
                WriteIndented = true
            })
        };
        
        _jsonPage.Controls.Clear();
        _jsonPage.Controls.Add(jsonTextBox);
    }
    
    private void TreeViewPage_Load(object? sender, EventArgs e)
    {
        var treeView = new TreeView()
        {
            Dock = DockStyle.Fill
        };
        
        treeView.LoadJsonToTreeView(JsonSerializer.Serialize(_selectedPacket));
        
        _treeViewPage.Controls.Clear();
        _treeViewPage.Controls.Add(treeView);
    }
    
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
        SuspendLayout();
        
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1200, 800);
        Text = "MctsDetective";
        ResumeLayout(false);
    }
}