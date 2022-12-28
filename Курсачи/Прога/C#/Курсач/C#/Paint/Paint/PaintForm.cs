
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace Paint
{
    public partial class PaintForm : Form
    {
        private bool drawing;
        private bool moving;
        private Point prevMousePos;

        public PaintForm()
        {
            InitializeComponent();
            toolsBar.ImageList = imageList;
        }

        private void toolsBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            SetToolBarButtonsState(e.Button);
        }

        private void SetToolBarButtonsState(ToolBarButton curButton)
        {
            curButton.Pushed = true;
            foreach (ToolBarButton btn in toolsBar.Buttons)
            {
                if (btn != curButton)
                    btn.Pushed = false;
            }
        }

        private void PaintForm_Load(object sender, EventArgs e)
        {
            // fill Width list
            for (int i = 1; i < 11; i++)
                widthCombo.Items.Add(i);
            for (int i = 15; i <= 60; i += 5)
                widthCombo.Items.Add(i);
            widthCombo.SelectedIndex = 0;
        }

        private void ColorBox_Click(object sender, EventArgs e)
        {
            PictureBox picBox = (PictureBox)sender;
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.FullOpen = true;

            colorDlg.Color = picBox.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                picBox.BackColor = colorDlg.Color;
            }
        }

        private void imageClearMnu_Click(object sender, EventArgs e)
        {
            VersionControl.elem.Clear();
            imageBox.Refresh();
        }

        private void fileExitMnu_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            VersionControl.commit();
            imageBox.Refresh();
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            VersionControl.next_commit();
            imageBox.Refresh();
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            VersionControl.prev_commit();
            imageBox.Refresh();
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            VersionControl.next_branch();
            imageBox.Refresh();
        }

        private void imageBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (arrowBtn.Pushed)
                {
                    foreach (Element el in VersionControl.elem)
                    {
                        if (el.coords.point1.X < e.X && e.X < el.coords.point2.X && el.coords.point1.Y < e.Y && e.Y < el.coords.point2.Y)
                        {
                            VersionControl.elem.Remove(el);
                            VersionControl.elem.Add(el);
                            moving = true;
                            break;
                        }
                    }
                }
                else
                {
                    drawing = true;
                    Shape shape = Shape.line;
                    if (lineBtn.Pushed)
                    {
                        shape = Shape.line;
                    }
                    else if (rectangleBtn.Pushed)
                    {
                        shape = Shape.rectangle;
                    }
                    else if (ellipseBtn.Pushed)
                    {
                        shape = Shape.ellipse;
                    }
                    Element new_el = new Element(new Point(e.X, e.Y), new Point(e.X, e.Y), shape, Int32.Parse(widthCombo.Text), primColorBox.BackColor.ToArgb(), new Color().ToArgb());
                    VersionControl.elem.Add(new_el);
                    imageBox.Refresh();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < VersionControl.elem.Count; i++)
                {
                    Element el = VersionControl.elem[i];
                    if (el.coords.point1.X < e.X && e.X < el.coords.point2.X && el.coords.point1.Y < e.Y && e.Y < el.coords.point2.Y)
                    {
                        VersionControl.elem[i] = VersionControl.elem[VersionControl.elem.Count - 1];
                        VersionControl.elem.Remove(VersionControl.elem[VersionControl.elem.Count - 1]);
                        break;
                    }
                }
            }
        }

        private void imageBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                VersionControl.elem[VersionControl.elem.Count - 1].coords.point2 = e.Location;
                imageBox.Refresh();
            }
            else if (moving)
            {
                VersionControl.elem[VersionControl.elem.Count - 1].coords.point1.X += e.Location.X - prevMousePos.X;
                VersionControl.elem[VersionControl.elem.Count - 1].coords.point1.Y += e.Location.Y - prevMousePos.Y;
                VersionControl.elem[VersionControl.elem.Count - 1].coords.point2.X += e.Location.X - prevMousePos.X;
                VersionControl.elem[VersionControl.elem.Count - 1].coords.point2.Y += e.Location.Y - prevMousePos.Y;
                imageBox.Refresh();
            }
            prevMousePos= e.Location;
        }

        private void imageBox_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;
            moving = false;
            imageBox.Refresh();
        }

        private void imageBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            PictureBox sendr = ((PictureBox)sender);
            SolidBrush resetBrush = new SolidBrush(Color.White);
            Rectangle wholeArea = new Rectangle(0, 0, sendr.Width, sendr.Height);
            g.FillRectangle(resetBrush, wholeArea);


            for (int i = 0; i < VersionControl.elem.Count; i++)
            {
                Element el = VersionControl.elem[i];

                if (el.shape == Shape.line)
                {
                    Pen pen = new Pen(Color.FromArgb(el.color), el.size);
                    g.DrawLine(pen, el.coords.point1, el.coords.point2);
                }
                else if (el.shape == Shape.rectangle)
                {
                    Rectangle rect = GetRectangleFromPoints(el.coords.point1, el.coords.point2);
                    Pen pen = new Pen(Color.FromArgb(el.color), el.size);
                    e.Graphics.DrawRectangle(pen, rect);
                }
                else if (el.shape == Shape.ellipse)
                {
                    Rectangle rect = GetRectangleFromPoints(el.coords.point1, el.coords.point2);
                    Pen pen = new Pen(Color.FromArgb(el.color), el.size);
                    e.Graphics.DrawEllipse(pen, rect);
                }
            }
        }

        protected Rectangle GetRectangleFromPoints(Point p1, Point p2)
        {
            Point oPoint;
            Rectangle rect;

            if ((p2.X > p1.X) && (p2.Y > p1.Y))
            {
                rect = new Rectangle(p1, new Size(p2.X - p1.X, p2.Y - p1.Y));
            }
            else if ((p2.X < p1.X) && (p2.Y < p1.Y))
            {
                rect = new Rectangle(p2, new Size(p1.X - p2.X, p1.Y - p2.Y));
            }
            else if ((p2.X > p1.X) && (p2.Y < p1.Y))
            {
                oPoint = new Point(p1.X, p2.Y);
                rect = new Rectangle(oPoint, new Size(p2.X - p1.X, p1.Y - oPoint.Y));
            }
            else
            {
                oPoint = new Point(p2.X, p1.Y);
                rect = new Rectangle(oPoint, new Size(p1.X - p2.X, p2.Y - p1.Y));
            }
            return rect;
        }
    }
}