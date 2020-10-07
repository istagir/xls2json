﻿using System;
using System.Collections.Generic;
 using System.IO;
using System.Linq;
 using System.Threading.Tasks;
 using Lab1_ping;
 using Newtonsoft.Json;
using OfficeOpenXml;


namespace ExcellParser
{
    public abstract class Parser
    {
        protected ArrayOperations _operations;
        protected Dictionary<string, Dictionary<string, List<string>>> dictionaryOut;
        List<Task> tasks = new List<Task>();
        MTaskShelder lcts;
        private TaskFactory factory;
        public Parser()
        {
            lcts = new MTaskShelder(100);
            factory = new TaskFactory(lcts);
            _operations=new ArrayOperations();
            dictionaryOut=new Dictionary<string, Dictionary<string, List<string>>>();
        }
        public String parse(String path)
        {
            using (ExcelPackage xlPackage =
                new ExcelPackage(new FileInfo(path)))
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                int c = xlPackage.Workbook.Worksheets.Count;
                string[] names=new string[c];
                for (int i = 0; i < c; i++)
                {
                    var myWorksheet = xlPackage.Workbook.Worksheets[i];
                    var totalRows = myWorksheet.Dimension.End.Row;
                    var totalColumns = myWorksheet.Dimension.End.Column;
                    
                    string[,] data=new string[totalRows,totalColumns];
                    for (int rowNum = 1; rowNum <= totalRows; rowNum++)
                    {
                        for (int colNum = 1; colNum <= totalColumns; colNum++)
                        {
                            var cell=myWorksheet.Cells[rowNum,colNum,rowNum,colNum].Select(c => c.Value == null ? string.Empty : c.Value.ToString());
                            String str=string.Join("", cell);
                            data[rowNum-1, colNum-1] = str;
                        }
                    }
                
                    Task<Dictionary<string,List<string>>> t1 =factory.StartNew(() => {
                        _operations.removeFirstEmpty(ref data); 
                        return createKeyValue(data);
                    });
                    tasks.Add(t1);
                    names[i] = myWorksheet.Name;
                }
                Task.WaitAll(tasks.ToArray());
                int k = 0;
                foreach (Task<Dictionary<string,List<string>>> task in tasks)
                {
                    dictionaryOut.Add(names[k],task.Result);
                    k++;
                }
                return JsonConvert.SerializeObject(dictionaryOut);
            }
        } 
       

        protected virtual  Dictionary<string,List<string>> createKeyValue(string[,] data)
        {
            return null;
        }

        protected void dictAdd(string key,string val,Dictionary<string,List<string>> inside)
        {
            if (!inside.ContainsKey(key))
            {
                List<String> s=new List<String>();
                s.Add(val);
                inside.Add(key,s);
            }
            else
            {
                inside[key].Add(val);
            }
        }
        protected bool check(string s)
        {
            return String.IsNullOrEmpty(s) || s == Double.NaN.ToString();
        }
    }
}