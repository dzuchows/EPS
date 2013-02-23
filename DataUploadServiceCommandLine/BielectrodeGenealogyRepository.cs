﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUploadService
{
    public class BielectrodeGenealogyRepository
    {

        public void saveWeightData(IList<ElectrodeWeight> data)
        {
            foreach (ElectrodeWeight electrodeWeight in data)
            {
                saveWeight(electrodeWeight);
            }
        }


        public void saveThicknessData(IList<ElectrodeThickness> data)
        {
            foreach (ElectrodeThickness electrodeThickness in data)
            {
                saveThickness(electrodeThickness);
            }
        }

        public void saveWeight(ElectrodeWeight electrodeWeight)
        {
            SqlConnection connection = new SqlConnection();
            SqlParameter param;
            SqlCommand command = new SqlCommand();

            SqlParameter testIdField = new SqlParameter("ID", SqlDbType.Int);
            testIdField.Direction = ParameterDirection.Output;

            try
            {
                connection.ConnectionString = Configuration.QADataConnectionString;
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "BielectrodeWeight_Update";
                connection.Open();

                param = new SqlParameter("@bielectrode_id", SqlDbType.VarChar, 10);
                param.Value = electrodeWeight.BielectrodeNum;
                command.Parameters.Add(param);

                param = new SqlParameter("@pos_patty_wt", SqlDbType.Float);
                param.Value = electrodeWeight.PositiveBipattiesWeight;
                command.Parameters.Add(param);

                param = new SqlParameter("@neg_patty_wt", SqlDbType.Float);
                param.Value = electrodeWeight.NegativeBipattiesWeight;
                command.Parameters.Add(param);

                param = new SqlParameter("@grid_wire_wt", SqlDbType.Float);
                param.Value = electrodeWeight.GridWireWeight;
                command.Parameters.Add(param);

                param = new SqlParameter("@precure_wt", SqlDbType.Float);
                param.Value = electrodeWeight.PrecureBielectrodeWeight;
                command.Parameters.Add(param);

                param = new SqlParameter("@operator", SqlDbType.VarChar, 50);
                param.Value = electrodeWeight.Operators;
                command.Parameters.Add(param);

                param = new SqlParameter("@datetime", SqlDbType.DateTime);
                param.Value = electrodeWeight.Timestamp;
                command.Parameters.Add(param);
              
                command.ExecuteNonQuery();
            }
            finally
            {
                command.Dispose();
                connection.Close();
            }
        }

        public void saveThickness(ElectrodeThickness electrodeThickness)
        {
            SqlConnection connection = new SqlConnection();
            SqlParameter param;
            SqlCommand command = new SqlCommand();

            SqlParameter testIdField = new SqlParameter("ID", SqlDbType.Int);
            testIdField.Direction = ParameterDirection.Output;

            try
            {
                connection.ConnectionString = Configuration.QADataConnectionString;
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "BielectrodeThickness_Update";
                connection.Open();

                param = new SqlParameter("@bielectrode_id", SqlDbType.VarChar, 10);
                param.Value = electrodeThickness.BielectrodeNum;
                command.Parameters.Add(param);

                param = new SqlParameter("@thickness01", SqlDbType.Float);
                param.Value = electrodeThickness.Thickness_1;
                command.Parameters.Add(param);

                param = new SqlParameter("@thickness02", SqlDbType.Float);
                param.Value = electrodeThickness.Thickness_2;
                command.Parameters.Add(param);

                param = new SqlParameter("@thickness03", SqlDbType.Float);
                param.Value = electrodeThickness.Thickness_3;
                command.Parameters.Add(param);

                param = new SqlParameter("@thickness04", SqlDbType.Float);
                param.Value = electrodeThickness.Thickness_4;
                command.Parameters.Add(param);

                param = new SqlParameter("@thickness05", SqlDbType.Float);
                param.Value = electrodeThickness.Thickness_5;
                command.Parameters.Add(param);

                param = new SqlParameter("@thickness06", SqlDbType.Float);
                param.Value = electrodeThickness.Thickness_6;
                command.Parameters.Add(param);

                param = new SqlParameter("@thickness07", SqlDbType.Float);
                param.Value = electrodeThickness.Thickness_7;
                command.Parameters.Add(param);

                param = new SqlParameter("@thickness08", SqlDbType.Float);
                param.Value = electrodeThickness.Thickness_8;
                command.Parameters.Add(param);

                param = new SqlParameter("@thickness09", SqlDbType.Float);
                param.Value = electrodeThickness.Thickness_9;
                command.Parameters.Add(param);

                command.ExecuteNonQuery();
            }
            finally
            {
                command.Dispose();
                connection.Close();
            }
        }


        public IList<ElectrodeGenealogySummary> getElectrodeGenealogySummary()
        {
            string sqlText = "  select IDs.Bielectrode_ID, " + 
                             " case when bw.Bielectrode_ID is null then 'N' else 'Y' end as 'WeightData_YN', " + 
                             " case when bt.Bielectrode_ID is null then 'N' else 'Y' end as 'ThicknessData_YN' " + 
                             " from " + 
                             "  ( " + 
                             "     SELECT Bielectrode_ID from Bielectrode_thickness " + 
                             "         UNION " + 
                             "  SELECT Bielectrode_ID from Bielectrode_weight " + 
                             " ) IDs " + 
                             " LEFT JOIN bielectrode_weight bw on IDs.Bielectrode_ID = bw.Bielectrode_ID " + 
                             " LEFT JOIN bielectrode_thickness bt on IDs.Bielectrode_ID = bt.bielectrode_ID " +
                             " order by IDs.Bielectrode_ID ";

            IList<ElectrodeGenealogySummary> data = new List<ElectrodeGenealogySummary>();
            SqlConnection connection = new SqlConnection();
            SqlCommand command = new SqlCommand();
            SqlDataReader dr;
             try
            {
                connection.ConnectionString = Configuration.QADataConnectionString;
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sqlText;
                connection.Open();

                dr = command.ExecuteReader();

                 ElectrodeGenealogySummary summary;
                 while (dr.Read() ) {
                     summary = new ElectrodeGenealogySummary();

                     summary.BielectrodeId = Convert.ToString(dr[0]);
                     summary.HasWeightData = Convert.ToString(dr[1]);
                     summary.HasThicknessData = Convert.ToString(dr[2]);

                     data.Add(summary);
                 }


             } finally {
                 command.Dispose();
                 connection.Close();
             }

             return data;
        }
    }
}
