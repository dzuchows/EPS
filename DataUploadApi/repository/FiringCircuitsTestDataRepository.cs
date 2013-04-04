using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUploadApi.repository
{
    public class FiringCircuitsTestDataRepository
    {

        public FiringCircuitsTestDataRepository()
        {
            
        }

        public void save(FiringCircuitsTest test)
        {

            SqlConnection connection = new SqlConnection();
            SqlParameter param;
            SqlCommand command = new SqlCommand();

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

                command.Parameters.Clear();

                String commandText =
                    " INSERT INTO testdata_firingcircuits(module_test_id, measurement_id, batteryname, circuit, program, starttime, endtime, testsection, " +
                    " comment, orderno, producer, type, nominalvoltage, nominalcurrent, nominalcapacity, cells, maximumvoltage, gassingvoltage, " +
                    " breakvoltage, chargefactor, impedance, coldcrankingcurrent, energydensity ) " +
                    " VALUES ( @module_test_id, @measurement_id, @batteryname, @circuit, @program, @starttime, @endtime, @testsection, @comment, @orderno, " +
                    " @producer, @type, @nominalvoltage, @nominalcurrent, @nominalcapacity, @cells, @maximumvoltage, @gassingvoltage, @breakvoltage, @chargefactor, " +
                    " @impedance, @coldcrankingcurrent, @energydensity); select cast(scope_identity() as int) ";

                command.CommandText = commandText;

                param = new SqlParameter("module_test_id", SqlDbType.Int);
                param.Value = test.MeasurementId;
                command.Parameters.Add(param);

                param = new SqlParameter("measurement_id", SqlDbType.Int);
                param.Value = test.MeasurementId;
                command.Parameters.Add(param);

                param = new SqlParameter("batteryname", SqlDbType.VarChar, 256);
                param.Value = test.BatteryName;
                command.Parameters.Add(param);

                param = new SqlParameter("circuit", SqlDbType.VarChar, 256);
                param.Value = test.Circuit;
                command.Parameters.Add(param);

                param = new SqlParameter("program", SqlDbType.VarChar, 256);
                param.Value = test.Program;
                command.Parameters.Add(param);

                param = new SqlParameter("starttime", SqlDbType.DateTime);
                param.Value = test.StartTime;
                command.Parameters.Add(param);

                param = new SqlParameter("endtime", SqlDbType.DateTime);
                param.Value = test.EndTime;
                command.Parameters.Add(param);

                param = new SqlParameter("testsection", SqlDbType.VarChar, 256);
                param.Value = test.TestSection;
                command.Parameters.Add(param);

                param = new SqlParameter("comment", SqlDbType.VarChar, 256);
                param.Value = test.Comment;
                command.Parameters.Add(param);

                param = new SqlParameter("orderno", SqlDbType.VarChar, 256);
                param.Value = test.OrderNo;
                command.Parameters.Add(param);

                param = new SqlParameter("producer", SqlDbType.VarChar, 256);
                param.Value = test.Producer;
                command.Parameters.Add(param);

                param = new SqlParameter("type", SqlDbType.VarChar, 256);
                param.Value = test.Type;
                command.Parameters.Add(param);

                param = new SqlParameter("nominalvoltage", SqlDbType.Float);
                param.Value = test.NominalVoltage;
                command.Parameters.Add(param);

                param = new SqlParameter("nominalcurrent", SqlDbType.Float);
                param.Value = test.NominalCurrent;
                command.Parameters.Add(param);

                param = new SqlParameter("nominalcapacity", SqlDbType.Float);
                param.Value = test.NominalCapacity;
                command.Parameters.Add(param);

                param = new SqlParameter("cells", SqlDbType.VarChar, 256);
                param.Value = test.Cells;
                command.Parameters.Add(param);

                param = new SqlParameter("maximumvoltage", SqlDbType.Float);
                param.Value = test.MaximumVoltage;
                command.Parameters.Add(param);

                param = new SqlParameter("gassingvoltage", SqlDbType.Float);
                param.Value = test.GassingVoltage;
                command.Parameters.Add(param);

                param = new SqlParameter("breakvoltage", SqlDbType.Float);
                param.Value = test.BreakVoltage;
                command.Parameters.Add(param);

                param = new SqlParameter("chargefactor", SqlDbType.Float);
                param.Value = test.ChargeFactor;
                command.Parameters.Add(param);

                param = new SqlParameter("impedance", SqlDbType.Float);
                param.Value = test.Impedance;
                command.Parameters.Add(param);

                param = new SqlParameter("coldcrankingcurrent", SqlDbType.Float);
                param.Value = test.ColdCrankingCurrent;
                command.Parameters.Add(param);

                param = new SqlParameter("energydensity", SqlDbType.Float);
                param.Value = test.EnergyDensity;
                command.Parameters.Add(param);

                param = new SqlParameter("testdata_firingcircuits_id", SqlDbType.Int);
                param.Value = testId;
                command.Parameters.Add(param);

                int firingCircuitsTestId = (int) command.ExecuteScalar();

                commandText = " INSERT INTO testdata_firingcircuits_results(timestamp, step, status, " +
                              " progtime, steptime, cycle, cyclelevel, [procedure], voltagev, currenta, " +
                              " ahaccu, ahcha, ahdch, ahstep, energy, whstep, testdata_firingcircuits_id )" +
                              " VALUES ( @timestamp, @step, @status, @progtime, @steptime, @cycle, @cyclelevel, " +
                              " @procedure, @voltagev, @currenta, @ahaccu, @ahcha, @ahdch, @ahstep, @energy, " +
                              " @whstep, @testdata_firingcircuits_id) ";

                command.CommandText = commandText;

                foreach (FiringCircuitsTestData t in test.TestResults)
                {
                    command.Parameters.Clear();

                    param = new SqlParameter("timestamp", SqlDbType.DateTime);
                    param.Value = t.TimeStamp;
                    command.Parameters.Add(param);

                    param = new SqlParameter("step", SqlDbType.Int);
                    param.Value = t.Step;
                    command.Parameters.Add(param);

                    param = new SqlParameter("status", SqlDbType.VarChar, 256);
                    param.Value = t.Status;
                    command.Parameters.Add(param);

                    param = new SqlParameter("progtime", SqlDbType.VarChar, 32);
                    param.Value = t.ProgTime;
                    command.Parameters.Add(param);

                    param = new SqlParameter("steptime", SqlDbType.VarChar, 32);
                    param.Value = t.StepTime;
                    command.Parameters.Add(param);

                    param = new SqlParameter("cycle", SqlDbType.Int);
                    param.Value = t.Cycle;
                    command.Parameters.Add(param);

                    param = new SqlParameter("cyclelevel", SqlDbType.Int);
                    param.Value = t.CycleLevel;
                    command.Parameters.Add(param);

                    param = new SqlParameter("procedure", SqlDbType.VarChar, 256);
                    param.Value = t.Procedure;
                    command.Parameters.Add(param);

                    param = new SqlParameter("voltagev", SqlDbType.Float);
                    param.Value = t.Voltage;
                    command.Parameters.Add(param);

                    param = new SqlParameter("currenta", SqlDbType.Float);
                    param.Value = t.CurrentA;
                    command.Parameters.Add(param);

                    param = new SqlParameter("ahaccu", SqlDbType.Float);
                    param.Value = t.AhAccu;
                    command.Parameters.Add(param);

                    param = new SqlParameter("ahcha", SqlDbType.Float);
                    param.Value = t.AhCha;
                    command.Parameters.Add(param);

                    param = new SqlParameter("ahdch", SqlDbType.Float);
                    param.Value = t.AhDch;
                    command.Parameters.Add(param);

                    param = new SqlParameter("ahstep", SqlDbType.Float);
                    param.Value = t.AhStep;
                    command.Parameters.Add(param);

                    param = new SqlParameter("energy", SqlDbType.Float);
                    param.Value = t.Energy;
                    command.Parameters.Add(param);

                    param = new SqlParameter("whstep", SqlDbType.Float);
                    param.Value = t.WhStep;
                    command.Parameters.Add(param);

                    param = new SqlParameter("testdata_firingcircuits_id", SqlDbType.Int);
                    param.Value = firingCircuitsTestId;
                    command.Parameters.Add(param);

                    command.ExecuteNonQuery();
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
