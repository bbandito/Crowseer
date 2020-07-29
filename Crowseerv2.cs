using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Linq;

namespace Crowseer
{
    public class Seecrow : MonoBehaviour
    {
        public static Rect rc = new Rect(Screen.width / 2, 100, 350, 220);
        public bool menu = false;
        public int one;
        public int two = 50;
        public static Texture2D texture;
        public static Texture2D inactivetexture;
        public static Texture2D texture2;
        public static Texture2D Activetexture;
        public static Texture2D backgroundtexture;
        public static Texture2D background;
        public static Texture2D hoverbackground;
        public static Color HoverColor = new Color32(149, 0, 1, 255);
        public static Color BackGround = new Color32(64, 64, 64, 220);
        public static Color BackGroundColor = new Color32(13, 13, 13, 255);
        public static Color hoverColor = new Color32(26, 26, 26, 255);
        public static Color Color3 = new Color32(0, 149, 1, 255);
        public static GUIStyle MenuItemStyle;
        public static GUIStyle ActiveMenuItemStyle;

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public static string nameToAdd = "RemotePlayer";
        public static string BoxButton = "BOX ESP: ON";
        public static List<GameObject> badguys = new List<GameObject>();
        public static GameObject clientManager;
        public static Transform objects;
        public static Color AmbientColor = Color.white;
        public static Shader DefaultShader;
        public string Shit1;

        public static bool BoxEsp = true;
        private static Texture2D aaLineTex = null;
        private static Texture2D lineTex = null;
        private static Material blitMaterial = null;
        private static Material blendMaterial = null;

        public static void RectFilled(float x, float y, float width, float height, Texture2D text)
        {
            GUI.DrawTexture(new Rect(x, y, width, height), text);
        }
        public static void RectOutlined(float x, float y, float width, float height, Texture2D text, float thickness = 1f)
        {
            RectFilled(x, y, thickness, height, text);
            RectFilled(x + width - thickness, y, thickness, height, text);
            RectFilled(x + thickness, y, width - thickness * 2f, thickness, text);
            RectFilled(x + thickness, y + height - thickness, width - thickness * 2f, thickness, text);
        }

        public static void Box(float x, float y, float width, float height, Texture2D text, float thickness = 1f)
        {
            RectOutlined(x - width / 2f, y - height, width, height, text, thickness);
        }

