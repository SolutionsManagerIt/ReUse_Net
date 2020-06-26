using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
using ReUse_Std.AppDataModels.Common;
using ReUse_Std.Common;
using System.Data.Entity;
using ReUse_Net_Data.Platform;
using ReUse_Std.Base;
using ReUse_Std.AppDataModels.Feats;
using ReUse_Std.AppDataModels.Logging;
using ReUse_Std.Base.Performance;

namespace TestingNetConsoleApp.Tests
{
    public static class AppStart
    {
        /// <summary>
        /// Test common app data storage
        /// </summary>
        public static async Task Ga(this int Ensure)
        {
            //await 0.Gap();
            await 0.Gl();

        }


        /// <summary>
        /// Test common app data storage
        /// </summary>
        public static async Task Gap(this int Ensure)
        {
            var qs1 = "name=Testing_DB_AppModel1";

            var c10 = qs1.Gsa();
            var q1 = Ta();
            var r1 = await q1.Ua(qs1);
            var c11 = qs1.Gsa();

        }

        /// <summary>
        /// Test common Logging data storage
        /// </summary>
        public static async Task Gl(this int Ensure)
        {
            var qs2 = "name=Testing_DB_LoggingModel1";

            var ct = Tcx();
            var tc = true.Ns().sA().N("TestingNetConsoleApp");
            var cm = 1.M("AppStart", "Ga uuuu");
            await tc.R(qs2, ct, 1, async (a, x) =>
            {
                var g1 = a.L.Pc(x);
                var g2 = a.L.Pp(x);
                var g3 = a.L.En(x);
                await new Task<bool>(() => true);
                a.L.E("Error message 1", new Exception("test exc 11"), x, cm);
                a.L.I("info message 1", x, cm);
                throw new Exception("Test Exception 444");
            }, 0.M("AppStart", "Ga", "R"));

            var c21 = qs2.Gsl();
        }


        #region test data

        /// <summary>
        /// Test App Data
        /// </summary>
        public static App[] Ta()
        {
            var a1 = new App();
            a1.B = new Bn() { G = _.g, S = "Bn 1111", O = _.g }.L(new Bn() { G = _.g, S = "Bn 222", O = _.g });
            a1.E = new En() { A = Ets.A, E = "En 1111" }.L(new En() { A = Ets.Rc, E = "En 222" });
            a1.L = new Sq() { T = 20, D = "dbs L 1111", S = "server L 11" }.L(new Sq() { T = 30, D = "dbs L 222", S = "server L 22" });
            a1.Q = new Sq() { T = 20, D = "dbs Q 1111", S = "server Q 11" }.L(new Sq() { T = 30, D = "dbs Q 222", S = "server Q 22" });
            return a1.A();
        }

        /// <summary>
        /// Test Logs Data
        /// </summary>
        public static IDictionary<int, Cx> Tcx()
        {
            return 1.D(true.Ns().sA().X());
        }

        ///// <summary>
        ///// Test Logs Data
        ///// </summary>
        //public static Lst[] Tl()
        //{
        //    var r = _.l<Lst>();

        //    var tc = true._Ls();
        //    tc.G(e => eGsl)

        //    //var a1 = new Lst();
        //    //a1.C = new Cde() { C = "Class 11", M = new Cme() { G = _.g, M = "Method 1111" }.L(new Cme() { G = _.g, M = "Method 1122" }) }
        //    //    .L(new Cde() { C = "Class 22", M = new Cme() { G = _.g, M = "Method 2211" }.L(new Cme() { G = _.g, M = "Method 2222" }) });
        //    //a1.E = new Err() { As = 4, C = "Err 1111" }.L(new Err() { As = 33, C = "Err 222" });
        //    //a1.En = new Env() { Ct = 20, Os = "dbs L 1111", S = "server L 11" }.L(new Sq() { T = 30, D = "dbs L 222", S = "server L 22" });
        //    //a1.Q = new Sq() { T = 20, D = "dbs Q 1111", S = "server Q 11" }.L(new Sq() { T = 30, D = "dbs Q 222", S = "server Q 22" });


        //    return r.a();
        //}

        #endregion

        #region common methods

        /// <summary>
        /// Run new Async common MethodToRunAsync using new context with logging settings and save logs after completed
        /// </summary>
        public static async Task R<T>(this Sld Settings, string ConnectionName, IDictionary<T, Cx> Contexts, T ContextKey, f<Ax<T>, Cx, Task> MethodToRunAsync, Mx MethodContext = null , bool Ensure = true)
        {
            var rs = ConnectionName.Gsl();
            var a = Settings.N(async e => await e.ua(ConnectionName), Contexts);

            var rs1 = a.r(async x =>
            {
                await MethodToRunAsync(a, x);
                return 1;
            }, ContextKey, MethodContext);
            await a.L.Sva();
        }

        /// <summary>
        /// Run new Sync common MethodToRunSync using new context with logging settings and save logs after completed
        /// </summary>
        public static void r<T>(this Sld Settings, string ConnectionName, IDictionary<T, Cx> Contexts, T ContextKey, v<Ax<T>, Cx> MethodToRunSync, Mx MethodContext = null, bool Ensure = true)
        {
            var rs = ConnectionName.Gsl();
            var a = Settings.N(async e => await e.ua(ConnectionName), Contexts);

            var rs1 = a.r(x =>
            {
                MethodToRunSync(a, x);
                return 1;
            }, ContextKey, MethodContext);
            a.L.Sv();
        }


        /// <summary>
        /// Get common app data storage
        /// </summary>
        public static IEnumerable<App> Gsa(this string ConnectionName, bool Ensure = true)
        {
            return ConnectionName.Gd<App>(s => s.I(a => a.B, a => a.E, a => a.Q, a => a.L), null, Ensure);
        }

        /// <summary>
        /// Get common app Logs storage
        /// </summary>
        public static IEnumerable<Lst> Gsl(this string ConnectionName, bool Ensure = true)
        {            
            return ConnectionName.Gd<Lst, Cx, Mx>(s => s.I(a => a.C, a => a.E, a => a.E, a => a.P, a => a.I)
            .I(a => a.X, a => a.S)
            .I(a => a.Hc, a => a.Hr, a => a.Hs, a => a.Hb, a => a.En)
            .I(a => a.Pd, a => a.Pr, a => a.Wi, a => a.Wp, a => a.Wpr), 
                s => s.I(a => a.S), 
                s => s.I(a => a.A), s => s.LstId == null, Ensure);
        } 
        #endregion
    }
}
