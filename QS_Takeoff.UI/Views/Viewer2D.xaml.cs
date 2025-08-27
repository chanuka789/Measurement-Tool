using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace QS_Takeoff.UI.Views
{
    public partial class Viewer2D : UserControl
    {
        public IEnumerable<Uri>? DrawingParts { get; set; }

        public Viewer2D()
        {
            InitializeComponent();
        }
    }
}

