using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1
{
    namespace ExcellParser
    {
        public class ColumnParser : Parser
        {
            public ColumnParser() : base()
            {

            }

            protected override Dictionary<string, List<string>> createKeyValue(String[,] data)
            {
                int N = data.GetLength(0);
                int M = data.GetLength(1);
                Dictionary<string, List<string>> dictionaryIn = new Dictionary<string, List<string>>();
                String[] fcol = _operations.getColumn(data, 0);

                for (int rowNum = 0; rowNum < N; rowNum++)
                {
                    for (int colNum = 1; colNum < M; colNum++)
                    {
                        if (check(data[rowNum, colNum]))
                        {
                            if (!dictionaryIn.ContainsKey(fcol[rowNum]))
                            {
                                dictionaryIn.Add(fcol[rowNum],new List<string>());
                            }
                            continue;
                        }

                        dictAdd(fcol[rowNum], data[rowNum, colNum], dictionaryIn);
                    }
                }

                return dictionaryIn;
            }
        }
    }
}