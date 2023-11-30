using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cs4423fp.InputController
{
    public static class MultiSelect
    {
        private static Texture2D _whiteTexture;

        public static Texture2D WhiteTexture{
            get{
                if(_whiteTexture == null){
                    _whiteTexture = new Texture2D(1,1);
                    _whiteTexture.SetPixel(0, 0, Color.white);
                    _whiteTexture.Apply();
                }

                return _whiteTexture;
            }
        }

        public static void DrawScreenRect(Rect rect, Color color){
            GUI.color = color;
            GUI.DrawTexture(rect, WhiteTexture);
        }

        public static void DrawScreenRectBorder(Rect rect, float thickness, Color color){
            DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
            DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
            DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
            DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
        }

        public static Rect GetScreenRect(Vector2 screenPosition1, Vector2 screenPosition2){
            screenPosition1.y = Screen.height - screenPosition1.y;
            screenPosition2.y = Screen.height - screenPosition2.y;

            Vector2 bR = Vector2.Max(screenPosition1, screenPosition2);
            Vector2 tL = Vector2.Min(screenPosition1, screenPosition2);

            return Rect.MinMaxRect(tL.x, tL.y, bR.x, bR.y);
        }

        public static Bounds GetViewportBounds(Camera camera, Vector2 screenPosition1, Vector2 screenPosition2){
            Vector2 position1 = camera.ScreenToViewportPoint(screenPosition1);
            Vector2 position2 = camera.ScreenToViewportPoint(screenPosition2);

            Vector2 min = Vector2.Min(position1, position2);
            Vector2 max = Vector2.Max(position1, position2);

            Bounds bounds = new Bounds();
            bounds.SetMinMax(min, max);

            return bounds; 
        }
    }
}
