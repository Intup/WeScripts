using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeScriptWrapper;
using WeScript.SDK.UI;
using WeScript.SDK.UI.Components;
using WeScript.SDK.Utils;
using SharpDX;

using static WeCSGlobal.Memory.Memory;
using static WeCSGlobal.Memory.Signature;
using static WeCSGlobal.Menu.Menu;
using static WeCSGlobal.Config.Aimbot;
using static WeCSGlobal.Config.TriggerBot;
using static WeCSGlobal.Config.Visual;
using static WeCSGlobal.Config.Misc;
using static WeCSGlobal.Memory.Address;
using static WeCSGlobal.Objects.Engine;
using static WeCSGlobal.Objects.Entity.BaseEntity;
using static WeCSGlobal.Objects.Entity.BasePlayer;
using static WeCSGlobal.Objects.Angle;
using static WeCSGlobal.Objects.GlowObject;

using WeCSGlobal.Objects.Entity;
using static WeCSGlobal.Objects.Client;
using WeCSGlobal.Objects;

namespace WeCSGlobal
{
    class Hacks
    {
        public static float M_PI_F = (180.0f / Convert.ToSingle(System.Math.PI));
        public static Vector2 AimTarg2D = new Vector2(0, 0); 
        public static Vector3 AimTarg3D = new Vector3(0, 0, 0);

        private static double GetDistance3D(Vector3 myPos, Vector3 enemyPos)
        {
            Vector3 vector = new Vector3(myPos.X - enemyPos.X, myPos.Y - enemyPos.Y, myPos.Z - enemyPos.Z);
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);
        }

        private static double GetDistance2D(Vector2 pos1, Vector2 pos2)
        {
            Vector2 vector = new Vector2(pos1.X - pos2.X, pos1.Y - pos2.Y);
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }
        public static Vector3 ClampAngle(Vector3 angle)
        {
            while (angle.Y > 180) angle.Y -= 360;
            while (angle.Y < -180) angle.Y += 360;

            if (angle.X > 89.0f) angle.X = 89.0f;
            if (angle.X < -89.0f) angle.X = -89.0f;

            angle.Z = 0f;

            return angle;
        }

        public static Vector3 NormalizeAngle(Vector3 angle)
        {
            while (angle.X < -180.0f) angle.X += 360.0f;
            while (angle.X > 180.0f) angle.X -= 360.0f;

            while (angle.Y < -180.0f) angle.Y += 360.0f;
            while (angle.Y > 180.0f) angle.Y -= 360.0f;

            while (angle.Z < -180.0f) angle.Z += 360.0f;
            while (angle.Z > 180.0f) angle.Z -= 360.0f;

            return angle;
        }

        public static Vector3 CalcAngle(Vector3 playerPosition, Vector3 enemyPosition, Vector3 aimPunch, Vector3 vecView, float yawRecoilReductionFactory, float pitchRecoilReductionFactor)
        {
            Vector3 delta = new Vector3(playerPosition.X - enemyPosition.X, playerPosition.Y - enemyPosition.Y, (playerPosition.Z + vecView.Z) - enemyPosition.Z);

            Vector3 tmp = Vector3.Zero;
            tmp.X = Convert.ToSingle(System.Math.Atan(delta.Z / System.Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y))) * 57.295779513082f - aimPunch.X * yawRecoilReductionFactory;
            tmp.Y = Convert.ToSingle(System.Math.Atan(delta.Y / delta.X)) * M_PI_F - aimPunch.Y * pitchRecoilReductionFactor;
            tmp.Z = 0;

            if (delta.X >= 0.0) tmp.Y += 180f;

            tmp = NormalizeAngle(tmp);
            tmp = ClampAngle(tmp);

