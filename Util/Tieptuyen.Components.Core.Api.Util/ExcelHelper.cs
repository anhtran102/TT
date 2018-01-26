using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace Tieptuyen.Components.Core.Api.Util
{
    public static class ExelHelper
    {
        #region READ LIST PRODUCT FROM FILE
        public static DataTable ReadDataTableFromSheet(string filePath, string sheetName)
        {
            string connStr = string.Empty;
            string pathOnly = string.Empty;
            string fileName = string.Empty;
            string query = string.Empty;
            if (string.IsNullOrEmpty(sheetName))
            {
                return null;
            }

           DataTable productTable = new DataTable();
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    return null;
                }
                FileInfo file = new FileInfo(filePath);
                if (!file.Exists)
                {
                    return null;
                }
                string extention = file.Extension;
                if (!string.IsNullOrEmpty(extention))
                {
                    extention = extention.ToUpper();
                }
                if (extention == ".XLSX")
                {
                    connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + "; Extended Properties=Excel 12.0;";
                }
                else if (extention == ".XLS")
                {
                    connStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" + filePath ;
                }
                else if (extention == ".CSV")
                {
                    pathOnly = Path.GetDirectoryName(filePath);
                    connStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly + ";Extended Properties=\"Text;HDR=yes\"";
                }
                using (OleDbConnection conn = new OleDbConnection(connStr))
                {
                    switch (extention)
                    {
                        case ".XLS":
                        case ".XLSX":
                            {
                                query = @"select * from [" + sheetName + "$]";
                                using (OleDbCommand command = new OleDbCommand(query, conn))
                                {
                                    //conn.Open();
                                    using (OleDbDataAdapter adp = new OleDbDataAdapter(command))
                                    {
                                        DataTable dt = new DataTable();
                                        adp.Fill(productTable);
                                    }
                                }
                            }
                            break;
                        case ".CSV":
                            {
                                fileName = Path.GetFileName(filePath);
                                query = @"SELECT * FROM [" + fileName + "]";
                                using (OleDbCommand command = new OleDbCommand(query, conn))
                                {
                                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                                    {
                                        adapter.Fill(productTable);
                                    }
                                }
                                break;
                            }                        
                    }
                }
                //productList = ExtractProductFromDataRow(productTable);
                return productTable;
            }
            catch(Exception e ){
                throw e;
            }           

        }
        #endregion
        /// <summary>
        /// Get sheets list from excel file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static DataTable GetSheetsFromWorkbook(string filePath)
        {
            string connStr = string.Empty;
            string pathOnly = string.Empty;
            string fileName = string.Empty;
            string query = string.Empty;

            DataTable dt = null;
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    return null;
                }
                FileInfo file = new FileInfo(filePath);
                if (!file.Exists)
                {
                    return null;
                }
                string extention = file.Extension;
                if (!string.IsNullOrEmpty(extention))
                {
                    extention = extention.ToUpper();
                }
                if (extention == ".XLSX")
                {
                    connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + "; Extended Properties=Excel 12.0;";
                }
                else if (extention == ".XLS")
                {
                    connStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" + filePath;
                }

                using (OleDbConnection conn = new OleDbConnection(connStr))
                {
                    conn.Open();                    
                    dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    conn.Close();
                }

                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// get connection string corresponde file type
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetConnectionStringFromFile(string filePath)
        {
            string connStr = string.Empty;
            string pathOnly = string.Empty;
            string fileName = string.Empty;
            string query = string.Empty;
            
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    return null;
                }
                FileInfo file = new FileInfo(filePath);
                if (!file.Exists)
                {
                    return null;
                }
                string extention = file.Extension;
                if (!string.IsNullOrEmpty(extention))
                {
                    extention = extention.ToUpper();
                }
                if (extention == ".XLSX")
                {
                    return connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + "; Extended Properties=Excel 12.0;";
                }
                else if (extention == ".XLS")
                {
                    return connStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" + filePath;
                }

                return string.Empty;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// get dataTable from sheet
        /// </summary>
        /// <param name="strConn"></param>
        /// <param name="sheet"></param>
        /// <returns></returns>
        public static DataTable getSheetData(string strConn, string sheet)
        {
            string query = "SELECT * FROM [" + sheet + "]";
            OleDbConnection objConn;
            OleDbDataAdapter oleDA;
            DataTable dt = new DataTable();
            objConn = new OleDbConnection(strConn);
            objConn.Open();
            oleDA = new OleDbDataAdapter(query, objConn);
            oleDA.Fill(dt);
            objConn.Close();
            oleDA.Dispose();
            objConn.Dispose();
            return dt;
        }
    }
}