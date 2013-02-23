using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataUploadService
{
    public class ArbinTestDataRepository
    {
        public ArbinTestDataRepository()
        {
        }

        public void save(ArbinTest test) 
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
                command.CommandText = "INSERT INTO module_test(test_machine_ID, test_name, channel_num,upload_timestamp) values(@test_machine_ID, @test_name, @channel_num, getdate()); select cast(scope_identity() as int)";
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
                int testId = (int)command.ExecuteScalar();
              //  int testId = Convert.ToInt32(command.Parameters["ID"].Value.ToString());

                command.CommandText = " INSERT INTO testdata_arbin(datapoint, testtime, [datetime], steptime, stepindex, cycleindex, " +
                        "[current], voltage, [power], [load], chargecapacity, dischargecapacity, chargeenergy, " +
                        "dischargeenergy, dvdt, internalresistance, isfcdata, acimpedance, module_test_ID) " +
                        "values( @datapoint, @testtime, @datetime, @steptime, @stepindex, @cycleindex, " +
                        "@current, @voltage, @power, @load, @chargecapacity, @dischargecapacity, @chargeenergy, " +
                        "@dischargeenergy, @dvdt, @internalresistance, @isfcdata, @acimpedance, @module_test_ID) ";

                int count=0;

                foreach (ArbinTestData t in test.TestResults)
                {
                    command.Parameters.Clear();

                    param = new SqlParameter("datapoint", SqlDbType.Int);
                    param.Value = t.DataPoint;
                    command.Parameters.Add(param);

                    param = new SqlParameter("testtime", SqlDbType.Float);
                    param.Value = t.TestTime;
                    command.Parameters.Add(param);

                    param = new SqlParameter("datetime", SqlDbType.DateTime);
                    param.Value = System.DateTime.Now;
                    command.Parameters.Add(param);

                    param = new SqlParameter("steptime", SqlDbType.Float);
                    param.Value = t.StepTime;
                    command.Parameters.Add(param);

                    param = new SqlParameter("stepindex", SqlDbType.Int);
                    param.Value = t.StepIndex;
                    command.Parameters.Add(param);

                    param = new SqlParameter("cycleindex", SqlDbType.Int);
                    param.Value = t.CycleIndex;
                    command.Parameters.Add(param);

                    param = new SqlParameter("current", SqlDbType.Float);
                    param.Value = t.Current;
                    command.Parameters.Add(param);

                    param = new SqlParameter("voltage", SqlDbType.Float);
                    param.Value = t.Voltage;
                    command.Parameters.Add(param);

                    param = new SqlParameter("power", SqlDbType.Float);
                    param.Value = t.Power;
                    command.Parameters.Add(param);

                    param = new SqlParameter("load", SqlDbType.Float);
                    param.Value = t.Load;
                    command.Parameters.Add(param);

                    param = new SqlParameter("chargecapacity", SqlDbType.Float);
                    param.Value = t.ChargeCapacity;
                    command.Parameters.Add(param);

                    param = new SqlParameter("dischargecapacity", SqlDbType.Float);
                    param.Value = t.DischargeCapacity;
                    command.Parameters.Add(param);

                    param = new SqlParameter("chargeenergy", SqlDbType.Float);
                    param.Value = t.ChargeEnergy;
                    command.Parameters.Add(param);

                    param = new SqlParameter("dischargeenergy", SqlDbType.Float);
                    param.Value = t.DischargeEnergy;
                    command.Parameters.Add(param);

                    param = new SqlParameter("dvdt", SqlDbType.Float);
                    param.Value = t.Dvdt;
                    command.Parameters.Add(param);

                    param = new SqlParameter("internalresistance", SqlDbType.Float);
                    param.Value = t.InternalResistance;
                    command.Parameters.Add(param);

                    param = new SqlParameter("isfcdata", SqlDbType.Float);
                    param.Value = t.IsfcData;
                    command.Parameters.Add(param);

                    param = new SqlParameter("acimpedance", SqlDbType.Float);
                    param.Value = t.Acimpedance;
                    command.Parameters.Add(param);

                    param = new SqlParameter("module_test_ID", SqlDbType.Int);
                    param.Value = testId;
                    command.Parameters.Add(param);

                    command.ExecuteNonQuery();
                    count++;

                    if (count % 100 == 0)
                    {
                        Console.WriteLine("{0} of {1} inserted: {2}", count, test.TestResults.Count, DateTime.Now.ToString());
                    }
                }
            }
            finally
            {
                command.Dispose();
                connection.Close();
            }


        }
    }
}
