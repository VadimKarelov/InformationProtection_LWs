namespace LW1
{
    public static class Cipher
    {
        /// <summary>
        /// Шифрование строки методом полибианского квадрата
        /// </summary>        
        /// <returns>Возвращается True если строка успешно зашифрована</returns>
        public static bool PolybianSquare(char[,] matrix, string inputLine, out string outputLine)
        {
            outputLine = "";

            if (matrix is null || matrix.GetLength(0) == 0 || matrix.GetLength(1) == 0)
            {
                return false;
            }           

            foreach (char symbol in inputLine)
            {
                if (symbol == ' ')
                {
                    outputLine += ' ';
                    continue;
                }

                // проходим по столбикам
                bool isFind = false;
                for (int j = 0; j < matrix.GetLength(1) && !isFind; j++)
                {
                    for (int i = 0; i < matrix.GetLength(0) && !isFind; i++)
                    {
                        if (matrix[i, j] == symbol)
                        {
                            isFind = true;
                            // проверка на последнюю строку
                            if (i + 1 == matrix.GetLength(0))
                            {
                                outputLine += matrix[0, j];
                            }
                            else
                            {
                                outputLine += matrix[i + 1, j];
                            }
                        }
                    }
                }

                // не найден символ в таблице -> нельзя закодировать
                if (!isFind)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
