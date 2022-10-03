using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    public class ColorChip
    {
        public Color StartColor { get; private set; }
        public Color EndColor { get; private set; }

        public ColorChip(Color startColor, Color endColor)
        {
            this.StartColor = startColor;
            this.EndColor = endColor;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", this.StartColor, this.EndColor);
        }
    }
}