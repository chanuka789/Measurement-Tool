using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace QS_Takeoff.UI.Views
{
    public partial class Viewer3D : UserControl
    {
        public IEnumerable<Uri>? ModelParts { get; set; }

        public Viewer3D()
        {
            InitializeComponent();
        }
    }
}

