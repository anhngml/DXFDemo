using netDxf;

namespace DXFDemo.Utils
{
    public static class DrawUtils
    {
        public static List<Vector2> polygon(Vector2 center, double radius, int numVertices)
        {
            List<Vector2> Vertices = new List<Vector2>();
            var pieceAngle = 2 * Math.PI / numVertices;
            for(double ang = -Math.PI/2 + pieceAngle/2; ang < 2 * Math.PI - Math.PI/2 + pieceAngle / 2; ang+=pieceAngle)
            {
                var p = new Vector2(center.X + radius * Math.Cos(ang), center.Y + radius * Math.Sin(ang));
                Vertices.Add(p);
            }
            return Vertices;
        }
    }
}
