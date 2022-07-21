using GTA;
using GTA.Math;
using VectorThis.Classes;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VectorThis
{
    public class VectorThis : Script
    {
        private ScriptSettings GetKeys;

        private List<Vector3> MyPos = new List<Vector3>();
        private List<Vector4> My4Pos = new List<Vector4>();

        private readonly string MyIni = "" + Directory.GetCurrentDirectory() + "/Scripts/VectorKeys.ini";

        Keys kColect = Keys.K;
        Keys kList = Keys.L;

        public VectorThis()
        {
            KeyDown += PushBut;
            LoadIni();
        }
        private Vector3 ZDown(Vector3 zDown)
        {
            float f = zDown.Z - 1;
            return new Vector3(zDown.X, zDown.Y, f);
        }
        private void CaptureLocal()
        {
            Vector3 vMePos = Game.Player.Character.Position;
            if (!Game.Player.Character.IsInVehicle())
                vMePos = ZDown(vMePos);
            MyPos.Add(vMePos);
            My4Pos.Add(new Vector4(vMePos.X, vMePos.Y, vMePos.Z, Game.Player.Character.Heading));
            UI.Notify("No in list == " + MyPos.Count);
        }
        private void PrintLocal()
        {
            string sLocalFile = "" + Directory.GetCurrentDirectory() + "/Scripts";

            string sName = "MyList_";
            int iBe = 0;
            string[] sList = Directory.GetFiles(sLocalFile);
            for (int i = 0; i < sList.Count(); i++)
            {
                if (sList[i].Contains(sName))
                    iBe++;
            }

            sName = sLocalFile + "/" + sName + iBe + ".txt";

            for (int i = 0; i < MyPos.Count; i++)
            {
                Writetext(" new Vector3(" + MyPos[i].X + "f, " + MyPos[i].Y + "f, " + MyPos[i].Z + "f);", sName);
            }
            MyPos.Clear();

            Writetext("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-", sName);

            for (int i = 0; i < My4Pos.Count; i++)
            {
                Writetext(" new Vector4(" + My4Pos[i].X + "f, " + My4Pos[i].Y + "f, " + My4Pos[i].Z + "f, " + +My4Pos[i].R + "f);", sName);
            }
            My4Pos.Clear();

            UI.Notify("New File == " + sName);
        }
        private void LoadIni()
        {
            try
            {
                if (File.Exists(MyIni))
                {
                    GetKeys = ScriptSettings.Load(MyIni);
                    kColect = GetKeys.GetValue<Keys>("KeyCodes", "KColect", Keys.K);
                    kList = GetKeys.GetValue<Keys>("KeyCodes", "KList", Keys.L);
                }
            }
            catch
            {

            }
        }
        private void Writetext(string stext, string sFile)
        {
            try//     
            {
                using (StreamWriter tex = File.AppendText(sFile))
                    tex.WriteLine(stext);
            }
            catch
            {

            }
        }
        private void PushBut(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == kColect)
                CaptureLocal();

            else if (e.KeyCode == kList)
                PrintLocal();
        }
    }
}
