﻿using System;


 namespace ExcellParser
 {
     namespace ExcellParser
     {
         public class ArrayOperations
         {
             public string[] getRow(string[,] data, int rowNumber)
             {
                 int size = data.GetLength(1);
                 string[] frow = new string[size];
                 for (int i = 0; i < size; i++)
                 {
                     frow[i] = data[rowNumber, i];
                 }

                 return frow;
             }

             public string[] getColumn(string[,] data, int colNumber)
             {
                 int size = data.GetLength(0);
                 string[] fcol = new string[size];
                 for (int i = 0; i < size; i++)
                 {
                     fcol[i] = data[i, colNumber];
                 }

                 return fcol;
             }

             public void removeFirstEmpty(ref String[,] data)
             {
                 int N = data.GetLength(0);
                 int M = data.GetLength(1);
                 for (int i = 0; i < N; i++)
                 {
                     if (isAllempty(getRow(data, 0)))
                     {
                         deleteRow(0, ref data);
                     }

                     else
                     {
                         break;
                     }
                 }

                 for (int i = 0; i < M; i++)
                 {
                     if (isAllempty(getColumn(data, 0)))
                     {
                         deleteColumn(0, ref data);
                     }

                     else
                     {
                         break;
                     }
                 }
             }

             public bool isAllempty(String[] d)
             {
                 for (int i = 0; i < d.Length; i++)
                 {
                     var words = d[i].Split(new char[] { ' ' });
                     var s1 = String.IsNullOrEmpty(words[1]);
                     var s2=words[1] == Double.NaN.ToString();
                     if (!s1 && !s2)
                     {
                         return false;
                     }
                 }
                 return true;
             }


             public void deleteRow(int rowToRemove, ref String[,] originalArray)
             {
                 String[,] result = new String[originalArray.GetLength(0) - 1, originalArray.GetLength(1)];

                 for (int i = 0, j = 0; i < originalArray.GetLength(0); i++)
                 {
                     if (i == rowToRemove)
                         continue;

                     for (int k = 0, u = 0; k < originalArray.GetLength(1); k++)
                     {
                         result[j, u] = originalArray[i, k];
                         u++;
                     }

                     j++;
                 }
                 originalArray = result;
             }

             public void deleteColumn(int columnToRemove, ref String[,] originalArray)
             {
                 String[,] result = new String[originalArray.GetLength(0), originalArray.GetLength(1) - 1];

                 for (int i = 0, j = 0; i < originalArray.GetLength(0); i++)
                 {
                     for (int k = 0, u = 0; k < originalArray.GetLength(1); k++)
                     {
                         if (k == columnToRemove)
                             continue;
                         result[j, u] = originalArray[i, k];
                         u++;
                     }

                     j++;
                 }

                 originalArray = result;
             }
         }
     }
 }