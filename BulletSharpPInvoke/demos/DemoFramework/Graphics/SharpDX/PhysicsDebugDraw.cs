using BulletSharp;
using SharpDX.Direct3D10;
using SharpDX.DXGI;
using DataStream = global::SharpDX.DataStream;
using Device = SharpDX.Direct3D10.Device;

namespace DemoFramework.SharpDX
{
    public class PhysicsDebugDraw : BufferedDebugDraw
    {
        Device _device;
        InputAssemblerStage _inputAssembler;
        InputLayout _inputLayout;
        BufferDescription _vertexBufferDesc;
        Buffer _vertexBuffer;
        VertexBufferBinding _vertexBufferBinding;
        int _vertexCount;

        public PhysicsDebugDraw(SharpDXGraphics graphics)
        {
            _device = graphics.Device;
            _inputAssembler = _device.InputAssembler;

            InputElement[] elements = {
                new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0, InputClassification.PerVertexData, 0),
                new InputElement("COLOR", 0, Format.R8G8B8A8_UNorm, 12, 0, InputClassification.PerVertexData, 0)
            };
            _inputLayout = new InputLayout(_device, graphics.GetDebugDrawPass().Description.Signature, elements);

            _vertexBufferDesc = new BufferDescription
            {
                Usage = ResourceUsage.Dynamic,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write
            };

            _vertexBufferBinding = new VertexBufferBinding(null, PositionColored.Stride, 0);
        }
        /*
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (vertexBuffer != null)
                {
                    vertexBuffer.Dispose();
                    vertexBuffer = null;
                }
            }

            base.Dispose(disposing);
        }
        */
        public void DrawDebugWorld(DynamicsWorld world)
        {
            world.DebugDrawWorld();

            if (LineIndex == 0)
                return;

            _inputAssembler.InputLayout = _inputLayout;

            if (_vertexCount != LineIndex)
            {
                if (_vertexBuffer != null)
                {
                    _vertexBuffer.Dispose();
                }
                _vertexCount = LineIndex;
                _vertexBufferDesc.SizeInBytes = PositionColored.Stride * _vertexCount;
                using (var data = new DataStream(_vertexBufferDesc.SizeInBytes, false, true))
                {
                    data.WriteRange(Lines, 0, _vertexCount);
                    data.Position = 0;
                    _vertexBuffer = new Buffer(_device, data, _vertexBufferDesc);
                }
                _vertexBufferBinding.Buffer = _vertexBuffer;
            }
            else
            {
                using (var map = _vertexBuffer.Map(MapMode.WriteDiscard))
                {
                    map.WriteRange(Lines, 0, _vertexCount);
                }
                _vertexBuffer.Unmap();
            }

            _inputAssembler.SetVertexBuffers(0, _vertexBufferBinding);
            _inputAssembler.PrimitiveTopology = global::SharpDX.Direct3D.PrimitiveTopology.LineList;

            _device.Draw(_vertexCount, 0);

            LineIndex = 0;
        }
    }
};
