#region Custom Border Forms - Copyright (C) 2005 Szymon Kobalczyk

// Custom Border Forms
// Copyright (C) 2005 Szymon Kobalczyk
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.

// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
//
// Szymon Kobalczyk (http://www.geekswithblogs.com/kobush)

#endregion

#region Using directives

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace KR_POP.Common.ControlEX
{
    public class LonghornForm : CustomBorderForm
    {
        public LonghornForm()
        {
            this.FormStyle = CreateFormStyle();
			this.Activated += new EventHandler(LonghornForm_Activated);
			this.SizeChanged += new EventHandler(LonghornForm_SizeChanged);
		}

		void LonghornForm_SizeChanged(object sender, EventArgs e)
		{
			this.InvalidateWindow();
			this.Refresh();
			this.UpdateStyles();
		}

		void LonghornForm_Activated(object sender, EventArgs e)
		{
			this.InvalidateWindow();
			this.Refresh();
			//this.UpdateStyles();
		}

		
		

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            int diam = 0;
			GraphicsPath path = new GraphicsPath();
			//path.AddArc(0, 0, diam, diam, -90, -90);
			path.AddLines(new Point[] {new Point(0,diam), new Point(0, Height), 
			    new Point(Width, Height), new Point(Width, diam)});
			//path.AddArc(Width - diam, 0, diam, diam, 0, -90);
            path.CloseFigure();

            this.Region = new Region(path);
		}

        FormStyle CreateFormStyle()
        {
            FormStyle style = new FormStyle();
#if false
			style.NormalState.Image = Properties.Resources.Border_White;
            style.NormalState.SizeMode = ImageSizeMode.Stretched;
            style.NormalState.StretchMargins = new Padding(3, 23, 3, 3);

            style.CloseButton.Size = Properties.Resources.Close_White_Normal.Size;
            style.CloseButton.Margin = new Padding(1,3,5,0);
			style.CloseButton.NormalState.Image = Properties.Resources.Close_White_Normal;
			style.CloseButton.DisabledState.Image = Properties.Resources.Close_White_Pressed;
			style.CloseButton.ActiveState.Image = Properties.Resources.Close_White_Pressed;
			style.CloseButton.HoverState.Image = Properties.Resources.Close_White_Normal;

			style.MaximizeButton.Size = Properties.Resources.Maximize_White_Normal.Size;
            style.MaximizeButton.Margin = new Padding(1, 3, 1, 0);
			style.MaximizeButton.NormalState.Image = Properties.Resources.Maximize_White_Normal;
			style.MaximizeButton.DisabledState.Image = Properties.Resources.Maximize_White_Pressed;
			style.MaximizeButton.ActiveState.Image = Properties.Resources.Maximize_White_Pressed;
			style.MaximizeButton.HoverState.Image = Properties.Resources.Maximize_White_Normal;

			style.MinimizeButton.Size = Properties.Resources.Minimize_White_Normal.Size;
            style.MinimizeButton.Margin = new Padding(1, 3, 1, 0);
			style.MinimizeButton.NormalState.Image = Properties.Resources.Minimize_White_Normal;
			style.MinimizeButton.DisabledState.Image = Properties.Resources.Minimize_White_Pressed;
			style.MinimizeButton.ActiveState.Image = Properties.Resources.Minimize_White_Pressed;
			style.MinimizeButton.HoverState.Image = Properties.Resources.Minimize_White_Normal;

			style.RestoreButton.Size = Properties.Resources.Maximize_White_Normal.Size;
            style.RestoreButton.Margin = new Padding(1, 3, 1, 0);
			style.RestoreButton.NormalState.Image = Properties.Resources.Maximize_White_Normal;
			style.RestoreButton.DisabledState.Image = Properties.Resources.Maximize_White_Pressed;
			style.RestoreButton.ActiveState.Image = Properties.Resources.Maximize_White_Pressed;
			style.RestoreButton.HoverState.Image = Properties.Resources.Maximize_White_Normal;
#else
            style.NormalState.Image = Properties.Resources.Border_Blue;
            style.NormalState.SizeMode = ImageSizeMode.Stretched;
            style.NormalState.StretchMargins = new Padding(3, 23, 3, 3);

            style.CloseButton.Size = Properties.Resources.Close_Blue_Normal.Size;
            style.CloseButton.Margin = new Padding(1,3,5,0);
			style.CloseButton.NormalState.Image = Properties.Resources.Close_Blue_Normal;
			style.CloseButton.DisabledState.Image = Properties.Resources.Close_Blue_Pressed;
			style.CloseButton.ActiveState.Image = Properties.Resources.Close_Blue_Pressed;
			style.CloseButton.HoverState.Image = Properties.Resources.Close_Blue_Normal;

            style.MaximizeButton.Size = Properties.Resources.Maximize_Blue_Normal.Size;
            style.MaximizeButton.Margin = new Padding(1, 3, 1, 0);
			style.MaximizeButton.NormalState.Image = Properties.Resources.Maximize_Blue_Normal;
			style.MaximizeButton.DisabledState.Image = Properties.Resources.Maximize_Blue_Pressed;
			style.MaximizeButton.ActiveState.Image = Properties.Resources.Maximize_Blue_Pressed;
			style.MaximizeButton.HoverState.Image = Properties.Resources.Maximize_Blue_Normal;

			style.MinimizeButton.Size = Properties.Resources.Minimize_Blue_Normal.Size;
            style.MinimizeButton.Margin = new Padding(1, 3, 1, 0);
			style.MinimizeButton.NormalState.Image = Properties.Resources.Minimize_Blue_Normal;
			style.MinimizeButton.DisabledState.Image = Properties.Resources.Minimize_Blue_Pressed;
            style.MinimizeButton.ActiveState.Image = Properties.Resources.Minimize_Blue_Pressed;
			style.MinimizeButton.HoverState.Image = Properties.Resources.Minimize_Blue_Normal;

			style.RestoreButton.Size = Properties.Resources.Maximize_Blue_Normal.Size;
            style.RestoreButton.Margin = new Padding(1, 3, 1, 0);
			style.RestoreButton.NormalState.Image = Properties.Resources.Maximize_Blue_Normal;
			style.RestoreButton.DisabledState.Image = Properties.Resources.Maximize_Blue_Pressed;
			style.RestoreButton.ActiveState.Image = Properties.Resources.Maximize_Blue_Pressed;
			style.RestoreButton.HoverState.Image = Properties.Resources.Maximize_Blue_Normal;
#endif
			style.TitleColor = Color.White;
            style.TitleShadowColor = Color.DimGray;
            style.TitleFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);

            style.ClientAreaPadding = new Padding(3, 23, 3, 3);
            style.IconPadding = new Padding(3, 3, 0, 0);

			this.NonClientAreaDoubleBuffering = true;

            return style;
        }
    }
}
