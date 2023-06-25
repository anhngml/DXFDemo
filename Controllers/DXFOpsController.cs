using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using netDxf.Header;
using netDxf;
using netDxf.Entities;
using System.IO;
using DXFDemo.Utils;
using netDxf.Tables;

namespace DXFDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DXFOpsController : ControllerBase
    {
        [HttpGet]
        public FileStreamResult GetDXF(double h, double r, int s)
        {
            // your DXF file name
            string file = "sample.dxf";

            // create a dimension style
            DimensionStyle style = new DimensionStyle("MyStyle");
            style.TextHeight = 1;
            style.DimSuffix = " m";
            style.DimLineColor = AciColor.Yellow;
            style.ExtLineColor = AciColor.Blue;

            // create a new document, by default it will create an AutoCad2000 DXF version
            var doc = DxfDocument.Load(file);

            // add the dimension style to the document
            doc.DimensionStyles.Add(style);

            var margin = 5;

            var center1 = new Vector2(120, 160);

            //var circle = new Circle(center1, r);
            //doc.Entities.Add(circle);

            var poligonVertices = DrawUtils.polygon(center1, r, s);
            var polygon = new Polyline2D(poligonVertices, true);
            doc.Entities.Add(polygon);

            var d = r * 2;

            var _l = poligonVertices.Min(e => e.X);
            var _r = poligonVertices.Max(e => e.X);
            var _w = _r - _l;

            var top = new Vector2(120 - _w/2, 160 - r - margin);
            var right = new Vector2(top.X + _w, top.Y);
            var bottom = new Vector2(right.X, right.Y - h);
            var left = new Vector2(top.X, bottom.Y);

            var polyline = new Polyline2D(new List<Vector2>() { top, right, bottom, left }, true);
            doc.Entities.Add(polyline);

            //doc.Layouts.Add(new netDxf.Objects.Layout)

            //LinearDimension dim = new LinearDimension(left, top, 5, 0.25, DimensionStyle.Default);
            //dim.Layer = new Layer("Dimensions");
            // add the dimension to the document

            //var line = new Line(center1, poligonVertices[0]);
            //doc.Entities.Add(line);
            //var dim = new AlignedDimension(line, 30);

            //doc.Entities.Add(dim);

            var stream = new MemoryStream();
            doc.Save(stream);
            stream.Position = 0;

            return new FileStreamResult(stream, "application/dxf")
            {
                FileDownloadName = file
            };
        }

    }
}
