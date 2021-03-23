using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace circuit_png
{
    public static class DrawHelpers
    {
        /// <summary>
        /// Draw a resistor symbol
        /// </summary>
        /// <param name="target"></param>
        public static void DrawReistor(Graphics target, string[] subs)
        {
            GernericCoordinate<float> text = new GernericCoordinate<float>();
            text.ParseCoordiante(subs);
            float x1 = text.getX1(), y1 = text.getY1(), x2 = text.getX2(), y2 = text.getY2(),x3, y3,x4,y4;
            string val = text.getVal();
            float slope = (x2 != x1 && y2 != y1) ? (y2 - y1) / (x2 - x1) : 0;
            float segment = (x2 != x1) ? (x2 - x1) / 10 : (y2 != y1) ? (y2 - y1) / 10 : 0;
            float orthogonal = (x2 != x1 || y2 != y1) ? -(1 / slope) : 0;
            float length = (float)Math.Sqrt((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1)) / 5;
            float Xlength = (float)Math.Sqrt((length * length) / ((orthogonal * orthogonal) + 1));
            Pen bluePen = new Pen(Color.Blue, 2);
            if (y2 != y1 && x2 != x1)
            {
                target.DrawLine(bluePen, x1, y1, x3 = x1 + 2 * segment, y3 = y1 + 2 * segment * slope);
                target.DrawLine(bluePen, x3, y3, x4 = x3 + segment + Xlength, y4 = y3 + segment * slope + Xlength * orthogonal);
                target.DrawLine(bluePen, x4, y4, x3 = x1 + 4 * segment, y3 = y1 + 4 * segment * slope);
                target.DrawLine(bluePen, x3, y3, x4 = x3 + segment - Xlength, y4 = y3 + segment * slope - Xlength * orthogonal);
                target.DrawLine(bluePen, x4, y4, x3 = x1 + 6 * segment, y3 = y1 + 6 * segment * slope);
                target.DrawLine(bluePen, x3, y3, x4 = x3 + segment + Xlength, y4 = y3 + segment * slope + Xlength * orthogonal);
                target.DrawLine(bluePen, x4, y4, x3 = x1 + 8 * segment, y3 = y1 + 8 * segment * slope);
                target.DrawLine(bluePen, x3, y3, x2, y2);
                int ChangeAngle = (x1 < x2 && y1 < y2) ? (int)(Math.Atan(slope) * 180 / Math.PI) :
                                  (x1 > x2 && y1 < y2) ? (int)(Math.Atan(slope) * 180 / Math.PI) + 180 :
                                  (x1 > x2 && y1 > y2) ? (int)(Math.Atan(slope) * 180 / Math.PI) - 180 :
                                  (int)(Math.Atan(slope) * 180 / Math.PI) + 360;
                target.TranslateTransform(-x1, -y1, System.Drawing.Drawing2D.MatrixOrder.Append);
                target.RotateTransform(ChangeAngle, System.Drawing.Drawing2D.MatrixOrder.Append);
                target.TranslateTransform(x1, y1, System.Drawing.Drawing2D.MatrixOrder.Append);
                int abslength = (x2 == x1 && y2 == y1) ? 0 : (int)Math.Sqrt((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1)) / 50;
                DrawText(target, (float)(x1 + x2) / 2, (float)y1 + abslength * 5, "R = " + val, 0);
                target.ResetTransform();
            }
            else if (y2 == y1 && x2 != x1)
            {
                target.DrawLine(bluePen, x1, y1, x3 = x1 + 2 * segment, y3 = y1);
                target.DrawLine(bluePen, x3, y3, x4 = x3 + segment, y4 = y3 + 2 * segment);
                target.DrawLine(bluePen, x4, y4, x3 = x4 + segment, y3 = y2);
                target.DrawLine(bluePen, x3, y3, x4 = x3 + segment, y4 = y3 - 2 * segment);
                target.DrawLine(bluePen, x4, y4, x3 = x4 + segment, y3 = y2);
                target.DrawLine(bluePen, x3, y3, x4 = x3 + segment, y4 = y3 + 2 * segment);
                target.DrawLine(bluePen, x4, y4, x3 = x4 + segment, y3 = y2);
                target.DrawLine(bluePen, x3, y3, x2, y2);
                DrawText(target, (float)(x1 + x2) / 2, (float)y1, "R = " + val, 0);
            }
            else if (y2 != y1 && x2 == x1)
            {
                target.DrawLine(bluePen, x1, y1, x3 = x1, y3 = y1 + 2 * segment);
                target.DrawLine(bluePen, x3, y3, x4 = x3 + 2 * segment, y4 = y3 + segment);
                target.DrawLine(bluePen, x4, y4, x3 = x2, y3 = y4 + segment);
                target.DrawLine(bluePen, x3, y3, x4 = x3 - 2 * segment, y4 = y3 + segment);
                target.DrawLine(bluePen, x4, y4, x3 = x2, y3 = y4 + segment);
                target.DrawLine(bluePen, x3, y3, x4 = x3 + 2 * segment, y4 = y3 + segment);
                target.DrawLine(bluePen, x4, y4, x3 = x2, y3 = y4 + segment);
                target.DrawLine(bluePen, x3, y3, x2, y2);
                DrawText(target, (float)x1, (float)(y1 + y2)/2, "R = " + val, 90);
            }
            else
            {
                Console.WriteLine("Resistor can not draw at this coordinate");
                Console.ReadKey();
            }
            bluePen.Dispose();
        }
        /// <summary>
        /// Draw a voltage source symbol
        /// </summary>
        /// <param name="target"></param>
        public static void DrawVoltageSource(Graphics target, string[] subs)
        {
            GernericCoordinate<int> text = new GernericCoordinate<int>();
            text.ParseCoordiante(subs);
            int x1 = text.getX1(), y1 = text.getY1(), x2 = text.getX2(), y2 = text.getY2(), radius = 50;
            string val = text.getVal();
            Pen bluePen = new Pen(Color.Blue, 2);
            Rectangle rect = new Rectangle(x1 - radius, y1 - radius, 2 * radius, 2 * radius);
            target.DrawEllipse(bluePen, rect);
            target.DrawLine(bluePen, new PointF(x1 - radius / 2, y1 - radius / 2), new PointF(x1 + radius / 2, y1 - radius / 2));
            target.DrawLine(bluePen, new PointF(x1, y1 - radius), new PointF(x1, y1));
            target.DrawLine(bluePen, new PointF(x1 - radius / 2, y1 + radius / 2), new PointF(x1 + radius / 2, y1 + radius / 2));
            DrawText(target, (float)x1, (float)y1, "V = " + val, 0);
            bluePen.Dispose();
        }
        /// <summary>
        /// Draw an inductor symbol
        /// </summary>
        /// <param name="target"></param>
        public static void DrawInductor(Graphics target, string[] subs)
        {
            GernericCoordinate<int> text = new GernericCoordinate<int>();
            text.ParseCoordiante(subs);
            int x1 = text.getX1(), y1 = text.getY1(), x2 = text.getX2(), y2 = text.getY2();
            string val = text.getVal();
            int segment = (x2 == x1 && y2 == y1) ? 0 : (int)Math.Sqrt((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1)) / 50;
            float fsegment = (x2 == x1 && y2 == y1) ? 0 : (float)Math.Sqrt((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1)) / 50;
            Pen green = new Pen(Color.Green, 3);
            Pen bluePen = new Pen(Color.Blue, 3);
            Rectangle rect = new Rectangle(x1, y1, segment * 10, segment * 10);
            float startAngle = 180.0F, sweepAngle = 225.0F, MstartAngle = 135.0F, MsweepAngle = 270.0F,
                  EstartAngle = 135.0F, ChangeAngle = 0;

            if (x1 != x2 && y1 == y2)
            {
                target.DrawLine(bluePen, x1, y1, x1 + segment * 6, y1);

                if (x1 < x2)
                {
                    rect.Offset(segment * 6, -segment * 5);
                    target.DrawArc(bluePen, rect, startAngle, sweepAngle);
                    for (int i = 0; i < 3; i++)
                    {
                        rect.Offset(segment * 7, 0);
                        target.DrawArc(bluePen, rect, MstartAngle, MsweepAngle);
                    }
                    rect.Offset(segment * 7, 0);
                    target.DrawArc(bluePen, rect, EstartAngle, sweepAngle);
                    target.DrawLine(bluePen, x2 - segment * 6, y2, x2, y2);
                }
                else if (x1 > x2)
                {
                    ChangeAngle = -180.0F;
                    rect.Offset(segment * 16, segment * 5);
                    target.DrawArc(bluePen, rect, startAngle + ChangeAngle, sweepAngle);
                    for (int i = 0; i < 3; i++)
                    {
                        rect.Offset(segment * 7, 0);
                        target.DrawArc(bluePen, rect, MstartAngle + ChangeAngle, MsweepAngle);
                    }
                    rect.Offset(segment * 7, 0);
                    target.DrawArc(bluePen, rect, EstartAngle + ChangeAngle, sweepAngle);
                    target.DrawLine(bluePen, x2 - segment * 6, y2, x2, y2);
                }
                DrawText(target, (float)(x1 + x2) / 2 - segment * 10, (float)y1 + segment * 5, "L = " + val, 0);
            }
            else if (x1 == x2 && y1 != y2)
            {
                target.DrawLine(bluePen, x1, y1, x1, y1 + segment * 6);
                if (y1 < y2)
                {
                    ChangeAngle = 90.0F;
                    rect.Offset(-segment * 5, segment * 6);
                    target.DrawArc(bluePen, rect, startAngle + ChangeAngle, sweepAngle);
                    for (int i = 0; i < 3; i++)
                    {
                        rect.Offset(0, segment * 7);
                        target.DrawArc(bluePen, rect, MstartAngle + ChangeAngle, MsweepAngle);
                    }
                    rect.Offset(0, segment * 7);
                    target.DrawArc(bluePen, rect, EstartAngle + ChangeAngle, sweepAngle);
                }
                else if (y1 > y2)
                {
                    ChangeAngle = -90.0F;
                    rect.Offset(segment * 5, segment * 16);
                    target.DrawArc(bluePen, rect, startAngle + ChangeAngle, sweepAngle);
                    for (int i = 0; i < 3; i++)
                    {
                        rect.Offset(0, segment * 7);
                        target.DrawArc(bluePen, rect, MstartAngle + ChangeAngle, MsweepAngle);
                    }
                    rect.Offset(0, segment * 7);
                    target.DrawArc(bluePen, rect, EstartAngle + ChangeAngle, sweepAngle);
                }
                target.DrawLine(bluePen, x2, y2 - segment * 6, x2, y2);
                DrawText(target, (float)x1 + segment * 5, (float)(y1 + y2)/2 - segment * 10, "L = " + val, 90);
            }
            else if (x1 != x2 && y1 != y2)
            {
                double slope = (double)(y2 - y1) / (x2 - x1);
                ChangeAngle = (x1 < x2 && y1 < y2) ? (int)(Math.Atan(slope) * 180 / Math.PI) :
                                  (x1 > x2 && y1 < y2) ? (int)(Math.Atan(slope) * 180 / Math.PI) + 180 :
                                  (x1 > x2 && y1 > y2) ? (int)(Math.Atan(slope) * 180 / Math.PI) - 180 :
                                  (int)(Math.Atan(slope) * 180 / Math.PI) + 360;
                target.TranslateTransform(-x1, -y1, System.Drawing.Drawing2D.MatrixOrder.Append);
                target.RotateTransform(ChangeAngle, System.Drawing.Drawing2D.MatrixOrder.Append);
                target.TranslateTransform(x1, y1, System.Drawing.Drawing2D.MatrixOrder.Append);
                target.DrawLine(bluePen, x1, y1, x1 + segment * 6, y1);
                rect.Offset(segment * 6, -segment * 5);
                target.DrawArc(bluePen, rect, startAngle, sweepAngle);
                for (int i = 0; i < 3; i++)
                {
                    rect.Offset(segment * 7, 0);
                    target.DrawArc(bluePen, rect, MstartAngle, MsweepAngle);
                }
                rect.Offset(segment * 7, 0);

                target.DrawArc(bluePen, rect, EstartAngle, sweepAngle);
                target.DrawLine(bluePen, x1 + segment * 44, y1, x1 + fsegment * 50, y1);
                DrawText(target, (float)x1 + segment * 25, (float)y1 + segment * 5, "L = " + val, 0);
                target.ResetTransform();
            }
            bluePen.Dispose();
        }
        /// <summary>
        /// Draw a capacitor symbol
        /// </summary>
        /// <param name="target"></param>
        public static void DrawCapacitor(Graphics target, string[] subs)
        {
            GernericCoordinate<float> text = new GernericCoordinate<float>();
            text.ParseCoordiante(subs);
            float x1 = text.getX1(), y1 = text.getY1(), x2 = text.getX2(), y2 = text.getY2(), x3, y3;
            string val = text.getVal();
            float slope = (x2 != x1 && y2 != y1) ? (y2 - y1) / (x2 - x1) : 0;
            float orthogonal = (x2 != x1 && y2 != y1) ? -(1 / slope) : 0;
            float segment = (x2 != x1) ? (x2 - x1) / 10 : (y2 != y1) ? (y2 - y1) / 10 : 0;
            Pen green = new Pen(Color.Green, 3);
            if (x2 != x1 && y2 != y1)
            {
                target.DrawLine(green, x1, y1, x3 = x1 + 4 * segment, y3 = y1 + (4 * segment) * slope);
                target.DrawLine(green, x3 - segment, y3 - segment * orthogonal, x3 + segment, y3 + segment * orthogonal);
                target.DrawLine(green, x2, y2, x3 = x2 - 4 * segment, y3 = y2 - (4 * segment) * slope);
                target.DrawLine(green, x3 - segment, y3 - segment * orthogonal, x3 + segment, y3 + segment * orthogonal);
                DrawText(target, (float)(x1 + x2) / 2 , (float)(y1 + y2) / 2, "F = " + val, 0);
            }
            else if (x2 == x1 && y2 != y1)
            {
                target.DrawLine(green, x1, y1, x3 = x1, y3 = y1 + 4 * segment);
                target.DrawLine(green, x3 - segment, y3, x3 + segment, y3);
                target.DrawLine(green, x2, y2, x3 = x2, y3 = y2 - 4 * segment);
                target.DrawLine(green, x3 - segment, y3, x3 + segment, y3);
                DrawText(target, (float)x1, (float)(y1 + y2) / 2, "F = " + val, 90);
            }
            else if (x2 != x1 && y2 == y1)
            {
                target.DrawLine(green, x1, y1, x3 = x1 + 4 * segment, y3 = y1);
                target.DrawLine(green, x3, y3 - segment, x3, y3 + segment);
                target.DrawLine(green, x2, y2, x3 = x2 - 4 * segment, y3 = y2);
                target.DrawLine(green, x3, y3 - segment, x3, y3 + segment);
                DrawText(target, (float)(x1 + x2) / 2, (float)y1, "F = " + val, 0);
            }
            else
            {
                Console.WriteLine("Resistor can not draw at this coordinate");
                Console.ReadKey();
            }
            green.Dispose();
        }
        /// <summary>
        /// Draw a generic impedance symbol
        /// </summary>
        /// <param name="target"></param>
        public static void DrawImpedance(Graphics target, string[] subs)
        {
            GernericCoordinate<int> coordinate = new GernericCoordinate<int>();
            coordinate.ParseCoordiante(subs);
            int x1 = coordinate.getX1(), y1 = coordinate.getY1(), x2 = coordinate.getX2(), y2 = coordinate.getY2(), x3, y3;
            string val = coordinate.getVal();
            int segment = (x2 == x1 && y2 == y1) ? 0 : (int)Math.Sqrt((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1)) / 50;
            float fsegment = (x2 == x1 && y2 == y1) ? 0 : (float)Math.Sqrt((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1)) / 50;
            Pen bluePen = new Pen(Color.Blue, 3);

            if (x1 != x2 && y1 == y2)
            {
                if (x1>x2)
                {
                    int temp;
                    temp = x1;
                    x1 = x2;
                    x2 = temp;
                    temp = y1;
                    y1 = y2;
                    y2 = temp;
                }
                target.DrawLine(bluePen, x1, y1, x3 = x1 + segment * 6, y3 = y1);
                target.DrawRectangle(bluePen, x3, y3 - segment * 5,segment * 38, segment * 10);
                target.DrawLine(bluePen, x2 - segment * 6, y2, x2, y2);
                DrawText(target, (float)(x1 + x2) / 2 - segment * 10, (float)y1 + segment * 5, "Z = " + val, 0);
            }
            else if (x1 == x2 && y1 != y2)
            {

                if (y1 > y2)
                {
                    int temp;
                    temp = x1;
                    x1 = x2;
                    x2 = temp;
                    temp = y1;
                    y1 = y2;
                    y2 = temp;
                }
                target.DrawLine(bluePen, x1, y1, x3 = x1, y3 = y1 + segment * 6);
                target.DrawRectangle(bluePen, x3 - segment * 5, y3, segment * 10, segment * 38);
                target.DrawLine(bluePen, x2, y2 - segment * 6, x2, y2);
                DrawText(target, (float)x1 + segment * 5, (float)(y1 + y2) / 2 - segment * 10, "Z = " + val, 90);
            }
            else if (x1 != x2 && y1 != y2)
            {
                if (x1 > x2)
                {
                    int temp;
                    temp = x1;
                    x1 = x2;
                    x2 = temp;
                    temp = y1;
                    y1 = y2;
                    y2 = temp;
                }
                double slope = (double)(y2 - y1) / (x2 - x1);
                int ChangeAngle = (x1 < x2 && y1 < y2) ? (int)(Math.Atan((y2 - y1) / (x2 - x1)) * 180 / Math.PI) :
                                  (x1 > x2 && y1 < y2) ? (int)(Math.Atan((y2 - y1) / (x2 - x1)) * 180 / Math.PI) + 180 :
                                  (x1 > x2 && y1 > y2) ? (int)(Math.Atan((y2 - y1) / (x2 - x1)) * 180 / Math.PI) - 180 :
                                  (int)(Math.Atan(slope) * 180.0 / Math.PI) + 360;
                target.TranslateTransform(-x1, -y1, System.Drawing.Drawing2D.MatrixOrder.Append);
                target.RotateTransform(ChangeAngle, System.Drawing.Drawing2D.MatrixOrder.Append);
                target.TranslateTransform(x1, y1, System.Drawing.Drawing2D.MatrixOrder.Append);

                target.DrawLine(bluePen, x1, y1, x3 = x1 + segment * 6, y3 = y1);
                target.DrawRectangle(bluePen, x3, y3 - segment * 5, segment * 38, segment * 10);
                target.DrawLine(bluePen, x1 + segment * 44, y1, x1 + fsegment * 50, y1);
                DrawText(target, (float)(x1 + x2) / 2 - segment * 10, (float)y1 + segment * 5, "Z = " + val, 0);
                target.ResetTransform();
            }
            bluePen.Dispose();
        }
        public static void DrawWire(Graphics target, string[] subs)
        {
            GernericCoordinate<int> coordinate = new GernericCoordinate<int>();
            coordinate.ParseCoordiante(subs);
            int x1 = coordinate.getX1(), y1 = coordinate.getY1(), x2 = coordinate.getX2(), y2 = coordinate.getY2();
            Pen bluePen = new Pen(Color.Red, 3);
            target.DrawLine(bluePen, x1, y1, x2, y2);
            bluePen.Dispose();
        }
        public static void DrawText(Graphics target, float x, float y, string text, int ChangeAngle)
        {
            Font drawFont = new Font("Arial", 12);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = (ChangeAngle == 90)? StringFormatFlags.DirectionVertical : StringFormatFlags.DisplayFormatControl;
            target.DrawString(text, drawFont, drawBrush, x, y, drawFormat);
        }
    }
}