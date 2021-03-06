﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronPython.Hosting;
using System.IO;
using MissionPlanner;

namespace MissionPlanner
{
    public class Script
    {
        Microsoft.Scripting.Hosting.ScriptEngine engine;
        Microsoft.Scripting.Hosting.ScriptScope scope;

        // keeps history
        MAVLink.mavlink_rc_channels_override_t rc = new MAVLink.mavlink_rc_channels_override_t();

        internal Utilities.StringRedirectWriter OutputWriter { get; private set; }

        public Script(bool redirectOutput = false)
        {
            Dictionary<string, object> options = new Dictionary<string, object>();
            options["Debug"] = true;

            if (engine != null)
                engine.Runtime.Shutdown();

            engine = Python.CreateEngine(options);
            scope = engine.CreateScope();

            var all = System.Reflection.Assembly.GetExecutingAssembly();
            engine.Runtime.LoadAssembly(all);
            scope.SetVariable("MAV", MainV2.comPort);
            scope.SetVariable("cs", MainV2.comPort.MAV.cs);
            scope.SetVariable("Script", this);
            scope.SetVariable("mavutil", this);
            scope.SetVariable("Joystick", MainV2.joystick);

            engine.CreateScriptSourceFromString("print 'hello world from python'").Execute(scope);
            engine.CreateScriptSourceFromString("print cs.roll").Execute(scope);

            if (redirectOutput)
            {
                //Redirect output through this writer
                //this writer will not actually write to the memorystreams
                OutputWriter = new Utilities.StringRedirectWriter();
                engine.Runtime.IO.SetErrorOutput(new MemoryStream(), OutputWriter);
                engine.Runtime.IO.SetOutput(new MemoryStream(), OutputWriter);
            }
            else
                OutputWriter = null;
        }

        public object mavlink_connection(string device, int baud = 115200, int source_system = 255,
            bool write = false, bool append = false,
            bool robust_parsing = true, bool notimestamps = false, bool input = true)
        {
            return null;
        }

        public object recv_match(string condition = null, string type = null, bool blocking = false)
        {
            return null;
        }

        public void Sleep(int ms)
        {
            System.Threading.Thread.Sleep(ms);
        }

        public void runScript(string filename)
        {
            try
            {
                Console.WriteLine("Run Script " + scope);
                engine.CreateScriptSourceFromFile(filename).Execute(scope);
                Console.WriteLine("Run Script Done");
            }
            catch (Exception e)
            {
                CustomMessageBox.Show("Error running script " + e.Message);
            }
        }

        public enum Conditional
        {
            NONE = 0,
            LT,
            LTEQ,
            EQ,
            GT,
            GTEQ,
            NEQ
        }

        public bool ChangeParam(string param, float value)
        {
            return MainV2.comPort.setParam(param, value);
        }

        public float GetParam(string param)
        {
            if (MainV2.comPort.MAV.param[param] != null)
                return (float)MainV2.comPort.MAV.param[param];

            return 0.0f;
        }

        public bool ChangeMode(string mode)
        {
            MainV2.comPort.setMode(mode);
            return true;
        }

        public bool WaitFor(string message, int timeout)
        {
            int timein = 0;
            while (!MainV2.comPort.MAV.cs.message.Contains(message))
            {
                System.Threading.Thread.Sleep(5);
                timein += 5;
                if (timein > timeout)
                    return false;
            }

            return true;
        }

        //Script.UpdateObstacles(async_radii_stationary, async_lat_stationary, async_lng_stationary, 
        //                          async_radii_moving, async_lat_moving, async_lng_moving)

        public bool UpdateObstacles(IList<Object> radii_stationary, IList<Object> lat_stationary, IList<Object> lng_stationary,
            IList<Object> h_stationary,
            IList<Object> radii_moving, IList<Object> lat_moving, IList<Object> lng_moving, IList<Object> h_moving)
        {
            //Console.WriteLine("updated obstacles");

            //radii is in feet. convert to meters
            //update stationary obstacles, just in case new ones pop up
            for (int i = 0; i < radii_stationary.Count; i++)
            {
                InteropData.InteropData_stationary.pushObjectStationary(i, (double)lat_stationary[i], (double)lng_stationary[i], (double)radii_stationary[i]* 0.3048, (double)h_stationary[i], true);
            }

            //update moving obstacles
            for (int i = 0; i < radii_moving.Count; i++)
            {
                //Console.Write(radii_stationary.ElementAt(i) + " ");
                //ObstacleObject obs = new ObstacleObject((double)lat_moving.ElementAt(i), (double)lng_moving.ElementAt(i), (double)radii_moving.ElementAt(i), false);
                //Console.WriteLine("begin write data");
                InteropData.InteropData_moving.pushObjectMoving(i, (double)lat_moving.ElementAt(i), (double)lng_moving.ElementAt(i), (double)radii_moving.ElementAt(i)* 0.3048, (double)h_moving[i], false);
                //Console.WriteLine(i + "\t" + obs.x + "\t" + obs.y + "\t" + obs.radius);
                //Console.WriteLine("pushed data");
                //obs = null;
            }
            //Console.WriteLine();
            return true;
        }

        public bool SendRC(int channel, ushort pwm, bool sendnow)
        {
            switch (channel)
            {
                case 1:
                    MainV2.comPort.MAV.cs.rcoverridech1 = pwm;
                    rc.chan1_raw = pwm;
                    break;
                case 2:
                    MainV2.comPort.MAV.cs.rcoverridech2 = pwm;
                    rc.chan2_raw = pwm;
                    break;
                case 3:
                    MainV2.comPort.MAV.cs.rcoverridech3 = pwm;
                    rc.chan3_raw = pwm;
                    break;
                case 4:
                    MainV2.comPort.MAV.cs.rcoverridech4 = pwm;
                    rc.chan4_raw = pwm;
                    break;
                case 5:
                    MainV2.comPort.MAV.cs.rcoverridech5 = pwm;
                    rc.chan5_raw = pwm;
                    break;
                case 6:
                    MainV2.comPort.MAV.cs.rcoverridech6 = pwm;
                    rc.chan6_raw = pwm;
                    break;
                case 7:
                    MainV2.comPort.MAV.cs.rcoverridech7 = pwm;
                    rc.chan7_raw = pwm;
                    break;
                case 8:
                    MainV2.comPort.MAV.cs.rcoverridech8 = pwm;
                    rc.chan8_raw = pwm;
                    break;
            }

            rc.target_component = MainV2.comPort.MAV.compid;
            rc.target_system = MainV2.comPort.MAV.sysid;

            if (sendnow)
            {
                MainV2.comPort.sendPacket(rc);
                System.Threading.Thread.Sleep(20);
                MainV2.comPort.sendPacket(rc);
            }

            return true;
        }
    }
}