        private bool IsVisable(GameObject origin, Vector3 toCheck)
        {
            RaycastHit hit;
            if (Physics.Linecast(Camera.main.transform.position, toCheck, out hit))
            {
                if (hit.collider.gameObject.layer == 22 || hit.collider.gameObject.layer == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static void GI(int id)
        {
            MenuItemStyle = new GUIStyle(GUI.skin.button);
            MenuItemStyle.fontStyle = FontStyle.Bold;
            MenuItemStyle.normal.textColor = Color.white;
            MenuItemStyle.normal.background = texture;
            MenuItemStyle.hover.textColor = Color.gray;
            MenuItemStyle.hover.background = inactivetexture;
            MenuItemStyle.active.background = inactivetexture;

            ActiveMenuItemStyle = new GUIStyle(GUI.skin.button);
            ActiveMenuItemStyle.fontStyle = FontStyle.Bold;
            ActiveMenuItemStyle.normal.textColor = Color.white;
            ActiveMenuItemStyle.normal.background = texture2;
            ActiveMenuItemStyle.hover.textColor = Color.gray;
            ActiveMenuItemStyle.hover.background = Activetexture;
            ActiveMenuItemStyle.active.background = Activetexture;

            GUIStyle TextStyle = new GUIStyle(GUI.skin.label);
            TextStyle.fontStyle = FontStyle.Bold;
            TextStyle.fontSize = 15;

            GUIStyle Button1;
            if (nameToAdd == "NPC") Button1 = ActiveMenuItemStyle;
            else Button1 = MenuItemStyle;

            GUIStyle Button2;
            if (nameToAdd == "RemotePlayer") Button2 = ActiveMenuItemStyle;
            else Button2 = MenuItemStyle;

            GUIStyle Button3;
            if (nameToAdd == "Both") Button3 = ActiveMenuItemStyle;
            else Button3 = MenuItemStyle;

            GUIStyle Button8;
            if (BoxButton.Contains("ON")) Button8 = ActiveMenuItemStyle;
            else Button8 = MenuItemStyle;

            GUI.DrawTexture(new Rect(0, 0, 350, 20), hoverbackground);
            GUI.Label(new Rect(60, 0, 350, 20), "Crowseer", TextStyle);
            GUI.Label(new Rect(39, 20, 140, 20), "TARGET TEAM:", TextStyle);
            if (GUI.Button(new Rect(20, 40, 140, 20), "NPC", Button1))
            {
                badguys.Clear();
                nameToAdd = "NPC";
            }
            if (GUI.Button(new Rect(20, 60, 140, 20), "Players", Button2))
            {
                badguys.Clear();
                nameToAdd = "RemotePlayer";
            }
            if (GUI.Button(new Rect(20, 80, 140, 20), "Both", Button3))
            {
                badguys.Clear();
                nameToAdd = "Both";
            }

            GUI.Label(new Rect(215, 20, 140, 20), "VISUALS:", TextStyle);
            if (GUI.Button(new Rect(180, 40, 140, 20), BoxButton, Button8))
            {
                if (BoxEsp)
                {
                    BoxEsp = false;
                    BoxButton = "BOX ESP: OFF";
                }
                else
                {
                    BoxEsp = true;
                    BoxButton = "BOX ESP: ON";
                }
            }
            GUI.DragWindow();
        }
        void Start()
        {
            texture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            texture.SetPixel(0, 0, Color.red);
            texture.SetPixel(1, 0, Color.red);
            texture.SetPixel(0, 1, Color.red);
            texture.SetPixel(1, 1, Color.red);
            texture.Apply();

            inactivetexture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            inactivetexture.SetPixel(0, 0, HoverColor);
            inactivetexture.SetPixel(1, 0, HoverColor);
            inactivetexture.SetPixel(0, 1, HoverColor);
            inactivetexture.SetPixel(1, 1, HoverColor);
            inactivetexture.Apply();

            texture2 = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            texture2.SetPixel(0, 0, Color.green);
            texture2.SetPixel(1, 0, Color.green);
            texture2.SetPixel(0, 1, Color.green);
            texture2.SetPixel(1, 1, Color.green);
            texture2.Apply();

            Activetexture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            Activetexture.SetPixel(0, 0, Color3);
            Activetexture.SetPixel(1, 0, Color3);
            Activetexture.SetPixel(0, 1, Color3);
            Activetexture.SetPixel(1, 1, Color3);
            Activetexture.Apply();

            backgroundtexture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            backgroundtexture.SetPixel(0, 0, BackGroundColor);
            backgroundtexture.SetPixel(1, 0, BackGroundColor);
            backgroundtexture.SetPixel(0, 1, BackGroundColor);
            backgroundtexture.SetPixel(1, 1, BackGroundColor);
            backgroundtexture.Apply();

            hoverbackground = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            hoverbackground.SetPixel(0, 0, hoverColor);
            hoverbackground.SetPixel(1, 0, hoverColor);
            hoverbackground.SetPixel(0, 1, hoverColor);
            hoverbackground.SetPixel(1, 1, hoverColor);
            hoverbackground.Apply();

            background = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            background.SetPixel(0, 0, BackGround);
            background.SetPixel(1, 0, BackGround);
            background.SetPixel(0, 1, BackGround);
            background.SetPixel(1, 1, BackGround);
            backgroundtexture.Apply();

            if (lineTex == null)
            {
                lineTex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                lineTex.SetPixel(0, 1, UnityEngine.Color.white);
                lineTex.Apply();
            }
            if (aaLineTex == null)
            {
                aaLineTex = new Texture2D(1, 3, TextureFormat.ARGB32, false);
                aaLineTex.SetPixel(0, 0, new UnityEngine.Color(1, 1, 1, 0));
                aaLineTex.SetPixel(0, 1, UnityEngine.Color.white);
                aaLineTex.SetPixel(0, 2, new UnityEngine.Color(1, 1, 1, 0));
                aaLineTex.Apply();
            }
            blitMaterial = (Material)typeof(GUI).GetMethod("get_blitMaterial", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, null);
            blendMaterial = (Material)typeof(GUI).GetMethod("get_blendMaterial", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, null);
        }

        void Update()
        {
            clientManager = GameObject.Find("ClientManager");
            objects = clientManager.transform.GetChild(0);
            List<GameObject> noGrandChildObjList = new List<GameObject>();
            /////////MENU HOTKEY////////////
            if (Input.GetKeyDown(KeyCode.Insert)) menu = !menu;

            ////////////////ENEMY LIST UPDATE/////////
            try
            {
                if (one <= two)
                {
                    one -= -1;
                    if (one == two - 1)
                    {

                        foreach (GameObject obj in objects)
                        {
                            GameObject child = obj.transform.GetChild(0).gameObject;
                            if (child != null)
                            {
                                noGrandChildObjList.Add(child);
                            }
                        }

                        foreach (GameObject go in noGrandChildObjList)
                        {
                            CheckObject(go);
                        }
                    }
                }
                if (one <= 0)
                {
                    badguys.Clear();
                    noGrandChildObjList.Clear();
                    one = two;
                }
            }
            catch { }
        }
        void OnGUI()
        {
            GUI.skin.window.active.background = backgroundtexture;
            GUI.skin.window.normal.background = backgroundtexture;
            GUI.skin.window.hover.background = backgroundtexture;
            GUI.skin.window.onFocused.background = backgroundtexture;
            GUI.skin.window.onHover.background = backgroundtexture;
            GUI.skin.window.onActive.background = backgroundtexture;
            GUI.skin.window.onNormal.background = backgroundtexture;
            GUI.skin.window.margin.left = 10;

            GUI.Label(new Rect(10, 100, 200, 20), Shit1);
            try
            {
                if (menu) rc = GUI.Window(0, rc, new GUI.WindowFunction(GI), "Crowseer");
                foreach (GameObject gameObj in badguys)
                {
                    Transform[] allChildren = gameObj.transform.GetComponentsInChildren<Transform>();
                    foreach (Transform child in allChildren)
                    {

                        Vector3 w2s = Camera.main.WorldToScreenPoint(gameObj.transform.position);
                        Vector3 w2s2 = Camera.main.WorldToScreenPoint(child.position);
                        if (w2s.z > -1)
                        // if (w2s.z > 0)
                        {
                            // if ((w2s.x > 0) || (w2s.x < Screen.width) || (w2s.y > 0) || (w2s.y < Screen.height))
                            {
                                var distance = Vector3.Distance(Camera.main.transform.position, gameObj.transform.position);
                                //  if (distance > 10)
                                {
                                    float num = Mathf.Abs(w2s.y - w2s2.y);
                                    if (IsVisable(Camera.main.gameObject, child.position))
                                    {
                                        if (BoxEsp) Box(w2s.x, Screen.height - w2s.y, num / 1.8f, num, texture2, 1f);
                                    }
                                    else
                                    {
                                        if (BoxEsp) Box(w2s.x, Screen.height - w2s.y, num / 1.8f, num, texture, 1f);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void CheckObject(GameObject go)
        {
            if (nameToAdd == "NPC")
            {
                if (go.name.Contains("King") || go.name.Contains("Captain"))
                {
                    if (!badguys.Contains(go))
                    {
                        badguys.Add(go);
                    }
                }
            }
            else if (nameToAdd == "RemotePlayer")
            {
                if (go.layer == 22)
                {
                    if (!badguys.Contains(go))
                    {
                        badguys.Add(go);
                    }
                }
            }
            else if (nameToAdd == "Both")
            {
                if (go.name.Contains("King") || go.name.Contains("Captain"))
                {
                    if (!badguys.Contains(go))
                    {
                        badguys.Add(go);
                    }
                }
                if (go.layer == 22)
                {
                    if (!badguys.Contains(go))
                    {
                        badguys.Add(go);
                    }
                }
            }
        }
    }
}