            return tmp;
        }

        private static Vector3 ReadBonePos(IntPtr playerPtr, int boneIDX)
        {
            Vector3 targetVec = new Vector3(0, 0, 0);
            var BoneMatrixPtr = WeScriptWrapper.Memory.ReadPointer(csHandle, (IntPtr)(playerPtr.ToInt64() + 0x26A8), isWow64Process); //m_dwBoneMatrix
            if (BoneMatrixPtr != IntPtr.Zero)
            {
                targetVec.X = WeScriptWrapper.Memory.ReadFloat(csHandle, (IntPtr)(BoneMatrixPtr.ToInt64() + 0x30 * boneIDX + 0x0C));
                targetVec.Y = WeScriptWrapper.Memory.ReadFloat(csHandle, (IntPtr)(BoneMatrixPtr.ToInt64() + 0x30 * boneIDX + 0x1C));
                targetVec.Z = WeScriptWrapper.Memory.ReadFloat(csHandle, (IntPtr)(BoneMatrixPtr.ToInt64() + 0x30 * boneIDX + 0x2C));
            }
            return targetVec;
        }



        public static EntityList entityList = new EntityList();
        internal static void OnRenderer(int fps, EventArgs args)
        {
            if (!ProcessExists) return;
            if ((!isGameOnTop) && (!isOverlayOnTop)) return;
            if (!InGame) return;

            //Misc
            if (cMisc.aintiflash.Enabled)
            {
                FlashAlpha = 0.0f;
            }

           
            if (isPlayerMoving())
            {
                if ((cMisc.BunnyKey.Enabled) && Flags == 257)
                {
 
                   ForceJump(true);
                   ForceForward(true);
                   Input.SleepWS(10);
                   ForceJump(false);
                   ForceForward(false);
                }
            }
            

            int mPlayers = MaxPlayer;
            for (int i = 0; i < mPlayers; i++)
            {
                BaseEntity baseEntity = entityList[i];
                if (baseEntity == null) continue;
                Player entity = new Player(baseEntity);
                if (entity == null) continue;
                if (entity.Dormant) continue;
                if (entity.Health <= 0) continue;

                if (entity.Team != Team)
                {
                    RenderColor rco = new RenderColor();
                    rco.r = Color.Red.R;
                    rco.g = Color.Red.G;
                    rco.b = Color.Red.B;
                    rco.a = 255;
                    entity.RenderColor = rco;
                }

                ModelAmbientIntensity = 1;

                //Radar
                if (entity.Team != Team)
                {
                    if (!entity.Spotted) entity.Spotted = true;
                }

                //TringgerBot 
                if (cTrigger.TriggerEnable.Enabled)
                {
                    if (cTrigger.TriggerKey.Enabled)
                    {
                        if (CrosshairID > 0 && CrosshairID < MaxPlayer + 2)
                        {
                            baseEntity = entityList[CrosshairID - 1];
                            if (baseEntity == null) return;
                            Player crossEntity = new Player(baseEntity);
                            if (crossEntity == null) return;
                            if (crossEntity != null && crossEntity.Team != Team)
                            {
                                Input.SleepWS(1);
                                ForceAttack(true);
                                Input.SleepWS(5);
                                ForceAttack(false);
                            }
                        }
                    }
                }

                //Aimbot and ESP
                if ((entity.Health > 0) && (entity.Dormant == false))
                {
                    //Factures Aimbot
                    double fClosestPos = 999999;
                    AimTarg2D = new Vector2(0, 0);
                    AimTarg3D = new Vector3(0, 0, 0);

                    var f_modelHeight = WeScriptWrapper.Memory.ReadFloat(csHandle, (IntPtr)(entity.Base + 0x33C));
                    var headPos_fake = new Vector3(entity.VectorOrigin.X, entity.VectorOrigin.Y, entity.VectorOrigin.Z + f_modelHeight);
                    var matrix = WeScriptWrapper.Memory.ReadMatrix(csHandle, (IntPtr)(dwViewMatrix_Offs.ToInt64() + 0xB0));

                    var m_bIsDefusing = WeScriptWrapper.Memory.ReadBool(csHandle, (IntPtr)entity.Base + 0x3930);
                    var myPunchAngles = WeScriptWrapper.Memory.ReadVector3(csHandle, (IntPtr)(LocalPlayerPtr + 0x302C));
                    var myEyePos = WeScriptWrapper.Memory.ReadVector3(csHandle, (IntPtr)(LocalPlayerPtr + 0x108));
                    var myAngles = WeScriptWrapper.Memory.ReadVector3(csHandle, (IntPtr)(LocalPlayerPtr + 0x31D8));
                    var myPos = WeScriptWrapper.Memory.ReadVector3(csHandle, (IntPtr)(LocalPlayerPtr + 0x138));

                    Vector2 vScreen_head = new Vector2(0, 0);
                    Vector2 vScreen_foot = new Vector2(0, 0);

                    //ESP
                    if (Renderer.WorldToScreen(headPos_fake, out vScreen_head, matrix, wndMargins, wndSize, W2SType.TypeD3D9))
                    {
                        Renderer.WorldToScreen(entity.VectorOrigin, out vScreen_foot, matrix, wndMargins, wndSize, W2SType.TypeD3D9);
                        {
                            if (cVisual.DrawTheVisuals.Enabled)
                            {
                                if (entity != null && entity.Team != Team)
                                {
                                    if (!m_bIsDefusing)
                                    {
                                        Renderer.DrawFPSBox(vScreen_head, vScreen_foot, (Team == 3) ? cVisual.ESPColor.Color : cVisual.ESPColor.Color, (f_modelHeight == 54.0f) ? BoxStance.crouching : BoxStance.standing, cVisual.DrawBoxThic.Value, cVisual.DrawBoxBorder.Enabled, cVisual.DrawBox.Enabled, entity.Health, cVisual.DrawBoxHP.Enabled ? 100 : 0, 0, 0);
                                    }
                                    else
                                    {
                                        Renderer.DrawFPSBox(vScreen_head, vScreen_foot, (Team == 3) ? Color.White : Color.White, BoxStance.crouching);
                                    }
                                }
                            }
                        }
                    }

                    //Aimbot
                    if (entity != null && entity.Team != Team)
                    {
                        Vector2 vScreen_aim = new Vector2(0, 0);
                        Vector3 targetVec = new Vector3(0, 0, 0);
                        var entityAddr = WeScriptWrapper.Memory.ReadPointer(csHandle, (IntPtr)(dwEntityList_Offs.ToInt64() + i * 0x10), isWow64Process);
                        switch (cAimbot.PriorityBone.Value)
                        {
                            case 0: //head
                                {
                                    targetVec = ReadBonePos(entityAddr, 8);
                                }
                                break;
                            case 1: //body
                                {
                                    targetVec = ReadBonePos(entityAddr, 0);
                                }
                                break;
                            default:
                                break;
                        }

                        var AimDist2D = GetDistance2D(vScreen_aim, GameCenterPos);
                        if (cAimbot.AimFov.Value < AimDist2D) continue;
                        if (AimDist2D < fClosestPos)
                        {
                            fClosestPos = AimDist2D;
                            AimTarg2D = vScreen_aim;
                            AimTarg3D = targetVec;
                        }
                        if (Renderer.WorldToScreen(headPos_fake, out vScreen_head, matrix, wndMargins, wndSize, W2SType.TypeD3D9))
                        {
                            if (cAimbot.AimKey.Enabled)
                            {
                                var dx = (GameCenterPos.X * 2) / 90;
                                var dy = (GameCenterPos.Y * 2) / 90;

                                var rx = GameCenterPos.X - (dx * ((myPunchAngles.Y)));
                                var ry = GameCenterPos.Y + (dy * ((myPunchAngles.X)));
                                double DistX = 0;
                                double DistY = 0;

                                DistX = (AimTarg2D.X) - rx;
                                DistY = (AimTarg2D.Y) - ry;

                                double slowDistX = DistX / (1.0f + (Math.Abs(DistX) / (1.0f + 12)));
                                double slowDistY = DistY / (1.0f + (Math.Abs(DistY) / (1.0f + 12)));
                                Input.mouse_eventWS(MouseEventFlags.MOVE, (int)slowDistX, (int)slowDistY, MouseEventDataXButtons.NONE, IntPtr.Zero);
                            }
                        }
                    }
                }
            }
        }
    }
}
