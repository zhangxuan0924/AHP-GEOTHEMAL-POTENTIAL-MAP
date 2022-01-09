
namespace F
{
    internal class GetNormalizedWeights
    {
        internal double[] GetNormalized(double[] assignedWeights)
        {
            double[,] A = new double[7, 7];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    A[i, j] = assignedWeights[i] / assignedWeights[j];
                }
            }
            double[] productOfRows = new double[7] { 1, 1, 1, 1, 1, 1, 1 };

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    productOfRows[i] *= A[i, j];
                }
            }
            double sumProductOfRows = 0;

            for (int i = 0; i < 3; i++)
            {
                sumProductOfRows += productOfRows[i];
            }
            double[] normalizedWeights = new double[3];
            for (int i = 0; i < 3; i++)
            {
                normalizedWeights[i] = productOfRows[i] / sumProductOfRows;
            }
            return normalizedWeights;

        }
    }
}