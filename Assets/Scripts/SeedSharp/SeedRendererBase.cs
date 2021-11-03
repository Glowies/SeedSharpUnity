using System;
using System.Numerics;
using System.Threading.Tasks;

namespace SeedSharp
{
    public abstract class SeedRendererBase
    {
        public Matrix3x2 WorldToViewMatrix { get; set; }
        public Plant RenderPlant { get; set; }

        public SeedRendererBase(Plant renderPlant)
        {
            RenderPlant = renderPlant;
            WorldToViewMatrix = Matrix3x2.CreateTranslation(.5f, 0);
        }

        public abstract void DrawLine(Vector2 p1, Vector2 p2);

        public abstract Task DrawLineAsync(Vector2 p1, Vector2 p2);

        public virtual void Render()
        {
            Render(RenderPlant.Root);
        }

        public void Render(PlantSegment segment)
        {
            if (segment is null)
                return;

            foreach(var child in segment.Children)
            {
                Render(child);
            }
            Render(segment.Next);

            var p1 = Vector2.Zero;
            var p2 = Vector2.UnitY * segment.Length;

            var matrix = segment.LocalToWorldMatrix * WorldToViewMatrix;

            var p1World = Vector2.Transform(p1, matrix);
            var p2World = Vector2.Transform(p2, matrix);

            DrawLine(p1World, p2World);
        }

        public virtual async Task RenderAsync()
        {
            await RenderAsync(RenderPlant.Root);
        }

        public async Task RenderAsync(PlantSegment segment)
        {
            if (segment is null)
                return;

            foreach (var child in segment.Children)
            {
                await RenderAsync(child);
            }
            await RenderAsync(segment.Next);

            var p1 = Vector2.Zero;
            var p2 = Vector2.UnitY * segment.Length;

            var matrix = segment.LocalToWorldMatrix * WorldToViewMatrix;

            var p1World = Vector2.Transform(p1, matrix);
            var p2World = Vector2.Transform(p2, matrix);

            await DrawLineAsync(p1World, p2World);
        }

        public virtual void SetViewportScale(Vector2 scale)
        {
            var translation = Matrix3x2.CreateTranslation(.5f, 0);
            var scaling = Matrix3x2.CreateScale(scale);
            WorldToViewMatrix = translation * scaling;
        }
    }
}
