using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataUploadApi.model;

namespace DataUploadApi.repository
{
    public class PECTestDataRepository
    {

        public PECTestDataRepository()
        {
            
        }

        public void save(PECTest test)
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
                command.CommandText =
                    "INSERT INTO module_test(test_machine_ID, test_name, channel_num,upload_timestamp) values(@test_machine_ID, @test_name, @channel_num, getdate()); select cast(scope_identity() as int)";
                connection.Open();

                param = new SqlParameter("test_machine_ID", SqlDbType.Int);
                param.Value = test.TestMachineId;
                command.Parameters.Add(param);

                param = new SqlParameter("test_name", SqlDbType.NVarChar, 32);
                param.Value = test.TestName;
                command.Parameters.Add(param);

                param = new SqlParameter("channel_num", SqlDbType.Int);
                param.Value = test.ChannelNum;
                command.Parameters.Add(param);

                command.Connection = connection;
                int testId = (int) command.ExecuteScalar();
                //  int testId = Convert.ToInt32(command.Parameters["ID"].Value.ToString());

                command.Parameters.Clear();

                String commandText =
                    "INSERT INTO testdata_pec(module_test_id, regime_name, regime_suffix, regime_cellsize, regime_version, starttime, endtime) " +
                    " VALUES (@module_test_id, @regime_name, @regime_suffix, @regime_cellsize, @regime_version, @starttime, @endtime); " +
                    " select cast(scope_identity() as int) ";

                command.CommandText = commandText;

                param = new SqlParameter("module_test_id", SqlDbType.Int);
                param.Value = testId;
                command.Parameters.Add(param);

                param = new SqlParameter("regime_name", SqlDbType.VarChar, 256);
                param.Value = test.TestRegimeName;
                command.Parameters.Add(param);

                param = new SqlParameter("regime_suffix", SqlDbType.VarChar, 256);
                param.Value = test.TestRegimeSuffix;
                command.Parameters.Add(param);

                param = new SqlParameter("regime_cellsize", SqlDbType.VarChar, 256);
                param.Value = test.TestRegimeCellSize;
                command.Parameters.Add(param);

                param = new SqlParameter("regime_version", SqlDbType.Int);
                param.Value = test.TestRegimeVersion;
                command.Parameters.Add(param);

                param = new SqlParameter("starttime", SqlDbType.DateTime);
                param.Value = test.StartTime;
                command.Parameters.Add(param);

                param = new SqlParameter("endtime", SqlDbType.DateTime);
                if (test.EndTime != null)
                {
                    param.Value = test.EndTime;
                }
                else
                {
                    param.Value = DBNull.Value;
                }
                command.Parameters.Add(param);


                int modulePECTestId = (int) command.ExecuteScalar();

                commandText = "INSERT INTO testdata_pec_results(testdata_pec_id, testnumber, test, step, cycle, " +
                              " totaltime, cyclechannel, cycledischargetime, voltage, [current], chargecapacityah, dischargecapacityah, " +
                              " chargecapacitywh, dischargecapacitywh) " +
                              " values( @testdata_pec_id, @testnumber, @test, @step, @cycle, @totaltime, @cyclechannel, @cycledischargetime, " +
                              " @voltage, @current, @chargecapacityah, @dischargecapacityah, @chargecapacitywh, @dischargecapacitywh) ";

                command.CommandText = commandText;
                foreach (KeyValuePair<String, List<PECTestData>> i in test.TestResults)
                {
                    var results = i.Value;

                    foreach (PECTestData t in results)
                    {
                        command.Parameters.Clear();

                        param = new SqlParameter("testdata_pec_id", SqlDbType.Int);
                        param.Value = modulePECTestId;
                        command.Parameters.Add(param);

                        param = new SqlParameter("testnumber", SqlDbType.VarChar, 256);
                        param.Value = t.TestNumber;
                        command.Parameters.Add(param);

                        param = new SqlParameter("test", SqlDbType.Int);
                        param.Value = t.Test;
                        command.Parameters.Add(param);

                        param = new SqlParameter("step", SqlDbType.Int);
                        param.Value = t.Step;
                        command.Parameters.Add(param);

                        param = new SqlParameter("cycle", SqlDbType.Int);
                        param.Value = t.Cycle;
                        command.Parameters.Add(param);

                        param = new SqlParameter("totaltime", SqlDbType.Float);
                        param.Value = t.TotalTime;
                        command.Parameters.Add(param);

                        param = new SqlParameter("cyclechannel", SqlDbType.Float);
                        param.Value = t.CycleChannel;
                        command.Parameters.Add(param);

                        param = new SqlParameter("cycledischargetime", SqlDbType.Int);
                        param.Value = t.CycleDischargeTime;
                        command.Parameters.Add(param);

                        param = new SqlParameter("voltage", SqlDbType.Float);
                        param.Value = t.Voltage;
                        command.Parameters.Add(param);

                        param = new SqlParameter("current", SqlDbType.Float);
                        param.Value = t.Current;
                        command.Parameters.Add(param);

                        param = new SqlParameter("chargecapacityah", SqlDbType.Float);
                        param.Value = t.ChargeCapacityAh;
                        command.Parameters.Add(param);

                        param = new SqlParameter("dischargecapacityah", SqlDbType.Float);
                        param.Value = t.DischargeCapacityAh;
                        command.Parameters.Add(param);

                        param = new SqlParameter("chargecapacitywh", SqlDbType.Float);
                        param.Value = t.ChargeCapacityWh;
                        command.Parameters.Add(param);

                        param = new SqlParameter("dischargecapacitywh", SqlDbType.Float);
                        param.Value = t.DischargeCapacityWh;
                        command.Parameters.Add(param);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                command.Dispose();
                connection.Close();
            }

        }

    }
}
