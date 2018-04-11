using Microsoft.Xna.Framework.Input;

namespace FF.Models
{
    /*
     * [Flags]
     * public enum
     * -> Better ?
     */
    public class Input
    {
        public Keys Down { get; set; }

        public Keys Left { get; set; }

        public Keys Right { get; set; }

        public Keys Up { get; set; }
    }
}
