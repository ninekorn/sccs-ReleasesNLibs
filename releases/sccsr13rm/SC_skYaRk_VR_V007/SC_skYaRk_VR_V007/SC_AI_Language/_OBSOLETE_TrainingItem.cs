using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_skYaRk_VR_Edition_v003
{
    /// <summary>
    /// Training item used in creation of the 
    /// training set of a percerptron.
    /// </summary>
    class TrainingItem
    {
        /// <summary>
        /// Inputs in the perceptron.
        /// </summary>
        public double[] Inputs { get; private set; }

        /// <summary>
        /// Expected output
        /// </summary>
        public bool Output { get; private set; }

        /// <summary>
        /// Create a training item.
        /// </summary>
        /// <param name="expectedOutput">Expected output class</param>
        /// <param name="inputs">Input parameters to the perceptron</param>
        public TrainingItem(bool expectedOutput, params double[] inputs)
        {
            this.Inputs = inputs;
            this.Output = expectedOutput;
        }
    }
}
