#region Includes
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;
using System.Threading;
#endregion

#region CircularProgressBar

namespace RoundProgressBar
{

    public class CircularProgressBar : Control
    {

        #region Enums

        public enum _ProgressShape
        {
            Round,
            Flat
        }

        #endregion
        #region Variables
        public bool isNeedTextValue { get; set; }
        private long _Value;
        private long _Maximum = 100;
        private Color _ProgressColor1 = Color.FromArgb(92, 92, 92);
        private Color _ProgressColor2 = Color.FromArgb(92, 92, 92);
        private _ProgressShape ProgressShapeVal;

        #endregion

        #region Custom Properties

        public long Value
        {
            get { return _Value; }
            set
            {
                if (value > _Maximum)
                    value = _Maximum;
                _Value = value;
                Invalidate();
            }
        }

        public long Maximum
        {
            get { return _Maximum; }
            set
            {
                if (value < 1)
                    value = 1;
                _Maximum = value;
                Invalidate();
            }
        }

        public Brush TextColor
        {
            get {
                var br = new SolidBrush(ProgressColor1);
                return br;
            }
        }

        public Color ProgressColor1
        {
            get { return _ProgressColor1; }
            set
            {
                _ProgressColor1 = value;
                Invalidate();
            }
        }

        public Color ProgressColor2
        {
            get { return _ProgressColor2; }

            set
            {
                _ProgressColor2 = value;
                Invalidate();
            }
        }

        public _ProgressShape ProgressShape
        {
            get { return ProgressShapeVal; }
            set
            {
                ProgressShapeVal = value;
                Invalidate();
            }
        }

        #endregion
        #region EventArgs

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetStandardSize();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetStandardSize();
        }

        protected override void OnPaintBackground(PaintEventArgs p)
        {
            base.OnPaintBackground(p);
        }

        #endregion

        public CircularProgressBar()
        {
            Size = new Size(80, 80);
            Font = new Font("Segoe UI", 10);
            MinimumSize = new Size(30, 30);
            DoubleBuffered = true;
            isNeedTextValue = true;
        }

        private void SetStandardSize()
        {
            int _Size = Math.Max(Width, Height);
            Size = new Size(_Size, _Size);
        }

        public void Increment(int Val)
        {
            this._Value += Val;
            Invalidate();
        }

        public void Decrement(int Val)
        {
            this._Value -= Val;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Bitmap bitmap = new Bitmap(this.Width, this.Height))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.Clear(this.BackColor);
                    using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, this._ProgressColor1, this._ProgressColor2, LinearGradientMode.ForwardDiagonal))
                    {
                        using (Pen pen = new Pen(brush, 8f))
                        {
                            switch (this.ProgressShapeVal)
                            {
                                case _ProgressShape.Round:
                                    pen.StartCap = LineCap.Round;
                                    pen.EndCap = LineCap.Round;
                                    break;

                                case _ProgressShape.Flat:
                                    pen.StartCap = LineCap.Flat;
                                    pen.EndCap = LineCap.Flat;
                                    break;
                            }
                            graphics.DrawArc(pen, 0x12, 0x12, (this.Width - 0x23) - 2, (this.Height - 0x23) - 2, -90, (int)Math.Round((double)((360.0 / ((double)this._Maximum)) * this._Value)));
                        }
                    }

                    //using (LinearGradientBrush brush2 = new LinearGradientBrush(this.ClientRectangle, Color.FromArgb(0x34, 0x34, 0x34), Color.FromArgb(0x34, 0x34, 0x34), LinearGradientMode.Vertical))
                    //{
                    //    graphics.FillEllipse(brush2, 0x18, 0x18, (this.Width - 0x30) - 1, (this.Height - 0x30) - 1);
                    //}

                    if (isNeedTextValue)
                    {
                        SizeF MS = graphics.MeasureString(Convert.ToString(Convert.ToInt32((100 / _Maximum) * _Value)), Font);
                        graphics.DrawString(Convert.ToString(Convert.ToInt32((100 / _Maximum) * _Value)), Font, TextColor, Convert.ToInt32(Width / 2 - MS.Width / 2), Convert.ToInt32(Height / 2 - MS.Height / 2));
                    }
                    
                    e.Graphics.DrawImage(bitmap, 0, 0);
                    graphics.Dispose();
                    bitmap.Dispose();
                }
            }
        }

        public void RunProgress()
        {
            Width = 50;
            Height = 50;
            Visible = true;
            Value = 20;
            ProgressShape = CircularProgressBar._ProgressShape.Flat;
            
            var i = 0;
            var j = 0;
            Maximum = 100;
            var b = false;
            while (0 != 1)
            {
                if (i <= 100 && !b)
                {
                    i++;
                    j = 0;
                    Value = i;
                }
                else
                {
                    b = true;
                    j++;
                    i = 0;
                    Value = 100 - j;
                    if (Value == 0) b = false;
                }

                Update();
                Application.DoEvents();
                Thread.Sleep(20);
            }
        }
    }

#endregion
}